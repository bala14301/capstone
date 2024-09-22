using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models; // Add this line to import the Models namespace

namespace DrugsAPI_New.Repositories
{
    public interface IRefillRepository
    {
        Task<IEnumerable<RefillOrderLineItem>> GetAllRefillsAsync();
        Task<RefillOrder> GetRefillByIdAsync(int id);
        Task<RefillOrderLineItem> AddRefillAsync(RefillOrderLineItem refill);
        Task<RefillOrder> UpdateRefillAsync(int id, RefillOrder refill);
        Task<bool> DeleteRefillAsync(int id);
        Task<IEnumerable<RefillOrder>> GetRefillsForPrescriptionAsync(int prescriptionId);
    }
}
