using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.ProjectDomain.Commands
{
    public static class RemoveRoleFromUrl
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

            public Guid UrlId { get; private set; }
            public Guid RoleId { get; private set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                _ = CreateMap<Command, UrlRole>()
                    .ForMember(dest => dest.UrlId, opt => opt.MapFrom(src => src.UrlId))
                    .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext, RoleManager<Role> roleManager)
            {
                _ = RuleFor(d => d.UrlId)
                       .EntityExists<Command, Url>(arpaContext);

                _ = RuleFor(d => d.RoleId)
                    .Cascade(CascadeMode.Stop)
                    .MustAsync(async (roleId, cancellation) => await roleManager.Roles.AnyAsync(r => r.Id == roleId, cancellation))
                    .WithErrorCode("404")
                    .WithMessage("Role could not be found.")

                   .MustAsync(async (dto, roleId, cancellation) => await arpaContext
                        .EntityExistsAsync<UrlRole>(ar => ar.RoleId == roleId && ar.UrlId == dto.UrlId, cancellation))
                    .WithMessage("The role is not linked to the url");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                UrlRole roleToRemove = await _arpaContext
                    .Set<UrlRole>()
                    .FirstOrDefaultAsync(ar => ar.RoleId == request.RoleId && ar.UrlId == request.UrlId, cancellationToken);

                _ = _arpaContext.Remove(roleToRemove);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _arpaContext.ClearChangeTracker();
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(UrlRole));
            }
        }
    }
}
