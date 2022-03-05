using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.MyProjects;

public static class SetProjectParticipationStatus
{
    public class Command : IRequest<ProjectParticipation>
    {
        public string CommentByPerformerInner { get; set; }
        public Guid ParticipationStatusInnerId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid MusicianProfileId { get; set; }
    }
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IArpaContext arpaContext, ITokenAccessor tokenAccessor)
        {
            RuleFor(c => c.ProjectId)
                .Cascade(CascadeMode.Stop)
                .EntityExists<Command, Project>(arpaContext)
                .MustAsync(async (projectId, cancellation) =>
                    !(await arpaContext.FindAsync<Project>(new object[] { projectId }, cancellation))
                        .IsCompleted)
                .WithMessage(
                    "The project is completed. You may not set the participation of a completed project")
                .MustAsync(async (command, projectId, cancellation) =>
                    (await arpaContext.EntityExistsAsync<ProjectParticipation>(
                        pp => pp.ProjectId == projectId &&
                              pp.MusicianProfileId == command.MusicianProfileId, cancellation)))
                .WithErrorCode("404")
                .WithMessage("Project Participation could not be found.");

            RuleFor(c => c.ParticipationStatusInnerId)
                .SelectValueMapping<Command, ProjectParticipation>(arpaContext, p => p.ParticipationStatusInner);

            RuleFor(c => c.MusicianProfileId)
                .Cascade(CascadeMode.Stop)
                .MustAsync(async (musicianProfileId, cancellation) =>
                    (await arpaContext.EntityExistsAsync<MusicianProfile>(
                        d => d.Id == musicianProfileId && d.PersonId == tokenAccessor.PersonId,
                        cancellation)))
                .WithErrorCode("404")
                .WithMessage("Musician Profile could not be found.")
                .MustAsync(async (musicianProfileId, cancellation) =>
                    !(await arpaContext.EntityExistsAsync<MusicianProfileDeactivation>(
                        d => d.MusicianProfileId == musicianProfileId,
                        cancellation)))
                .WithMessage("The musician profile is deactivated. A deactivated musician profile may not participate in a project");

        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Command, ProjectParticipation>()
                .ForMember(dest => dest.ParticipationStatusInnerId, opt => opt.MapFrom(src => src.ParticipationStatusInnerId))
                .ForMember(dest => dest.CommentByPerformerInner, opt => opt.MapFrom(src => src.CommentByPerformerInner))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }

    public class Handler : IRequestHandler<Command, ProjectParticipation>
    {
        private readonly IArpaContext _arpaContext;
        private readonly IMapper _mapper;

        public Handler(IArpaContext arpaContext, IMapper mapper)
        {
            _arpaContext = arpaContext;
            _mapper = mapper;
        }

        public async Task<ProjectParticipation> Handle(Command request, CancellationToken cancellationToken)
        {
            // Das MuPro muss einmal geladen werden, weil sonst die Property MusicianProfile des neuen Objekts null ist
            ProjectParticipation participation = (await _arpaContext.FindAsync<MusicianProfile>(new object[] { request.MusicianProfileId }, cancellationToken))
                .ProjectParticipations.FirstOrDefault(pp => pp.ProjectId == request.ProjectId);

            _mapper.Map(request, participation);
            participation = _arpaContext.ProjectParticipations.Update(participation).Entity;

            if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
            {
                return participation;
            }

            throw new Exception("Problem setting project participation");
        }
    }
}

