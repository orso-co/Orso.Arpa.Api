using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class ModifyDoublingInstrument
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid MusicianProfileId { get; set; }
            public byte LevelAssessmentInner { get; set; }
            public Guid? AvailabilityId { get; set; }
            public string Comment { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext, ITokenAccessor tokenAccessor)
            {
                RuleFor(c => c.MusicianProfileId)
                    .MustAsync(async (musicianProfileId, cancellation) => await arpaContext
                        .EntityExistsAsync<MusicianProfile>(mp => mp.Id == musicianProfileId && mp.PersonId == tokenAccessor.PersonId, cancellation))
                    .OnFailure(_ => throw new AuthorizationException("This musician profile is not yours. You don't have access to this musician profile."));

                RuleFor(c => c.Id)
                    .MustAsync(async (command, id, cancellation) => await arpaContext
                        .EntityExistsAsync<MusicianProfileSection>(s => s.Id == id && s.MusicianProfileId == command.MusicianProfileId, cancellation))
                    .WithMessage("Doubling instrument with this id and the supplied musician profile does not exist");

                RuleFor(c => c.AvailabilityId)
                    .SelectValueMapping<Command, MusicianProfileSection>(arpaContext, a => a.InstrumentAvailability);
            }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, MusicianProfileSection>()
                    .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                    .ForMember(dest => dest.InstrumentAvailabilityId, opt => opt.MapFrom(src => src.AvailabilityId))
                    .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.LevelAssessmentInner))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;
            private readonly IMapper _mapper;

            public Handler(IArpaContext arpaContext, IMapper mapper)
            {
                _arpaContext = arpaContext;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                MusicianProfileSection existingEntity = await _arpaContext.GetByIdAsync<MusicianProfileSection>(request.Id, cancellationToken);

                MusicianProfileSection modifiedEntity = _mapper.Map(request, existingEntity);

                _arpaContext.Entry(existingEntity)?.CurrentValues?.SetValues(modifiedEntity);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception($"Problem updating doubling instrument");
            }
        }
    }
}
