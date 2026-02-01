using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.MyProjectApplication.Interfaces;
using Orso.Arpa.Application.MyProjectApplication.Model;
using Orso.Arpa.Application.SetlistApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.ProjectDomain.Commands;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Notifications;
using Orso.Arpa.Domain.ProjectDomain.Queries;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Application.MyProjectApplication.Services;

public class MyProjectService : IMyProjectService
{
    private readonly IUserAccessor _userAccessor;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IArpaContext _arpaContext;

    public MyProjectService(IUserAccessor userAccessor, IMediator mediator, IMapper mapper, IArpaContext arpaContext)
    {
        _userAccessor = userAccessor;
        _mediator = mediator;
        _mapper = mapper;
        _arpaContext = arpaContext;
    }

    public async Task<MyProjectListDto> GetMyProjectsAsync(int? offset, int? limit, bool includeCompleted)
    {
        var query = new ListProjectParticipationsForPerson.Query
        {
            PersonId = _userAccessor.PersonId,
            Offset = offset,
            Limit = limit,
            IncludeCompleted = includeCompleted
        };
        Tuple<IEnumerable<PersonProjectParticipationGrouping>, int> result = await _mediator.Send(query);

        return new MyProjectListDto
        {
            UserProjects = _mapper.Map<IList<MyProjectDto>>(result.Item1),
            TotalRecordsCount = result.Item2
        };
    }

    public async Task<MyProjectParticipationDto> SetProjectParticipationStatus(MyProjectParticipationModifyDto myProjectParticipationModifyDto)
    {
        SetMyProjectParticipationStatus.Command command = _mapper
            .Map<SetMyProjectParticipationStatus.Command>(myProjectParticipationModifyDto);

        ProjectParticipation projectParticipation = await _mediator.Send(command);

        var notification = new ProjectParticipationChangedNotification
        {
            ProjectParticipation = projectParticipation,
            ChangedByPerformer = true,
        };
        await _mediator.Publish(notification);

        return _mapper.Map<MyProjectParticipationDto>(projectParticipation);
    }

    public async Task<SetlistDto> GetProjectSetlistAsync(Guid projectId)
    {
        // Note: We don't check for active participation here because:
        // 1. The project list already filters which projects are visible to the user
        // 2. Users should be able to see setlists even with INTERESTED status
        // 3. The project visibility (IsHiddenForPerformers, Status, etc.) is already enforced
        //    when the user fetches their project list
        var personId = _userAccessor.PersonId;

        // Get the project with its setlist (pieces sorted by SortOrder)
        var project = await _arpaContext.Projects
            .Include(p => p.Setlist)
                .ThenInclude(s => s.Pieces.OrderBy(p => p.SortOrder))
                    .ThenInclude(sp => sp.MusicPiece)
                        .ThenInclude(mp => mp.Files)
                            .ThenInclude(f => f.Sections)
            .FirstOrDefaultAsync(p => p.Id == projectId);

        if (project?.Setlist == null)
        {
            return null;
        }

        // Get the user's musician profile sections (main instrument + doubling instruments)
        var userSectionIds = await _arpaContext.MusicianProfiles
            .Where(mp => mp.PersonId == personId)
            .SelectMany(mp => new[] { mp.InstrumentId }
                .Concat(mp.DoublingInstruments.Select(di => di.SectionId)))
            .ToListAsync();

        // Build a lookup of section parent IDs
        var allSections = await _arpaContext.Sections
            .Select(s => new { s.Id, s.ParentId })
            .ToListAsync();
        var sectionParentMap = allSections.ToDictionary(s => s.Id, s => s.ParentId);

        // Get all ancestor sections for the user's sections (parent, grandparent, etc.)
        // Files assigned to an ancestor section (e.g., "Choir") should be visible to all descendants (e.g., "Bass")
        var userSectionIdsWithAncestors = new HashSet<Guid>(userSectionIds);
        foreach (var sectionId in userSectionIds)
        {
            var currentId = sectionId;
            while (sectionParentMap.TryGetValue(currentId, out var parentId) && parentId.HasValue)
            {
                userSectionIdsWithAncestors.Add(parentId.Value);
                currentId = parentId.Value;
            }
        }

        // Map the setlist
        var setlistDto = _mapper.Map<SetlistDto>(project.Setlist);

        // Filter files by user's sections (including ancestors) in each piece
        foreach (var pieceDto in setlistDto.Pieces)
        {
            if (pieceDto.MusicPiece?.Files != null)
            {
                pieceDto.MusicPiece.Files = pieceDto.MusicPiece.Files
                    .Where(f => f.Sections == null || !f.Sections.Any() ||
                               f.Sections.Any(s => userSectionIdsWithAncestors.Contains(s.SectionId)))
                    .ToList();
            }
        }

        return setlistDto;
    }
}
