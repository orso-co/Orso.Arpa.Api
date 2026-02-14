using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.EmailCampaignApplication.Model;

namespace Orso.Arpa.Application.EmailCampaignApplication.Interfaces;

public interface IEmailTemplateService
{
    Task<EmailTemplateDto> CreateAsync(EmailTemplateCreateDto createDto);
    Task<IEnumerable<EmailTemplateDto>> GetAsync();
    Task<EmailTemplateDto> GetByIdAsync(Guid id);
    Task ModifyAsync(EmailTemplateModifyDto modifyDto);
    Task DeleteAsync(Guid id);
}
