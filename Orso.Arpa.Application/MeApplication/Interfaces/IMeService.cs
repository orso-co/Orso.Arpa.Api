using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.MeApplication.Model;
using Orso.Arpa.Application.MusicianProfileApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Commands;

namespace Orso.Arpa.Application.MeApplication.Interfaces
{
    public interface IMeService
    {
        Task<MyAppointmentListDto> GetMyAppointmentsAsync(int? limit, int? offset, bool passed);
        Task<IList<MyAppointmentDto>> GetAppointmentRangeAsync(DateTime? dateTime, DateRange dateRange);
        Task<MyUserProfileDto> GetMyUserProfileAsync();
        Task ModifyMyUserProfileAsync(MyUserProfileModifyDto modifyDto);
        Task<SendMyQRCode.QrCodeFile> GetMyQrCodeAsync(bool sendEmail);
        Task SetMyAppointmentParticipationPredictionAsync(SetMyAppointmentParticipationPredictionDto setParticipationPredictionDto);
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
