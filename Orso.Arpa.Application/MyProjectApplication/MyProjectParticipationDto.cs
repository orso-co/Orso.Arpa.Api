using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;

namespace Orso.Arpa.Application.MyProjectApplication
{
    public class MyProjectParticipationDto : BaseEntityDto
    {
        public ProjectParticipationStatusInner? ParticipationStatusInner { get; set; }

        public ProjectParticipationStatusInternal? ParticipationStatusInternal { get; set; }

        public string CommentByPerformerInner { get; set; }

        public string CommentByStaffInner { get; set; }

        public ReducedMusicianProfileDto MusicianProfile { get; set; }
    }

    public class MyProjectParticipationDtoMappingProfile : Profile
    {
        public MyProjectParticipationDtoMappingProfile()
        {
            _ = CreateMap<ProjectParticipation, MyProjectParticipationDto>();
        }
    }
}
