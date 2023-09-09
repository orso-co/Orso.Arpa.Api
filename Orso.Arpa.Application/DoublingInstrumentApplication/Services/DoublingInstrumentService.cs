
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.DoublingInstrumentApplication.Interfaces;
using Orso.Arpa.Application.DoublingInstrumentApplication.Model;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Application.DoublingInstrumentApplication.Services
{
    public class DoublingInstrumentService
        : BaseService<
            DoublingInstrumentDto,
            MusicianProfileSection,
            DoublingInstrumentCreateDto,
            CreateMusicianProfileSection.Command,
            DoublingInstrumentModifyDto,
            DoublingInstrumentModifyBodyDto,
            ModifyMusicianProfileSection.Command>,
        IDoublingInstrumentService
    {
        public DoublingInstrumentService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}
