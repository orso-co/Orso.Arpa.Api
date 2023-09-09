using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.MusicianProfileApplication.Model;
using Orso.Arpa.Application.ProjectApplication.Interfaces;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Application.MyProjectApplication.Model
{
    public class MyProjectParticipationDto : BaseEntityDto, IHasProjectParticipationStatusDto
    {
        public ProjectParticipationStatusInner? ParticipationStatusInner { get; set; }

        public ProjectParticipationStatusInternal? ParticipationStatusInternal { get; set; }

        public ProjectParticipationStatusResult ParticipationStatusResult { get; set; }

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
