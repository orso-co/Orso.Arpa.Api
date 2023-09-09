using System;
using System.Collections.Generic;
using Orso.Arpa.Application.MyProjectApplication.Model;
using Orso.Arpa.Domain.ProjectDomain.Enums;
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
                dto.Participations.Add(new MyProjectParticipationDto()
                {
                    MusicianProfile = ReducedMusicianProfileDtoData.PerformerDeactivatedTubaProfile,
                    ParticipationStatusResult = ProjectParticipationStatusResult.Pending
                });
                dto.Participations.Add(new MyProjectParticipationDto
                {
                    CommentByStaffInner = "Comment by staff",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("429ac181-9b36-4635-8914-faabc5f593ff"),
                    MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
                    ParticipationStatusInner = ProjectParticipationStatusInner.Refusal,
                    ParticipationStatusInternal = ProjectParticipationStatusInternal.Candidate,
                    ParticipationStatusResult = ProjectParticipationStatusResult.Refusal
                });
                dto.Participations.Add(new MyProjectParticipationDto()
                {
                    MusicianProfile = ReducedMusicianProfileDtoData.PerformerHornProfile,
                    ParticipationStatusResult = ProjectParticipationStatusResult.Pending
                });
                return dto;
            }
        }

        public static MyProjectDto PerformerChorwerkstattBerlinDto
        {
            get
            {
                var dto = new MyProjectDto
                {
                    Project = ProjectDtoData.ChorwerkstattBerlin,
                    ParentProject = ProjectDtoData.Chorwerkstatt
                };
                dto.Participations.Add(new MyProjectParticipationDto()
                {
                    MusicianProfile = ReducedMusicianProfileDtoData.PerformerDeactivatedTubaProfile,
                    ParticipationStatusResult = ProjectParticipationStatusResult.Pending
                });
                dto.Participations.Add(new MyProjectParticipationDto
                {
                    MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
                    ParticipationStatusResult = ProjectParticipationStatusResult.Pending
                });
                dto.Participations.Add(new MyProjectParticipationDto()
                {
                    MusicianProfile = ReducedMusicianProfileDtoData.PerformerHornProfile,
                    ParticipationStatusResult = ProjectParticipationStatusResult.Pending
                });
                return dto;
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
                dto.Participations.Add(new MyProjectParticipationDto()
                {
                    MusicianProfile = ReducedMusicianProfileDtoData.PerformerDeactivatedTubaProfile,
                    ParticipationStatusResult = ProjectParticipationStatusResult.Pending
                });
                dto.Participations.Add(new MyProjectParticipationDto
                {
                    CommentByStaffInner = "Comment by staff",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("42fe1129-72f1-4935-b136-9bc41583e895"),
                    MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
                    ParticipationStatusInner = ProjectParticipationStatusInner.Acceptance,
                    ParticipationStatusInternal = ProjectParticipationStatusInternal.Candidate,
                    ParticipationStatusResult = ProjectParticipationStatusResult.Pending
                });
                dto.Participations.Add(new MyProjectParticipationDto()
                {
                    MusicianProfile = ReducedMusicianProfileDtoData.PerformerHornProfile,
                    ParticipationStatusResult = ProjectParticipationStatusResult.Pending
                });
                return dto;
            }
        }
    }
}
