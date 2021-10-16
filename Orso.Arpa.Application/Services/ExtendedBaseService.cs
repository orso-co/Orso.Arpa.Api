using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.GenericHandlers.Create;
using static Orso.Arpa.Domain.GenericHandlers.Delete;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Application.Services
{
    public abstract class ExtendedBaseService<TGetDto, TEntity, TCreateDto, TCreateCommand, TModifyDto, TModifyBodyDto, TModifyCommand, TDeleteDto, TDeleteCommand> : BaseService<TGetDto, TEntity, TCreateDto, TCreateCommand, TModifyDto, TModifyBodyDto, TModifyCommand>
        where TEntity : BaseEntity
        where TCreateCommand : ICreateCommand<TEntity>
        where TModifyDto : IdFromRouteDto<TModifyBodyDto>
        where TModifyCommand : IModifyCommand<TEntity>
        where TDeleteCommand : IDeleteCommand<TEntity>
    {

        protected ExtendedBaseService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public virtual async Task DeleteAsync(TDeleteDto deleteDto)
        {
            TDeleteCommand command = _mapper.Map<TDeleteCommand>(deleteDto);
            await _mediator.Send(command);
        }
    }
}
