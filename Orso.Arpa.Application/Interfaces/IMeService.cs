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
        Task<MyAppointmentListDto> GetMyAppointmentsAsync(int? limit, int? offset, bool passed);
        Task<MyUserProfileDto> GetMyUserProfileAsync();
        Task ModifyMyUserProfileAsync(MyUserProfileModifyDto modifyDto);
        Task<SendQRCode.QrCodeFile> GetMyQrCodeAsync(bool sendEmail);
        Task SetMyAppointmentParticipationPredictionAsync(SetMyProjectAppointmentPredictionDto setParticipationPredictionDto);
        Task<MyMusicianProfileDto> CreateMusicianProfileAsync(MyMusicianProfileCreateDto musicianProfileCreateDto);
        Task<MyMusicianProfileDto> GetMyMusicianProfileAsync(Guid id);
        Task<IEnumerable<MyMusicianProfileDto>> GetMyMusicianProfilesAsync(bool includeDeactivated);
        Task<MyMusicianProfileDto> UpdateMusicianProfileAsync(MyMusicianProfileModifyDto musicianProfileModifyDto);
        Task<MyDoublingInstrumentDto> CreateDoublingInstrumentAsync(MyDoublingInstrumentCreateDto myDoublingInstrumentCreateDto);
        Task ModifyDoublingInstrumentAsync(MyDoublingInstrumentModifyDto myDoublingInstrumentModifyDto);
        Task AddDocumentToMusicianProfileAsync(MyMusicianProfileAddDocumentDto addDocumentDto);
        Task RemoveDocumentFromMusicianProfileAsync(MyMusicianProfileRemoveDocumentDto removeDocumentDto);
        Task<RegionPreferenceDto> CreateRegionPreferenceAsync(MyRegionPreferenceCreateDto myRegionPreferenceCreateDto);
        Task ModifyRegionPreferenceAsync(MyRegionPreferenceModifyDto myRegionPreferenceModifyDto);
        Task RemoveRegionPreferenceAsync(MyRegionPreferenceRemoveDto myRegionPreferenceRemoveDto);
    }
}
