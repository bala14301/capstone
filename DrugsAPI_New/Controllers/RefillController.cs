using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DrugsAPI_New.Services;
using DrugsAPI_New.Models;
using DrugsAPI_New.DTO;

namespace DrugsAPI_New.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefillController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IDrugService _drugService;

        private readonly IRefillService _refillService;

        public RefillController(ISubscriptionService subscriptionService, IDrugService drugService, IRefillService refillService)
        {
            _subscriptionService = subscriptionService;
            _drugService = drugService;
            _refillService = refillService;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestRefill([FromBody] RefillOrderLineItem request)
        {
            await _refillService.CreateRefillAsync(request);
            return Ok(new { Message = "Refill request processed" });
        }

        [HttpGet("viewRefillStatus")]
        public async Task<IActionResult> ViewRefillStatus([FromQuery] int subscriptionId)
        {
            var lastRefillStatus = await _subscriptionService.GetLastRefillStatus(subscriptionId);
            return Ok(lastRefillStatus);
        }

        [HttpGet("getRefillDuesAsOfDate")]
        public async Task<IActionResult> GetRefillDuesAsOfDate([FromQuery] int subscriptionId, [FromQuery] DateTime fromDate)
        {
            var dueSubscriptions = await _subscriptionService.GetDueSubscriptions(subscriptionId, fromDate);
            return Ok(dueSubscriptions);
        }

        [HttpGet("getAllRefills")]
        public async Task<ActionResult<IEnumerable<RefillOrderLineItem>>> GetAllRefills()
        {
            try
            {
                var refills = await _refillService.GetAllRefillsAsync();
                if (refills == null || !refills.Any())
                {
                    return NotFound("No refills found.");
                }
                return Ok(refills);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving refills.");
            }
        }

        [HttpPost("requestAdhocRefill")]
        public async Task<IActionResult> RequestAdhocRefill([FromBody] AdhocRefillRequestDto request)
        {
            if (request == null || request.MemberId<=0 || request.PolicyId<=0 || request.SubscriptionId<=0 ||  string.IsNullOrEmpty(request.Location))
            {
                return BadRequest(new { Message = "Invalid request. All fields (Policy_ID, Member_ID, Subscription_ID, and Location) are required." });
            }

            var isSubscriptionValid = await _subscriptionService.IsSubscriptionActiveAsync(request.SubscriptionId);
            if (!isSubscriptionValid)
            {
                return BadRequest(new { Message = "Invalid or inactive subscription." });
            }

            var drugsAvailable = await _drugService.CheckDrugAvailability(request.Location);
            if (drugsAvailable==null)
            {
                return BadRequest(new { Message = "Refill request failed. Requested drugs are not available at the specified location." });
            }

            var refillOrder = await _subscriptionService.ProcessAdhocRefillAsync(request);
            
            if (refillOrder == null)
            {
                return StatusCode(500, new { Message = "An error occurred while processing the refill request." });
            }

            return Ok(refillOrder);
        }
    }
}
