using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Interfaces;

namespace Orso.Arpa.Application.Services
{
    public class SelectValueService : ISelectValueService
    {
        public Task<IEnumerable<SelectValueDto>> GetAsync(string tableName, string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
