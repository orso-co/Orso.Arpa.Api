using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.SelectValueApplication.Interfaces;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Domain.SelectValueDomain.Commands;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Queries;

namespace Orso.Arpa.Application.SelectValueApplication.Services
{
    public class SelectValueService : BaseService<
        SelectValueDto,
        SelectValue,
        SelectValueCreateDto,
        CreateSelectValue.Command,
        SelectValueModifyDto,
        SelectValueModifyBodyDto,
        ModifySelectValue.Command>, ISelectValueService
    {
        public SelectValueService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<IEnumerable<SelectValueDto>> GetAsync(string tableName, string propertyName)
        {
            IImmutableList<SelectValueMapping> selectValues = await _mediator
                .Send(new ListSelectValues.Query { TableName = tableName, PropertyName = propertyName });
            return _mapper.Map<IEnumerable<SelectValueDto>>(selectValues);
        }
    }
}
