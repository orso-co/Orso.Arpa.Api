using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.Interfaces
{
    public interface ISelectValueService
    {
        Task<IEnumerable<SelectValueDto>> GetAsync(string tableName, string propertyName);
    }
}
