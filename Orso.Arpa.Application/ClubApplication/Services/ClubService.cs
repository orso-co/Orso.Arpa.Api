using System.Threading.Tasks;
using AutoMapper;
using Orso.Arpa.Application.ClubApplication.Interfaces;
using Orso.Arpa.Application.ClubApplication.Model;
using Orso.Arpa.Domain.General.Configuration;

namespace Orso.Arpa.Application.ClubApplication.Services
{
    public class ClubService : IClubService
    {
        private readonly IMapper _mapper;
        private readonly ClubConfiguration _clubConfiguration;


        public ClubService(IMapper mapper, ClubConfiguration clubConfiguration)
        {
            _mapper = mapper;
            _clubConfiguration = clubConfiguration;
        }

        public Task<ClubDto> GetAsync()
        {
            return Task.FromResult(_mapper.Map<ClubDto>(_clubConfiguration));
        }
    }
}
