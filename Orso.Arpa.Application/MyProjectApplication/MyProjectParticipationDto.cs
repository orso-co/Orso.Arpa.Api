using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.SelectValueApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MyProjectApplication
{
    public class MyProjectParticipationDto : BaseEntityDto
    {
        public SelectValueDto ParticipationStatusInner { get; set; }

        public SelectValueDto ParticipationStatusInternal { get; set; }

        public string CommentByPerformerInner { get; set; }

        public string CommentByStaffInner { get; set; }

        public ReducedMusicianProfileDto MusicianProfile { get; set; }
    }

    public class MyProjectParticipationDtoMappingProfile : Profile
    {
        public MyProjectParticipationDtoMappingProfile()
        {
            CreateMap<ProjectParticipation, MyProjectParticipationDto>();
        }
    }
}
