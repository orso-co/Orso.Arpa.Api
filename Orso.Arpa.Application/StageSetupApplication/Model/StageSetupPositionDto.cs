using System;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.MusicianProfileApplication.Model;
using Orso.Arpa.Application.PersonApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.StageSetupDomain.Model;

namespace Orso.Arpa.Application.StageSetupApplication.Model
{
    public class StageSetupPositionDto : BaseEntityDto
    {
        public Guid StageSetupId { get; set; }
        public Guid MusicianProfileId { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public int? Row { get; set; }
        public int? Stand { get; set; }

        /// <summary>
        /// Reduced musician profile info for display
        /// </summary>
        public ReducedMusicianProfileDto MusicianProfile { get; set; }
    }

    public class StageSetupPositionDtoMappingProfile : Profile
    {
        public StageSetupPositionDtoMappingProfile()
        {
            CreateMap<StageSetupPosition, StageSetupPositionDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>()
                .ForMember(dest => dest.MusicianProfile, opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    if (src.MusicianProfile == null) return null;

                    var mp = src.MusicianProfile;
                    return new ReducedMusicianProfileDto
                    {
                        Id = mp.Id,
                        InstrumentName = mp.Instrument?.Name,
                        Qualification = mp.Qualification?.SelectValue?.Name ?? string.Empty,
                        DoublingInstrumentNames = mp.DoublingInstruments?.Select(di => di.Section?.Name).Where(n => n != null).ToList() ?? [],
                        PreferredPositionsTeam = mp.PreferredPositionsTeam?.Select(p => p.SelectValueSection?.SelectValue?.Name).Where(n => n != null).ToList() ?? [],
                        Deactivation = mp.Deactivation != null ? context.Mapper.Map<MusicianProfileDeactivationApplication.Model.MusicianProfileDeactivationDto>(mp.Deactivation) : null,
                        Person = mp.Person != null ? new PersonDto
                        {
                            Id = mp.Person.Id,
                            DisplayName = mp.Person.DisplayName,
                            GivenName = mp.Person.GivenName,
                            Surname = mp.Person.Surname
                        } : null
                    };
                }));
        }
    }
}
