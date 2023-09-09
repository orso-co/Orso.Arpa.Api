using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Application.General.Services
{
    public abstract class BaseService<TGetDto, TEntity, TCreateDto, TCreateCommand, TModifyDto, TModifyBodyDto, TModifyCommand>
        : BaseCreateService<TGetDto, TEntity, TCreateDto, TCreateCommand>
        where TEntity : BaseEntity
        where TCreateCommand : ICreateCommand<TEntity>
        where TModifyDto : IdFromRouteDto<TModifyBodyDto>
        where TModifyCommand : IModifyCommand<TEntity>
    {
        protected BaseService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public virtual async Task ModifyAsync(TModifyDto modifyDto)
        {
            TModifyCommand command = _mapper.Map<TModifyCommand>(modifyDto);
            await _mediator.Send(command);
        }
    }
}
