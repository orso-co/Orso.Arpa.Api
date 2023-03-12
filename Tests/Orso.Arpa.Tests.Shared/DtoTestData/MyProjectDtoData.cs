using System;
using System.Collections.Generic;
using Orso.Arpa.Application.MyProjectApplication;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class MyProjectDtoData
    {
        public static List<MyProjectDto> PerformerProjects
        {
            get
            {
                return new List<MyProjectDto>()
                {
                    PerformerSchneekoeniginDto,
                    PerformerChorwerkstattBerlinDto,
                    PerformerHoorayForHollywoodDto
                };
            }
        }

        public static MyProjectDto PerformerSchneekoeniginDto
        {
            get
            {
                var dto = new MyProjectDto
                {
                    Project = ProjectDtoData.Schneekönigin
                };
                dto.Participations.Add(new MyProjectParticipationDto
                {
                    CommentByStaffInner = "Comment by staff",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("429ac181-9b36-4635-8914-faabc5f593ff"),
                    MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
                    ParticipationStatusInner = ProjectParticipationStatusInner.Refusal,
                    ParticipationStatusInternal = ProjectParticipationStatusInternal.Candidate,
                });
                return dto;
            }
        }

        public static MyProjectDto PerformerChorwerkstattBerlinDto
        {
            get
            {
                return new MyProjectDto
                {
                    Project = ProjectDtoData.ChorwerkstattBerlin,
                    ParentProject = ProjectDtoData.Chorwerkstatt
                };
            }
        }

        public static MyProjectDto PerformerHoorayForHollywoodDto
        {
            get
            {
                var dto = new MyProjectDto
                {
                    Project = ProjectDtoData.HoorayForHollywood
                };
                dto.Participations.Add(new MyProjectParticipationDto
                {
                    CommentByStaffInner = "Comment by staff",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("42fe1129-72f1-4935-b136-9bc41583e895"),
                    MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
                    ParticipationStatusInner = ProjectParticipationStatusInner.Acceptance,
                    ParticipationStatusInternal = ProjectParticipationStatusInternal.Candidate,
                });
                return dto;
            }
        }
    }
}
