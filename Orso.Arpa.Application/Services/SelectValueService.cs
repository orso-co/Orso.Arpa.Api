using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Logic.SelectValues;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.SelectValues;

namespace Orso.Arpa.Application.Services
{
    public class SelectValueService : ISelectValueService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SelectValueService(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<SelectValueDto>> GetAsync(string tableName, string propertyName)
        {
            IImmutableList<SelectValueMapping> selectValues = await _mediator
                .Send(new List.Query { TableName = tableName, PropertyName = propertyName });
            return _mapper.Map<IEnumerable<SelectValueDto>>(selectValues);
        }
    }
}
