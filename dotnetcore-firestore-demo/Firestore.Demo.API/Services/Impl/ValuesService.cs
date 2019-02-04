using System.Collections.Generic;
using System.Threading.Tasks;
using Firestore.Demo.API.Models;
using Firestore.Demo.API.Repositories;

namespace Firestore.Demo.API.Services.Impl
{
    public class ValuesService : IValuesService
    {
        private readonly IValuesRepository _valuesRepository;

        public ValuesService(IValuesRepository valuesRepository)
        {
            _valuesRepository = valuesRepository;
        }

        public async Task<IEnumerable<Cafe>> GetValues()
        {
            return await _valuesRepository.GetValues();
        }
    }
}