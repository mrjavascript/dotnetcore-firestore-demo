using System.Collections.Generic;
using System.Threading.Tasks;
using Firestore.Demo.API.Models;

namespace Firestore.Demo.API.Repositories
{
    public interface IValuesRepository
    {
        Task<IEnumerable<Cafe>> GetValues();
        Task CreateValue(Cafe value);
    }
}