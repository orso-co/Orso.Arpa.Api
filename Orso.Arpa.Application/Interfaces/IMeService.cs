using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.MyMusicianProfileApplication;
using Orso.Arpa.Application.ProjectApplication;
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
        Task<MyMusicianProfileDto> CreateMusicianProfileAsync(MyMusicianProfileCreateDto musicianProfileCreateDto);
        Task<MyMusicianProfileDto> GetMyMusicianProfileAsync(Guid id);
        Task<IEnumerable<MyMusicianProfileDto>> GetMyMusicianProfilesAsync(bool includeDeactivated);
        Task<ProjectParticipationDto> SetMyProjectParticipationAsync(SetMyProjectParticipationDto myProjectParticipationDto);
        Task<MyMusicianProfileDto> UpdateMusicianProfileAsync(MyMusicianProfileModifyDto musicianProfileModifyDto);
        Task<MyDoublingInstrumentDto> CreateDoublingInstrumentAsync(MyDoublingInstrumentCreateDto myDoublingInstrumentCreateDto);
        Task ModifyDoublingInstrumentAsync(MyDoublingInstrumentModifyDto myDoublingInstrumentModifyDto);
    }
}
