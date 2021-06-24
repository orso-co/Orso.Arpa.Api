using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.MusicianProfileSections;

namespace Orso.Arpa.Application.Services
{
    public class DoublingInstrumentService
        : BaseService<DoublingInstrumentDto, MusicianProfileSection, DoublingInstrumentCreateDto, Create.Command, DoublingInstrumentModifyDto, DoublingInstrumentModifyBodyDto, Modify.Command>,
        IDoublingInstrumentService
    {
        public DoublingInstrumentService(MediatR.IMediator mediator, AutoMapper.IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}
