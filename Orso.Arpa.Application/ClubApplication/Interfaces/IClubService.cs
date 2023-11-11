using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.ClubApplication.Model;

namespace Orso.Arpa.Application.ClubApplication.Interfaces
{
    public interface IClubService
    {
        Task<ClubDto> GetAsync();
    }
}
