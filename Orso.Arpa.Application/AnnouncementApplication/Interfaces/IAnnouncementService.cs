using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.AnnouncementApplication.Model;

namespace Orso.Arpa.Application.AnnouncementApplication.Interfaces;

public interface IAnnouncementService
{
    Task<UnreadAnnouncementsDto> GetUnreadAsync();
    Task<List<AnnouncementDto>> GetTickerItemsAsync();
    Task<List<AnnouncementAdminDto>> GetAllAsync();
    Task<AnnouncementDto> CreateAsync(CreateAnnouncementDto dto);
    Task UpdateAsync(UpdateAnnouncementDto dto);
    Task DeleteAsync(Guid id);
    Task ToggleActiveAsync(Guid id);
    Task MarkAsReadAsync(Guid id);
    Task MarkAllAsReadAsync();
    Task ToggleTickerPinAsync(Guid id);
    Task<AnnouncementDto> CreateDeployAnnouncementAsync(DeployAnnouncementDto dto);
}
