using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;

namespace DrugsAPI_New.Repositories
{
    public interface IDrugRepository
    {
        Task<IEnumerable<Drug>> GetAllAsync();
         Task<List<Drug>> CreateMultipleDrugsAsync(List<Drug> drugs);
        Task<Drug> GetByIdAsync(int id);
        Task<Drug> CreateAsync(Drug drug);
        Task<Drug> UpdateAsync(int id, Drug drug);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Drug>> CheckAvailabilityAsync(string location);
    }
}
