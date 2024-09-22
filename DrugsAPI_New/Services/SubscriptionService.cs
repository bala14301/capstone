using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugsAPI_New.DTO;
using DrugsAPI_New.DTOs;
using DrugsAPI_New.Models;
using DrugsAPI_New.Repositories;
using Microsoft.EntityFrameworkCore;
using Drugs_API.Data;
using System.Runtime.Serialization;

namespace DrugsAPI_New.Services
{
    public enum SubscriptionStatus
    {
        [EnumMember(Value = "Inactive")]
        Inactive,
        [EnumMember(Value = "Active")]
        Active
    }
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ApplicationDbContext _context;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, ApplicationDbContext context)
        {
            _subscriptionRepository = subscriptionRepository;
            _context = context;
        }

        public async Task<MemberSubscription> CreateSubscriptionAsync(MemberSubscription subscription)
        {
            return await _subscriptionRepository.CreateAsync(subscription);
        }

        public async Task<MemberSubscription> GetSubscriptionByIdAsync(int id)
        {
            return await _subscriptionRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<MemberSubscription>> GetAllSubscriptionsAsync()
        {
            return await _subscriptionRepository.GetAllSubscriptionsAsync();
        }

        public async Task<MemberSubscription> GetSubscriptionsByUserIdAsync(int userId)
        {
            return await _subscriptionRepository.GetByUserIdAsync(userId);
        }

        public async Task<MemberSubscription> UpdateSubscriptionAsync(MemberSubscription subscription)
        {
            return await _subscriptionRepository.UpdateAsync(subscription);
        }

        public async Task DeleteSubscriptionAsync(int id)
        {
            await _subscriptionRepository.DeleteAsync(id);
        }

        public async Task<bool> IsSubscriptionActiveAsync(int id)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(id);
            return subscription != null && subscription.EndDate > DateTime.UtcNow;
        }

        public async Task<MemberSubscription> RenewSubscriptionAsync(int id)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(id);
            if (subscription != null)
            {
                subscription.StartDate = DateTime.UtcNow;
                subscription.EndDate = subscription.StartDate.AddMonths(1);
                await _subscriptionRepository.UpdateAsync(subscription);
            }
            return subscription;
        }

        public async Task<MemberSubscription> CancelSubscriptionAsync(int id)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(id);
            if (subscription != null)
            {
                subscription.EndDate = DateTime.UtcNow;
                subscription.SubscriptionStatus=(int)SubscriptionStatus.Inactive;
                await _subscriptionRepository.UpdateAsync(subscription);
            }
            return subscription;
        }

        public async Task<RefillResultDto> ProcessAdhocRefillAsync(RefillOrder request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                bool isSubscriptionActive = await IsSubscriptionActiveAsync(request.SubscriptionId);
                if (!isSubscriptionActive)
                {
                    return new RefillResultDto
                    {
                        Success = false,
                        Message = "Subscription is not active"
                    };
                }

                bool isRefillAllowed = await CheckRefillAllowed(request);
                if (!isRefillAllowed)
                {
                    return new RefillResultDto
                    {
                        Success = false,
                        Message = "Refill is not allowed at this time"
                    };
                }

                await ProcessRefill(request);

                return new RefillResultDto
                {
                    Success = true,
                    Message = "Adhoc refill processed successfully"
                };
            }
            catch (Exception ex)
            {
                return new RefillResultDto
                {
                    Success = false,
                    Message = $"Error processing adhoc refill: {ex.Message}"
                };
            }
        }

        public async Task<IEnumerable<RefillOrder>> GetRefillOrdersBySubscriptionIdAsync(int subscriptionId)
        {
            return await _context.RefillOrders
                .Where(ro => ro.SubscriptionId == subscriptionId)
                .OrderByDescending(ro => ro.OrderDate)
                .ToListAsync();
        }

        private async Task<bool> CheckRefillAllowed(RefillOrder request)
        {
            var existingRefillOrder = await _context.RefillOrders
                .Where(ro => ro.SubscriptionId == request.SubscriptionId && ro.EndDate > DateTime.Today)
                .FirstOrDefaultAsync();

            return existingRefillOrder == null;
        }

        private async Task<RefillOrder> ProcessRefill(RefillOrder request)
        {
            var refillOrder = new RefillOrder
            {
                SubscriptionId = request.SubscriptionId,
                QuantityStatus=request.QuantityStatus,
                RefillOrderItemId = request.RefillOrderItemId,
                StartDate = request.StartDate,
                EndDate = DateTime.Now.AddDays(30),
                RefillDate = request.RefillDate
            };

            _context.RefillOrders.Add(refillOrder);
            await _context.SaveChangesAsync();

            return refillOrder;
        }

        public async Task<RefillOrder> GetLastRefillStatus(int subscriptionId)
        {
            var refillOrders = await GetRefillOrdersBySubscriptionIdAsync(subscriptionId);
            return refillOrders.OrderByDescending(ro => ro.OrderDate).FirstOrDefault();
        }

        public async Task<IEnumerable<MemberSubscription>> GetDueSubscriptions(int subscriptionId, DateTime fromDate)
        {
            return await _context.MemberSubscriptions
                .Where(s => s.Id == subscriptionId &&
                            s.StartDate >= fromDate &&
                            
                            s.SubscriptionStatus == (int)SubscriptionStatus.Active)
                .ToListAsync();
        }

        public async Task<RefillOrder> ProcessAdhocRefillAsync(AdhocRefillRequestDto request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var isSubscriptionActive = await IsSubscriptionActiveAsync(request.SubscriptionId);
                if (!isSubscriptionActive)
                {
                    throw new InvalidOperationException("Subscription is not active");
                }

                var refillOrder = new RefillOrder
                {
                    SubscriptionId = request.SubscriptionId,
                    RefillDate = DateTime.UtcNow,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                    OrderDate = DateTime.UtcNow,
                    QuantityStatus = "Pending"
                };

                _context.RefillOrders.Add(refillOrder);

                foreach (var drug in request.Drugs)
                {
                    var lineItem = new RefillOrderLineItem
                    {
                        SubscriptionId = request.SubscriptionId,
                        RefillOrderId = refillOrder.Id,
                        DrugId = drug.Id,
                        Quantity = drug.TotalQuantity
                    };
                    _context.RefillOrderLineItems.Add(lineItem);
                }

                await _context.SaveChangesAsync();

                return refillOrder;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Invalid request: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Subscription error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing adhoc refill: {ex.Message}");
            }
        }
    }
}
