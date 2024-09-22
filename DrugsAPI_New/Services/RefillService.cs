using System;
using System.Threading.Tasks;
using DrugsAPI_New.Models;
using DrugsAPI_New.Repositories;

namespace DrugsAPI_New.Services
{
    public class RefillService : IRefillService
    {
        private readonly IRefillRepository _refillRepository;

        public RefillService(IRefillRepository refillRepository)
        {
            _refillRepository = refillRepository ?? throw new ArgumentNullException(nameof(refillRepository));
        }

        public async Task<RefillOrderLineItem> CreateRefillAsync(RefillOrderLineItem refill)
        {
            return await _refillRepository.AddRefillAsync(refill);
        }

        public async Task<RefillOrder> GetRefillByIdAsync(int prescriptionId)
        {
            return await _refillRepository.GetRefillByIdAsync(prescriptionId);
        }

        public async Task<RefillOrder> UpdateRefillAsync(RefillOrder refill)
        {
            return await _refillRepository.UpdateRefillAsync(refill.Id, refill);
        }

        public async Task DeleteRefillAsync(int id)
        {
            await _refillRepository.DeleteRefillAsync(id);
        }

        public async Task<RefillOrder> GetRefillsForPrescriptionAsync(int prescriptionId)
        {
            return (RefillOrder)await _refillRepository.GetRefillsForPrescriptionAsync(prescriptionId);
        }

    public async Task<IEnumerable<RefillOrderLineItem>> GetAllRefillsAsync()
    {
        return await _refillRepository.GetAllRefillsAsync();
    }

    }
}
