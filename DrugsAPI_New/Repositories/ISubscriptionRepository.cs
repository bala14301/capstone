using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;

namespace DrugsAPI_New.Repositories
{
    
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<MemberSubscription>> GetAllSubscriptionsAsync();
        Task<MemberSubscription> GetByIdAsync(int id);
        Task<MemberSubscription> CreateAsync(MemberSubscription subscription);
        Task<MemberSubscription> UpdateAsync(MemberSubscription subscription);
        Task<bool> DeleteAsync(int id);
        Task<MemberSubscription> GetByUserIdAsync(int userId);
    }
}