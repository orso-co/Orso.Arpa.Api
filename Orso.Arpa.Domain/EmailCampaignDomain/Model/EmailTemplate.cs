using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.EmailCampaignDomain.Commands;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Model;

public class EmailTemplate : BaseEntity
{
    public EmailTemplate(Guid? id, CreateEmailTemplate.Command command) : base(id)
    {
        Name = command.Name;
        Description = command.Description;
        ProjectDataJson = command.ProjectDataJson;
        MjmlSource = command.MjmlSource;
        CompiledHtml = command.CompiledHtml;
    }

    protected EmailTemplate() { }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ProjectDataJson { get; private set; }
    public string MjmlSource { get; private set; }
    public string CompiledHtml { get; private set; }
    public string ThumbnailPath { get; private set; }

    public void Update(ModifyEmailTemplate.Command command)
    {
        Name = command.Name;
        Description = command.Description;
        ProjectDataJson = command.ProjectDataJson;
        MjmlSource = command.MjmlSource;
        CompiledHtml = command.CompiledHtml;
    }
}
