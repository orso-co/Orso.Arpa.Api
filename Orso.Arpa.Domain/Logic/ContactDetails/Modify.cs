using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.ContactDetails
{
    public static class Modify
    {
        public class Command : IModifyCommand<ContactDetail>
        {
            public ContactDetailKey Key { get; set; }
            public string Value { get; set; }
            public Guid? TypeId { get; set; }
            public string CommentTeam { get; set; }
            public byte Preference { get; set; }
            public Guid PersonId { get; set; }
            public Guid Id { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, ContactDetail>()
                    .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                    .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                    .ForMember(dest => dest.CommentTeam, opt => opt.MapFrom(src => src.CommentTeam))
                    .ForMember(dest => dest.Preference, opt => opt.MapFrom(src => src.Preference))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .MustAsync(async (command, id, cancellation) => await arpaContext
                        .EntityExistsAsync<ContactDetail>(cd => cd.Id == id && cd.PersonId == cd.PersonId, cancellation))
                    .WithMessage("Contact Detail could not be found")
                    .WithErrorCode("404");

                RuleFor(c => c.PersonId)
                    .EntityExists<Command, Person>(arpaContext);

                RuleFor(c => c.TypeId)
                    .SelectValueMapping<Command, ContactDetail>(arpaContext, c => c.Type);
            }
        }
    }
}
