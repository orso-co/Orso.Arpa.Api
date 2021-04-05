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

namespace Orso.Arpa.Domain.Logic.Urls
{
    public static class AddRole
    {
        public class Command : IRequest
        {
            public Command(Guid id, Guid roleId)
            {
                Id = id;
                RoleId = roleId;
            }

            public Command()
            {
            }

            public Guid Id { get; private set; }
            public Guid RoleId { get; private set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, UrlRole>()
                    .ForMember(dest => dest.UrlId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext, RoleManager<Role> roleManager)
            {
                RuleFor(d => d.Id)
                    .EntityExists<Command, Url>(arpaContext);

                RuleFor(d => d.RoleId)
                    .MustAsync(async (roleId, cancellation) => await roleManager.Roles.AnyAsync(r => r.Id == roleId, cancellation))
                    .WithMessage("The role could not be found.")

                    .MustAsync(async (dto, roleId, cancellation) => !(await arpaContext.UrlRoles
                        .AnyAsync(ar => ar.RoleId == roleId && ar.UrlId == dto.Id, cancellation)))
                    .WithMessage("The role is already linked to the url");
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
                Url existingUrl = await _arpaContext.Urls.FindAsync(new object[] { request.Id }, cancellationToken);
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
