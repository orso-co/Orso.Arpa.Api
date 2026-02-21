using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Orso.Arpa.Application.TicketApplication.Model;

namespace Orso.Arpa.Application.TicketApplication.Interfaces
{
    public interface ITicketService
    {
        Task<List<TicketListItemDto>> GetTicketsAsync(string status, string type, Guid? creatorId, string search, CancellationToken cancellationToken = default);
        Task<TicketDto> GetTicketByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TicketDto> CreateTicketAsync(CreateTicketDto dto, List<(string fileName, string contentType, byte[] data)> files, CancellationToken cancellationToken = default);
        Task<TicketDto> UpdateTicketAsync(Guid id, UpdateTicketDto dto, CancellationToken cancellationToken = default);
        Task DeleteTicketAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TicketMessageDto> AddMessageAsync(Guid ticketId, string content, List<(string fileName, string contentType, byte[] data)> files, CancellationToken cancellationToken = default);
        Task<TicketListItemDto> VoteAsync(Guid ticketId, int value, CancellationToken cancellationToken = default);
        Task<List<TicketReactionDto>> ToggleReactionAsync(Guid messageId, string emoji, CancellationToken cancellationToken = default);
        Task<TicketLinkDto> AddLinkAsync(Guid ticketId, CreateTicketLinkDto dto, CancellationToken cancellationToken = default);
        Task RemoveLinkAsync(Guid linkId, CancellationToken cancellationToken = default);
        Task MarkAsReadAsync(Guid ticketId, CancellationToken cancellationToken = default);
        Task<TicketStatsDto> GetStatsAsync(CancellationToken cancellationToken = default);
        Task<int> GetUnreadCountAsync(CancellationToken cancellationToken = default);
        Task<(byte[] content, string fileName, string contentType)?> GetAttachmentFileAsync(Guid attachmentId, CancellationToken cancellationToken = default);
    }
}
