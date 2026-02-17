using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SurveyDomain.Commands;

namespace Orso.Arpa.Domain.SurveyDomain.Model;

public class Survey : BaseEntity
{
    public Survey(Guid? id, CreateSurvey.Command command) : base(id)
    {
        Title = command.Title;
        Description = command.Description;
        StartDate = command.StartDate;
        EndDate = command.EndDate;
        IsActive = false;
        IsAnonymous = command.IsAnonymous;
    }

    protected Survey() { }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsAnonymous { get; private set; }

    public virtual ICollection<SurveyQuestion> Questions { get; private set; } = new List<SurveyQuestion>();
    public virtual ICollection<SurveyUserResponse> UserResponses { get; private set; } = new List<SurveyUserResponse>();

    public void Update(ModifySurvey.Command command)
    {
        Title = command.Title;
        Description = command.Description;
        StartDate = command.StartDate;
        EndDate = command.EndDate;
        IsAnonymous = command.IsAnonymous;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
