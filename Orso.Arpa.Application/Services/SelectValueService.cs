using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.SelectValueApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.SelectValues;

namespace Orso.Arpa.Application.Services
{
    public class SelectValueService : BaseService<SelectValueDto, SelectValue, SelectValueCreateDto, Create.Command, SelectValueModifyDto, SelectValueModifyBodyDto, Modify.Command>, ISelectValueService
    {
        public SelectValueService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<IEnumerable<SelectValueDto>> GetAsync(string tableName, string propertyName)
        {
            IImmutableList<SelectValueMapping> selectValues = await _mediator
                .Send(new List.Query { TableName = tableName, PropertyName = propertyName });
            return _mapper.Map<IEnumerable<SelectValueDto>>(selectValues);
        }
    }
}
