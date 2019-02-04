using System.Collections.Generic;
using System.Threading.Tasks;
using Firestore.Demo.API.Models;

namespace Firestore.Demo.API.Services
{
    public interface IValuesService
    {
        Task<IEnumerable<Cafe>> GetValues();
        Task CreateValue(Cafe value);
    }
}