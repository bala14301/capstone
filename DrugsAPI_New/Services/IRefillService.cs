using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;

namespace DrugsAPI_New.Services
{
    public interface IRefillService
    {
        Task<RefillOrderLineItem> CreateRefillAsync(RefillOrderLineItem refill);
        Task<RefillOrder> GetRefillByIdAsync(int id);
        Task<RefillOrder> GetRefillsForPrescriptionAsync(int prescriptionId);
        Task<RefillOrder> UpdateRefillAsync(RefillOrder refill);
        Task DeleteRefillAsync(int id);
        Task<IEnumerable<RefillOrderLineItem>> GetAllRefillsAsync();
    }
}