using Orso.Arpa.Domain;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(User user);

    }
}
