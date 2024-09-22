using System.Collections.Generic;
using System.Threading.Tasks;
using Drugs_API.Data;
using DrugsAPI_New.Models;
using Microsoft.EntityFrameworkCore;

namespace DrugsAPI_New.Repositories
{
    public class RefillRepository : IRefillRepository
    {
        private readonly ApplicationDbContext _context;

        public RefillRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RefillOrderLineItem>> GetAllRefillsAsync()
        {
            return await _context.RefillOrderLineItems.ToListAsync();
        }
        public async Task<IEnumerable<RefillOrder>> GetRefillsByPrescriptionIdAsync(int SubscriptionId)
        {
            return await _context.RefillOrders
                .Where(r => r.SubscriptionId == SubscriptionId)
                .ToListAsync();
        }

        public async Task<RefillOrderLineItem> AddRefillAsync(RefillOrderLineItem refill)
        {
            _context.RefillOrderLineItems.Add(refill);
            await _context.SaveChangesAsync();
            return refill;
        }

        public async Task<RefillOrder> UpdateRefillAsync(RefillOrder refill)
        {
            _context.Entry(refill).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return refill;
        }

        public async Task<bool> DeleteRefillAsync(int id)
        {
            var refill = await _context.RefillOrders.FindAsync(id);
            if (refill == null)
                return false;

            _context.RefillOrders.Remove(refill);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<RefillOrder> GetRefillByIdAsync(int id)
        {
            return await _context.RefillOrders.FindAsync(id);
        }

        public async Task<RefillOrder> UpdateRefillAsync(int id, RefillOrder refill)
        {
            var existingRefill = await _context.RefillOrders.FindAsync(id);
            if (existingRefill == null)
            {
                return null;
            }

          
            _context.Entry(existingRefill).CurrentValues.SetValues(refill);

            await _context.SaveChangesAsync();
            return existingRefill;
        }

    public async Task<IEnumerable<RefillOrder>> GetRefillsForPrescriptionAsync(int prescriptionId)
    {
        return await _context.RefillOrders
            .Where(r => r.RefillOrderItemId == prescriptionId)
            .ToListAsync();
    }
    }
}
