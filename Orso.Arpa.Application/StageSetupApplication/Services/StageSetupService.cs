using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.StageSetupApplication.Interfaces;
using Orso.Arpa.Application.StageSetupApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.StageSetupDomain.Commands;
using Orso.Arpa.Domain.StageSetupDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Delete;

namespace Orso.Arpa.Application.StageSetupApplication.Services
{
    public class StageSetupService : IStageSetupService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IArpaContext _arpaContext;
        private readonly string _storageBasePath;

        public StageSetupService(
            IMediator mediator,
            IMapper mapper,
            IArpaContext arpaContext)
        {
            _mediator = mediator;
            _mapper = mapper;
            _arpaContext = arpaContext;
            // TODO: Make this configurable via appsettings
            _storageBasePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "arpa",
                "stage-setups"
            );
        }

        public async Task<IEnumerable<StageSetupDto>> GetByProjectAsync(Guid projectId, bool includeHidden = false)
        {
            var query = _arpaContext.Set<StageSetup>()
                .Where(s => s.ProjectId == projectId);

            if (!includeHidden)
            {
                query = query.Where(s => s.IsVisibleToPerformers);
            }

            var setups = await query
                .Include(s => s.Positions)
                    .ThenInclude(p => p.MusicianProfile)
                        .ThenInclude(mp => mp.Person)
                .Include(s => s.Positions)
                    .ThenInclude(p => p.MusicianProfile)
                        .ThenInclude(mp => mp.Instrument)
                .OrderByDescending(s => s.IsActive)
                .ThenByDescending(s => s.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<StageSetupDto>>(setups);
        }

        public async Task<StageSetupDto> GetByIdAsync(Guid id)
        {
            var setup = await _arpaContext.Set<StageSetup>()
                .Include(s => s.Positions)
                    .ThenInclude(p => p.MusicianProfile)
                        .ThenInclude(mp => mp.Person)
                .Include(s => s.Positions)
                    .ThenInclude(p => p.MusicianProfile)
                        .ThenInclude(mp => mp.Instrument)
                .FirstOrDefaultAsync(s => s.Id == id);

            return _mapper.Map<StageSetupDto>(setup);
        }

        public async Task<StageSetupDto> CreateAsync(Guid projectId, StageSetupCreateDto createDto, Stream fileStream, string fileName, string contentType)
        {
            // Ensure storage directory exists
            var projectStoragePath = Path.Combine(_storageBasePath, projectId.ToString());
            Directory.CreateDirectory(projectStoragePath);

            // Generate unique filename
            var fileId = Guid.NewGuid();
            var extension = Path.GetExtension(fileName);
            var storagePath = Path.Combine(projectStoragePath, $"{fileId}{extension}");

            // Save file
            using (var fileStream2 = new FileStream(storagePath, FileMode.Create))
            {
                await fileStream.CopyToAsync(fileStream2);
            }

            // Get file size
            var fileInfo = new FileInfo(storagePath);

            var command = _mapper.Map<CreateStageSetup.Command>(createDto);
            command.ProjectId = projectId;
            command.FileName = fileName;
            command.StoragePath = storagePath;
            command.ContentType = contentType;
            command.FileSize = fileInfo.Length;

            var result = await _mediator.Send(command);
            return _mapper.Map<StageSetupDto>(result);
        }

        public async Task ModifyAsync(StageSetupModifyBodyDto modifyDto)
        {
            var command = _mapper.Map<ModifyStageSetup.Command>(modifyDto);
            await _mediator.Send(command);
        }

        public async Task DeleteAsync(Guid id)
        {
            // Get the setup to find the file path
            var setup = await _arpaContext.Set<StageSetup>().FindAsync(id);
            if (setup != null && !string.IsNullOrEmpty(setup.StoragePath) && File.Exists(setup.StoragePath))
            {
                File.Delete(setup.StoragePath);
            }

            var command = new Command<StageSetup> { Id = id };
            await _mediator.Send(command);
        }

        public async Task<(Stream FileStream, string FileName, string ContentType)> GetFileAsync(Guid id)
        {
            var setup = await _arpaContext.Set<StageSetup>().FindAsync(id);
            if (setup == null || string.IsNullOrEmpty(setup.StoragePath))
            {
                return (null, null, null);
            }

            if (!File.Exists(setup.StoragePath))
            {
                return (null, null, null);
            }

            var stream = new FileStream(setup.StoragePath, FileMode.Open, FileAccess.Read);
            return (stream, setup.FileName, setup.ContentType);
        }

        public async Task ReplaceFileAsync(Guid id, Stream fileStream, string fileName, string contentType)
        {
            var setup = await _arpaContext.Set<StageSetup>().FindAsync(id);
            if (setup == null)
            {
                throw new InvalidOperationException($"Stage setup with id {id} not found");
            }

            // Delete old file if it exists
            if (!string.IsNullOrEmpty(setup.StoragePath) && File.Exists(setup.StoragePath))
            {
                File.Delete(setup.StoragePath);
            }

            // Generate new storage path
            var projectStoragePath = Path.GetDirectoryName(setup.StoragePath)
                ?? Path.Combine(_storageBasePath, setup.ProjectId.ToString());
            Directory.CreateDirectory(projectStoragePath);

            var fileId = Guid.NewGuid();
            var extension = Path.GetExtension(fileName);
            var newStoragePath = Path.Combine(projectStoragePath, $"{fileId}{extension}");

            // Save new file
            using (var outputStream = new FileStream(newStoragePath, FileMode.Create))
            {
                await fileStream.CopyToAsync(outputStream);
            }

            // Update database record using the entity method
            var fileInfo = new FileInfo(newStoragePath);
            setup.UpdateFile(fileName, newStoragePath, contentType, fileInfo.Length);

            await _arpaContext.SaveChangesAsync(default);
        }

        public async Task ActivateAsync(Guid projectId, Guid id)
        {
            var command = new ActivateStageSetup.Command
            {
                Id = id,
                ProjectId = projectId
            };
            await _mediator.Send(command);
        }

        public async Task SetVisibilityAsync(Guid projectId, Guid id, bool isVisibleToPerformers)
        {
            var command = new SetStageSetupVisibility.Command
            {
                Id = id,
                ProjectId = projectId,
                IsVisibleToPerformers = isVisibleToPerformers
            };
            await _mediator.Send(command);
        }
    }
}
