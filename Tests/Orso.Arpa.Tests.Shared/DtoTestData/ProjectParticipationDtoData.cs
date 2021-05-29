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
                return dto;
            }
        }

        public static ProjectParticipationDto PerformerSchneeköniginParticipationForPerformer
        {
            get
            {
                return new ProjectParticipationDto
                {
                    CommentByStaffInner = "Comment by staff",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("429ac181-9b36-4635-8914-faabc5f593ff"),
                    MusicianProfile = new ReducedMusicianProfileDto
                    {
                        Id = Guid.Parse("9a609084-a5b6-485f-8960-724a8b470b13"),
                        InstrumentName = "Alto 1",
                        Qualification = "Amateur",
                    },
                    ParticipationStatusInner = "Acceptance",
                    ParticipationStatusInnerId = Guid.Parse("eef4a4d1-796b-4b37-96f6-f31dbccf0aeb"),
                    ParticipationStatusInternal = "Candidate",
                    ParticipationStatusInternalId = Guid.Parse("b0dcb5e9-bbc6-4004-b9d7-0f6723416b9b"),
                    Project = new ReducedProjectDto
                    {
                        Code = "1007",
                        Description = "Let it snow",
                        Id = Guid.Parse("b781c54d-8115-4561-b01e-9836fa05175e"),
                        ShortTitle = "Schnee",
                        Title = "Die Schneekönigin"
                    }
                };
            }
        }

        public static ProjectParticipationDto PerformerRockingXMasParticipation
        {
            get
            {
                return new ProjectParticipationDto
                {

                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("2b3503d3-9061-4110-85e6-88e864842ece"),
                    MusicianProfile = new ReducedMusicianProfileDto
                    {
                        Id = Guid.Parse("9a609084-a5b6-485f-8960-724a8b470b13"),
                        InstrumentName = "Alto 1",
                        Qualification = "Amateur"
                    },
                    Project = new ReducedProjectDto
                    {
                        Code = "1005",
                        Description = "Rocking around the christmas tree",
                        Id = Guid.Parse("a19d84f1-4ac1-49c3-abfe-527092b80b6d"),
                        ShortTitle = "RockXmas",
                        Title = "Rocking X-mas Freiburg"
                    }
                };
            }
        }
    }
}
