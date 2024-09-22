using Drugs_API.DTO;
using DrugsAPI_New.Models;
using DrugsAPI_New.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace DrugsAPI_New.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost("subscribe")]
        [Authorize]
        public async Task<IActionResult> Subscribe([FromBody] MemberSubscription request)
        {
            try
            {
                var result = await _subscriptionService.CreateSubscriptionAsync(request);
                return Ok(new { Status = "Success Susbscription", Description = "Added the Susbscription successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Description = "An error occurred while processing the subscription request." });
            }
        }

        [HttpPost("unsubscribe")]
        [Authorize]
        public async Task<IActionResult> Unsubscribe([FromBody] UnsubscriptionRequest request)
        {
            try
            {
                var result = await _subscriptionService.CancelSubscriptionAsync(request.SubscriptionId);
                if (result != null)
                {
                    return Ok(new { Message = "Subscription successfully cancelled", CancelledDate = result.EndDate });
                }
                else
                {
                    return NotFound(new { Message = "Subscription not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing the unsubscription request." });
            }
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            try
            {
                var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
                return Ok(subscriptions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving subscriptions." });
            }
        }
    }
}
