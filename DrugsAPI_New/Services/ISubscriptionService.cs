using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.DTO;
using DrugsAPI_New.Models;

namespace DrugsAPI_New.Services
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<RefillOrder>> GetRefillOrdersBySubscriptionIdAsync(int subscriptionId);
        Task<MemberSubscription> CreateSubscriptionAsync(MemberSubscription subscription);
        Task<MemberSubscription> GetSubscriptionByIdAsync(int id);
        Task<IEnumerable<MemberSubscription>> GetAllSubscriptionsAsync();
        Task<MemberSubscription> GetSubscriptionsByUserIdAsync(int userId);
        Task<MemberSubscription> UpdateSubscriptionAsync(MemberSubscription subscription);
        Task DeleteSubscriptionAsync(int id);
        Task<bool> IsSubscriptionActiveAsync(int id);
        Task<MemberSubscription> RenewSubscriptionAsync(int id);
        Task<MemberSubscription> CancelSubscriptionAsync(int id);
        Task<RefillOrder> GetLastRefillStatus(int subscriptionId);
        Task<RefillOrder> ProcessAdhocRefillAsync(AdhocRefillRequestDto request);
        Task<IEnumerable<MemberSubscription>> GetDueSubscriptions(int subscriptionId, DateTime fromDate);
    }
}

