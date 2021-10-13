using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Addresses
{
    public static class Modify
    {
        public class Command : IModifyCommand<Address>
        {
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Zip { get; set; }
            public string City { get; set; }
            public string UrbanDistrict { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
            public string CommentInner { get; set; }
            public Guid? TypeId { get; set; }
            public Guid PersonId { get; set; }
            public Guid Id { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Address>()
                    .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
                    .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.Address2))
                    .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.Zip))
                    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                    .ForMember(dest => dest.UrbanDistrict, opt => opt.MapFrom(src => src.UrbanDistrict))
                    .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                    .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                    .ForMember(dest => dest.CommentInner, opt => opt.MapFrom(src => src.CommentInner))
                    .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .MustAsync(async (command, id, cancellation) => await arpaContext
                        .EntityExistsAsync<Address>(cd => cd.Id == id && cd.PersonId == cd.PersonId, cancellation))
                    .WithMessage("Address could not be found")
                    .WithErrorCode("404");

                RuleFor(c => c.PersonId)
                    .EntityExists<Command, Person>(arpaContext);

                RuleFor(c => c.TypeId)
                    .SelectValueMapping<Command, Address>(arpaContext, c => c.Type);
            }
        }
    }
}
