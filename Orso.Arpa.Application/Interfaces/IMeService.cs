using System.Threading.Tasks;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Domain.Logic.Me;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IMeService
    {
        Task<MyAppointmentListDto> GetMyAppointmentsAsync(int? limit, int? offset);
        Task<MyProfileDto> GetMyProfileAsync();
        Task ModifyMyProfileAsync(MyProfileModifyDto userProfileModifyDto);
        Task<SendQRCode.QrCodeFile> SendMyQrCodeAsync();
        Task SetMyAppointmentParticipationPredictionAsync(SetMyProjectAppointmentPredictionDto setParticipationPredictionDto);
    }
}
