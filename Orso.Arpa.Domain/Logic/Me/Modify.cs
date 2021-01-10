using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class Modify
    {
        public class Command : IRequest
        {
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string GivenName { get; set; }
            public string Surname { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, User>()
                    .ForPath(dest => dest.Person.GivenName, opt => opt.MapFrom(src => src.GivenName))
                    .ForPath(dest => dest.Person.Surname, opt => opt.MapFrom(src => src.Surname))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(UserManager<User> userManager, IUserAccessor userAccessor)
            {
                
                RuleFor(c => c.Email)
                    .MustAsync(async (dto, email, cancellation) =>
                    {
                        User user = await userManager.FindByEmailAsync(email);
                        return user == null || userAccessor.UserName == user.UserName;
                    })
                    .WithMessage("Email aleady exists");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMapper _mapper;
            private readonly UserManager<User> _userManager;
            private readonly IUserAccessor _userAccessor;

            public Handler(
                UserManager<User> userManager,
                IUserAccessor userAccessor,
                IMapper mapper)
            {
                _mapper = mapper;
                _userManager = userManager;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User existingUser = await _userAccessor.GetCurrentUserAsync();

                User modifiedUser = _mapper.Map(request, existingUser);

                IdentityResult result = await _userManager.UpdateAsync(modifiedUser);

                if (result.Succeeded)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem updating user");
            }
        }
    }
}
