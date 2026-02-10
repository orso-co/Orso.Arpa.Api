using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.OrganizationApplication.Interfaces;
using Orso.Arpa.Application.OrganizationApplication.Model;
using Orso.Arpa.Domain.General.GenericHandlers;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.OrganizationDomain.Commands;
using Orso.Arpa.Domain.OrganizationDomain.Model;

namespace Orso.Arpa.Application.OrganizationApplication.Services
{
    public class OrganizationService : BaseService<
        OrganizationDto,
        Organization,
        OrganizationCreateDto,
        CreateOrganization.Command,
        OrganizationModifyDto,
        OrganizationModifyBodyDto,
        ModifyOrganization.Command>, IOrganizationService
    {
        private readonly IArpaContext _arpaContext;

        public OrganizationService(IMediator mediator, IMapper mapper, IArpaContext arpaContext)
            : base(mediator, mapper)
        {
            _arpaContext = arpaContext;
        }

        public Task<IEnumerable<OrganizationDto>> GetAsync()
        {
            return base.GetAsync(orderBy: o => o.OrderBy(org => org.Name));
        }

        public async Task<PersonOrganizationDto> AddPersonAsync(Guid organizationId, PersonOrganizationCreateDto dto)
        {
            var command = new CreatePersonOrganization.Command
            {
                OrganizationId = organizationId,
                PersonId = dto.PersonId,
                Function = dto.Function,
                RelationshipTypeId = dto.RelationshipTypeId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };
            PersonOrganization created = await _mediator.Send(command);
            return _mapper.Map<PersonOrganizationDto>(created);
        }

        public async Task ModifyPersonOrganizationAsync(PersonOrganizationModifyDto dto)
        {
            var command = _mapper.Map<ModifyPersonOrganization.Command>(dto);
            await _mediator.Send(command);
        }

        public async Task RemovePersonOrganizationAsync(Guid personOrganizationId)
        {
            await _mediator.Send(new Delete.Command<PersonOrganization> { Id = personOrganizationId });
        }

        public async Task<IEnumerable<PersonOrganizationDto>> GetPersonOrganizationsAsync(Guid organizationId)
        {
            var entities = await _arpaContext.Set<PersonOrganization>()
                .Where(po => po.OrganizationId == organizationId)
                .OrderBy(po => po.Person.Surname)
                .ThenBy(po => po.Person.GivenName)
                .ToListAsync();
            return _mapper.Map<IEnumerable<PersonOrganizationDto>>(entities);
        }

        public async Task<OrganizationRelationshipDto> AddRelationshipAsync(OrganizationRelationshipCreateDto dto)
        {
            var command = _mapper.Map<CreateOrganizationRelationship.Command>(dto);
            OrganizationRelationship created = await _mediator.Send(command);
            return _mapper.Map<OrganizationRelationshipDto>(created);
        }

        public async Task ModifyRelationshipAsync(OrganizationRelationshipModifyDto dto)
        {
            var command = _mapper.Map<ModifyOrganizationRelationship.Command>(dto);
            await _mediator.Send(command);
        }

        public async Task RemoveRelationshipAsync(Guid relationshipId)
        {
            await _mediator.Send(new Delete.Command<OrganizationRelationship> { Id = relationshipId });
        }

        public async Task<IEnumerable<OrganizationRelationshipDto>> GetRelationshipsAsync(Guid organizationId)
        {
            var entities = await _arpaContext.Set<OrganizationRelationship>()
                .Where(r => r.SourceOrganizationId == organizationId || r.TargetOrganizationId == organizationId)
                .OrderBy(r => r.SourceOrganization.Name)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OrganizationRelationshipDto>>(entities);
        }

        public async Task<IEnumerable<string>> GetAllTagsAsync()
        {
            var allTags = await _arpaContext.Set<Organization>()
                .Where(o => o.Tags != null && o.Tags != string.Empty)
                .Select(o => o.Tags)
                .ToListAsync();

            return allTags
                .SelectMany(t => t.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                .Distinct()
                .OrderBy(t => t);
        }
    }
}
