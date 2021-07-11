using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Domain.Logic.Urls
{
    public static class AddRole
    {
        public class Command : IRequest
        {
            public Command(Guid urlId, Guid roleId)
            {
                UrlId = urlId;
                RoleId = roleId;
            }

            public Command()
            {
            }

            public Guid UrlId { get; set; }
            public Guid RoleId { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, UrlRole>()
                    .ForMember(dest => dest.UrlId, opt => opt.MapFrom(src => src.UrlId))
                    .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext, RoleManager<Role> roleManager)
            {
                RuleFor(d => d.UrlId)
                    .EntityExists<Command, Url>(arpaContext);

                RuleFor(d => d.RoleId)
                    .Cascade(CascadeMode.Stop)
                    .MustAsync(async (roleId, cancellation) => await roleManager.Roles.AnyAsync(r => r.Id == roleId, cancellation))
                    .WithErrorCode("404")
                    .WithMessage("Role could not be found.")

                    .MustAsync(async (dto, roleId, cancellation) => !(await arpaContext.EntityExistsAsync<UrlRole>(ar => ar.RoleId == roleId && ar.UrlId == dto.UrlId, cancellation)))
                    .WithMessage("The role is already linked to the url")

                    .MustAsync(async (roleId, cancellation) => !await roleManager.Roles.AnyAsync(r => r.Id == roleId && r.Name == RoleNames.Admin, cancellation))
                    .WithMessage("Url visibility is not used for the admin role.");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;
            private readonly RoleManager<Role> _roleManager;

            public Handler(
                IArpaContext arpaContext,
                RoleManager<Role> roleManager)
            {
                _arpaContext = arpaContext;
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Url existingUrl = await _arpaContext.Urls.FindAsync(new object[] { request.UrlId }, cancellationToken);
                Role existingRole = await _roleManager.Roles.SingleAsync(r => r.Id == request.RoleId, cancellationToken);

                var urlRole = new UrlRole(null, existingUrl, existingRole);

                _arpaContext.UrlRoles.Add(urlRole);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem adding role to url");
            }
        }
    }
}
