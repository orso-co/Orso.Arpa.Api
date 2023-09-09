using AutoMapper;
using MediatR;
using Orso.Arpa.Application.EducationApplication.Interfaces;
using Orso.Arpa.Application.EducationApplication.Model;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Application.EducationApplication.Services
{
    public class EducationService : BaseService<
        EducationDto,
        Education,
        EducationCreateDto,
        CreateEducation.Command,
        EducationModifyDto,
        EducationModifyBodyDto,
        ModifyEducation.Command
        >, IEducationService
    {
        public EducationService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}
