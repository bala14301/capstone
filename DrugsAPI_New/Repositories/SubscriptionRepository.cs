using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Drugs_API.Data;
using DrugsAPI_New.Models;
using Microsoft.EntityFrameworkCore;

namespace DrugsAPI_New.Repositories
{
    public enum SubscriptionStatus
    {
        [EnumMember(Value = "Inactive")]
        Inactive,
        [EnumMember(Value = "Active")]
        Active
      
    }
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MemberSubscription>> GetAllSubscriptionsAsync()
        {
            return await _context.MemberSubscriptions.ToListAsync();
        }

        public async Task<MemberSubscription> GetByIdAsync(int id)
        {
            return await _context.MemberSubscriptions.FindAsync(id);
        }

        public async Task<MemberSubscription> CreateAsync(MemberSubscription subscription)
        {     subscription.SubscriptionDate = DateTime.Now;
            subscription.SubscriptionStatus = (int)SubscriptionStatus.Active;
            _context.MemberSubscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        public async Task<MemberSubscription> UpdateAsync(MemberSubscription subscription)
        {
            _context.Entry(subscription).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return subscription;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var subscription = await _context.MemberSubscriptions.FindAsync(id);
            if (subscription == null)
                return false;

            _context.MemberSubscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MemberSubscription> GetByUserIdAsync(int userId)
        {
            var subscription = await _context.MemberSubscriptions.FindAsync(userId) ?? new MemberSubscription();
            return subscription;

        }
    }
}
