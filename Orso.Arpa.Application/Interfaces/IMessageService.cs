using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.MessageApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto> CreateAsync(MessageCreateDto createDto);
        Task<MessageDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MessageDto>> GetAsync();
        Task ModifyAsync(MessageModifyDto modifyDto);
        Task DeleteAsync(Guid id);
    }
}
