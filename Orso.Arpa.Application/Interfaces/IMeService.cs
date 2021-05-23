using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.MyMusicianProfileApplication;
using Orso.Arpa.Domain.Logic.Me;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IMeService
    {
        Task<MyAppointmentListDto> GetMyAppointmentsAsync(int? limit, int? offset);
        Task<MyUserProfileDto> GetMyUserProfileAsync();
        Task ModifyMyUserProfileAsync(MyUserProfileModifyDto modifyDto);
        Task<SendQRCode.QrCodeFile> SendMyQrCodeAsync();
        Task SetMyAppointmentParticipationPredictionAsync(SetMyProjectAppointmentPredictionDto setParticipationPredictionDto);
        Task<MyMusicianProfileDto> CreateAsync(MyMusicianProfileCreateDto musicianProfileCreateDto);
        Task<MyMusicianProfileDto> GetMyMusicianProfileAsync(Guid id);
        Task<IEnumerable<MyMusicianProfileDto>> GetMyMusicianProfilesAsync();

    }
}
