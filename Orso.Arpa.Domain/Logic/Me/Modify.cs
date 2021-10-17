using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Identity;
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
            public string AboutMe { get; set; }
            public Guid GenderId { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public string Birthplace { get; set; }
            public string BirthName { get; set; }
        }

        public class MappingProfile : AutoMapper.Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, User>()
                    .ForPath(dest => dest.Person.GivenName, opt => opt.MapFrom(src => src.GivenName))
                    .ForPath(dest => dest.Person.Surname, opt => opt.MapFrom(src => src.Surname))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                    .ForPath(dest => dest.Person.AboutMe, opt => opt.MapFrom(src => src.AboutMe))
                    .ForPath(dest => dest.Person.GenderId, opt => opt.MapFrom(src => src.GenderId))
                    .ForPath(dest => dest.Person.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                    .ForPath(dest => dest.Person.Birthplace, opt => opt.MapFrom(src => src.Birthplace))
                    .ForPath(dest => dest.Person.BirthName, opt => opt.MapFrom(src => src.BirthName))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(ArpaUserManager userManager, IUserAccessor userAccessor, IArpaContext arpaContext)
            {
                RuleFor(c => c.Email)
                    .MustAsync(async (email, cancellation) =>
                    {
                        User user = await userManager.FindByEmailAsync(email);
                        return user == null || userAccessor.UserName == user.UserName;
                    })
                    .WithMessage("Email aleady exists");

                RuleFor(c => c.GenderId)
                    .SelectValueMapping<Command, Person>(arpaContext, p => p.Gender);
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMapper _mapper;
            private readonly ArpaUserManager _userManager;
            private readonly IUserAccessor _userAccessor;

            public Handler(
                ArpaUserManager userManager,
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
