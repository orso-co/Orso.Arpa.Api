using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;
using static Orso.Arpa.Domain.General.GenericHandlers.Delete;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Application.General.Services
{
    public abstract class ExtendedBaseService<TGetDto, TEntity, TCreateDto, TCreateCommand, TModifyDto, TModifyBodyDto, TModifyCommand, TDeleteDto, TDeleteCommand>
        : BaseService<TGetDto, TEntity, TCreateDto, TCreateCommand, TModifyDto, TModifyBodyDto, TModifyCommand>
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
