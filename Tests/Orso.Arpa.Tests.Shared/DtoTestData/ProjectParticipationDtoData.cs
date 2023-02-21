using System;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class ProjectParticipationDtoData
    {
        public static ProjectParticipationDto PerformerChorwerkstattFreiburgParticipationForStaff
        {
            get
            {
                ProjectParticipationDto dto = PerformerChorwerkstattFreiburgForPerformer;
                dto.CommentTeam = "Comment by team";
                dto.InvitationStatus = ProjectInvitationStatus.Invited;
                dto.Person = ReducedPersonDtoData.Performer;
                return dto;
            }
        }

        public static ProjectParticipationDto PerformerChorwerkstattFreiburgForPerformer
        {
            get
            {
                return new ProjectParticipationDto
                {
                    CommentByStaffInner = "Comment by staff",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("bd70283c-22ba-4ddb-9ae2-5f85d0151811"),
                    MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
                    ParticipationStatusInner = ProjectParticipationStatusInner.Acceptance,
                    ParticipationStatusInternal = ProjectParticipationStatusInternal.Acceptance,
                    Project = ReducedProjectDtoData.ChorwerkstattFreiburg
                };
            }
        }

        public static ProjectParticipationDto PerformerHoorayForHollywoodParticipationForStaff
        {
            get
            {
                ProjectParticipationDto dto = PerformerHoorayForHollywoodForPerformer;
                dto.CommentTeam = "Comment by team";
                dto.InvitationStatus = ProjectInvitationStatus.Invited;
                dto.Person = ReducedPersonDtoData.Performer;
                return dto;
            }
        }

        public static ProjectParticipationDto PerformerHoorayForHollywoodForPerformer
        {
            get
            {
                return new ProjectParticipationDto
                {
                    CommentByStaffInner = "Comment by staff",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("42fe1129-72f1-4935-b136-9bc41583e895"),
                    MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
                    ParticipationStatusInner = ProjectParticipationStatusInner.Acceptance,
                    ParticipationStatusInternal = ProjectParticipationStatusInternal.Candidate,
                    Project = ReducedProjectDtoData.HoorayForHollywood
                };
            }
        }

        public static ProjectParticipationDto PerformerChorwerkstattParticipationForStaff
        {
            get
            {
                ProjectParticipationDto dto = PerformerChorwerkstattForPerformer;
                dto.CommentTeam = "Comment by team";
                dto.InvitationStatus = ProjectInvitationStatus.Invited;
                dto.Person = ReducedPersonDtoData.Performer;
                return dto;
            }
        }

        public static ProjectParticipationDto PerformerChorwerkstattForPerformer
        {
            get
            {
                return new ProjectParticipationDto
                {
                    CommentByStaffInner = "Comment by staff",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("014b7ae4-9c6a-4273-b54e-c40a911d41a3"),
                    MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
                    ParticipationStatusInner = ProjectParticipationStatusInner.Acceptance,
                    ParticipationStatusInternal = ProjectParticipationStatusInternal.Refusal,
                    Project = ReducedProjectDtoData.Chorwerkstatt
                };
            }
        }

        public static ProjectParticipationDto PerformerSchneeköniginParticipationForStaff
        {
            get
            {
                ProjectParticipationDto dto = PerformerSchneeköniginParticipationForPerformer;
                dto.CommentTeam = "Comment by team";
                dto.InvitationStatus = ProjectInvitationStatus.Invited;
                dto.Person = ReducedPersonDtoData.Performer;
                return dto;
            }
        }

        public static ProjectParticipationDto PerformerSchneeköniginParticipationForPerformer => new()
        {
            CommentByStaffInner = "Comment by staff",
            CreatedAt = FakeDateTime.UtcNow,
            CreatedBy = "anonymous",
            Id = Guid.Parse("429ac181-9b36-4635-8914-faabc5f593ff"),
            MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
            ParticipationStatusInner = ProjectParticipationStatusInner.Refusal,
            ParticipationStatusInternal = ProjectParticipationStatusInternal.Candidate,
            Project = ReducedProjectDtoData.Schneekönigin
        };

        public static ProjectParticipationDto PerformerRockingXMasParticipationForStaff
        {
            get
            {
                ProjectParticipationDto dto = PerformerRockingXMasParticipationForPerformer;
                dto.Person = ReducedPersonDtoData.Performer;
                return dto;
            }
        }

        public static ProjectParticipationDto PerformerRockingXMasParticipationForPerformer => new()
        {
            CreatedAt = FakeDateTime.UtcNow,
            CreatedBy = "anonymous",
            Id = Guid.Parse("2b3503d3-9061-4110-85e6-88e864842ece"),
            MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
            Project = ReducedProjectDtoData.RockingXMas
        };

        public static ProjectParticipationDto AdminRockingXMasParticipation => new()
        {
            CreatedAt = FakeDateTime.UtcNow,
            CreatedBy = "anonymous",
            Id = Guid.Parse("55cb5f7d-2fd7-4328-9d27-413dab753e62"),
            MusicianProfile = ReducedMusicianProfileDtoData.AdminProfile1,
            Person = ReducedPersonDtoData.Admin,
            Project = ReducedProjectDtoData.RockingXMas
        };

        public static ProjectParticipationDto StaffRockingXMasBassParticipation => new()
        {
            CreatedAt = FakeDateTime.UtcNow,
            CreatedBy = "anonymous",
            Id = Guid.Parse("4b9666cf-2d06-43cc-bd9f-f2d665562471"),
            MusicianProfile = ReducedMusicianProfileDtoData.StaffProfile2,
            Person = ReducedPersonDtoData.Staff,
            Project = ReducedProjectDtoData.RockingXMas
        };

        public static ProjectParticipationDto StaffRockingXMasTenorParticipation => new()
        {
            CreatedAt = FakeDateTime.UtcNow,
            CreatedBy = "anonymous",
            Id = Guid.Parse("da5eb778-b907-45ce-955a-2af2e6c0b60f"),
            MusicianProfile = ReducedMusicianProfileDtoData.StaffProfile1,
            Person = ReducedPersonDtoData.Staff,
            Project = ReducedProjectDtoData.RockingXMas
        };
    }
}
