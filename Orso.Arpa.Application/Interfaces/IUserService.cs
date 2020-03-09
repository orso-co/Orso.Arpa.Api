using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Application.UserApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAsync();

        Task DeleteAsync(string userName);

        Task<MyProfileDto> GetProfileOfCurrentUserAsync();

        Task ModifyProfileOfCurrentUserAsync(MyProfileModifyDto userProfileModifyDto);

        Task<MyAppointmentListDto> GetAppointmentsOfCurrentUserAsync(int? limit, int? offset);

        Task SetAppointmentParticipationPredictionAsync(SetMyProjectAppointmentPredictionDto setParticipationPredictionDto);
    }
}
