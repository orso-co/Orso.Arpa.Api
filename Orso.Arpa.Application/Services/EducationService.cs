using AutoMapper;
using MediatR;
using Orso.Arpa.Application.EducationApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Educations;

namespace Orso.Arpa.Application.Services
{
    public class EducationService : BaseService<
        EducationDto,
        Education,
        EducationCreateDto,
        Create.Command,
        EducationModifyDto,
        EducationModifyBodyDto,
        Modify.Command
        >, IEducationService
    {
        public EducationService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}
