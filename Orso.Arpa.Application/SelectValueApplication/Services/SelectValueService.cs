using System;
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

        public async Task<SelectValueDto> CreateMappingAsync(SelectValueMappingCreateDto createDto)
        {
            var command = new CreateSelectValueMapping.Command
            {
                TableName = createDto.TableName,
                PropertyName = createDto.PropertyName,
                Name = createDto.Name,
                Description = createDto.Description
            };

            SelectValueMapping createdMapping = await _mediator.Send(command);
            return _mapper.Map<SelectValueDto>(createdMapping);
        }

        public async Task ModifyMappingAsync(SelectValueMappingModifyDto modifyDto)
        {
            var command = new ModifySelectValueMapping.Command
            {
                Id = modifyDto.Id,
                TableName = modifyDto.TableName,
                PropertyName = modifyDto.PropertyName,
                Name = modifyDto.Body.Name,
                Description = modifyDto.Body.Description
            };

            await _mediator.Send(command);
        }

        public async Task DeleteMappingAsync(Guid id)
        {
            await _mediator.Send(new DeleteSelectValueMapping.Command { Id = id });
        }
    }
}
