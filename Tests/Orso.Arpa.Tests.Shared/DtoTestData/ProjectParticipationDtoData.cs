using System;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class ProjectParticipationDtoData
    {
        public static ProjectParticipationDto PerformerSchneeköniginParticipationForStaff
        {
            get
            {
                ProjectParticipationDto dto = PerformerSchneeköniginParticipationForPerformer;
                dto.CommentTeam = "Comment by team";
                dto.InvitationStatus = "Invited";
                dto.InvitationStatusId = Guid.Parse("625a9195-2380-4762-8dc6-13163e354ef6");
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
            MusicianProfile = new ReducedMusicianProfileDto
            {
                Id = Guid.Parse("9a609084-a5b6-485f-8960-724a8b470b13"),
                InstrumentName = "Alto",
                Qualification = "Amateur",
            },
            ParticipationStatusInner = "Acceptance",
            ParticipationStatusInnerId = Guid.Parse("eef4a4d1-796b-4b37-96f6-f31dbccf0aeb"),
            ParticipationStatusInternal = "Candidate",
            ParticipationStatusInternalId = Guid.Parse("b0dcb5e9-bbc6-4004-b9d7-0f6723416b9b"),
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
            MusicianProfile = new ReducedMusicianProfileDto
            {
                Id = Guid.Parse("9a609084-a5b6-485f-8960-724a8b470b13"),
                InstrumentName = "Alto",
                Qualification = "Amateur"
            },
            Project = ReducedProjectDtoData.RockingXMas
        };

        public static ProjectParticipationDto AdminRockingXMasParticipation => new()
        {
            CreatedAt = FakeDateTime.UtcNow,
            CreatedBy = "anonymous",
            Id = Guid.Parse("55cb5f7d-2fd7-4328-9d27-413dab753e62"),
            MusicianProfile = new ReducedMusicianProfileDto
            {
                Id = Guid.Parse("7c215684-ee09-424f-9955-9b427494eaeb"),
                InstrumentName = "Soprano"
            },
            Person = ReducedPersonDtoData.Admin,
            Project = ReducedProjectDtoData.RockingXMas
        };

        public static ProjectParticipationDto StaffRockingXMasBassParticipation => new()
        {
            CreatedAt = FakeDateTime.UtcNow,
            CreatedBy = "anonymous",
            Id = Guid.Parse("4b9666cf-2d06-43cc-bd9f-f2d665562471"),
            MusicianProfile = new ReducedMusicianProfileDto
            {
                Id = Guid.Parse("c2b727eb-16f7-440d-a003-aab073532bbf"),
                InstrumentName = "Basso"
            },
            Person = ReducedPersonDtoData.Staff,
            Project = ReducedProjectDtoData.RockingXMas
        };

        public static ProjectParticipationDto StaffRockingXMasTenorParticipation => new()
        {
            CreatedAt = FakeDateTime.UtcNow,
            CreatedBy = "anonymous",
            Id = Guid.Parse("da5eb778-b907-45ce-955a-2af2e6c0b60f"),
            MusicianProfile = new ReducedMusicianProfileDto
            {
                Id = Guid.Parse("f9d85f58-9156-4d5d-988b-3a3d0cb67205"),
                InstrumentName = "Tenor"
            },
            Person = ReducedPersonDtoData.Staff,
            Project = ReducedProjectDtoData.RockingXMas
        };
    }
}
