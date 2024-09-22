using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Controllers;
using DrugsAPI_New.Models;
using DrugsAPI_New.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class SubscriptionControllerTests
    {
        private Mock<ISubscriptionService> _mockService;
        private SubscriptionController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<ISubscriptionService>();
            _controller = new SubscriptionController(_mockService.Object);
        }

        [Test]
        public async Task Subscribe_ShouldReturnOk_WhenSuccessful()
        {
            var subscription = new MemberSubscription { MemberId = "MEMBER1" };
            _mockService.Setup(s => s.CreateSubscriptionAsync(subscription)).ReturnsAsync(subscription);

            var result = await _controller.Subscribe(subscription) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        /*[Test]
        public async Task Unsubscribe_ShouldReturnNotFound_WhenSubscriptionDoesNotExist()
        {
            var request = new UnsubscriptionRequest { SubscriptionId = 1 };
            _mockService.Setup(s => s.CancelSubscriptionAsync(request.SubscriptionId)).ReturnsAsync((MemberSubscription)null);

            var result = await _controller.Unsubscribe(request) as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }*/

        [Test]
        public async Task GetAllSubscriptions_ShouldReturnOk_WhenSuccessful()
        {
            var subscriptions = new List<MemberSubscription>
            {
                new MemberSubscription { MemberId = "MEMBER1" },
                new MemberSubscription { MemberId = "MEMBER2" }
            };
            _mockService.Setup(s => s.GetAllSubscriptionsAsync()).ReturnsAsync(subscriptions);

            var result = await _controller.GetAllSubscriptions() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(2, ((List<MemberSubscription>)result.Value).Count);
        }
    }
}
