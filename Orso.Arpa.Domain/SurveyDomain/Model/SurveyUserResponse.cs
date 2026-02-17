using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.SurveyDomain.Model;

public class SurveyUserResponse : BaseEntity
{
    public SurveyUserResponse(Guid? id, Guid surveyId, Guid userId) : base(id)
    {
        SurveyId = surveyId;
        UserId = userId;
        StartedAt = DateTime.UtcNow;
        IsComplete = false;
    }

    protected SurveyUserResponse() { }

    public Guid SurveyId { get; private set; }
    public virtual Survey Survey { get; private set; }
    public Guid UserId { get; private set; }
    public virtual User User { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public bool IsComplete { get; private set; }
    public string IpAddress { get; private set; }

    public virtual ICollection<SurveyAnswer> Answers { get; private set; } = new List<SurveyAnswer>();

    public void Complete(string ipAddress)
    {
        CompletedAt = DateTime.UtcNow;
        IsComplete = true;
        IpAddress = ipAddress;
    }
}
