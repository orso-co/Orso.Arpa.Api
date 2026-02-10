using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.OrganizationApplication.Model;

namespace Orso.Arpa.Application.OrganizationApplication.Interfaces
{
    public interface IOrganizationService
    {
        Task<OrganizationDto> CreateAsync(OrganizationCreateDto dto);
        Task<OrganizationDto> GetByIdAsync(Guid id);
        Task<IEnumerable<OrganizationDto>> GetAsync();
        Task ModifyAsync(OrganizationModifyDto dto);
        Task DeleteAsync(Guid id);

        Task<PersonOrganizationDto> AddPersonAsync(Guid organizationId, PersonOrganizationCreateDto dto);
        Task ModifyPersonOrganizationAsync(PersonOrganizationModifyDto dto);
        Task RemovePersonOrganizationAsync(Guid personOrganizationId);
        Task<IEnumerable<PersonOrganizationDto>> GetPersonOrganizationsAsync(Guid organizationId);

        Task<OrganizationRelationshipDto> AddRelationshipAsync(OrganizationRelationshipCreateDto dto);
        Task ModifyRelationshipAsync(OrganizationRelationshipModifyDto dto);
        Task RemoveRelationshipAsync(Guid relationshipId);
        Task<IEnumerable<OrganizationRelationshipDto>> GetRelationshipsAsync(Guid organizationId);

        Task<IEnumerable<string>> GetAllTagsAsync();
    }
}
