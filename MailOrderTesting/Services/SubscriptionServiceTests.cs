using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;
using DrugsAPI_New.Repositories;
using DrugsAPI_New.Services;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class SubscriptionServiceTests
    {
        private Mock<ISubscriptionRepository> _mockRepository;
        private SubscriptionService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<ISubscriptionRepository>();
            _service = new SubscriptionService(_mockRepository.Object, null);
        }

        [Test]
        public async Task CreateSubscriptionAsync_ShouldCallRepository()
        {
            var subscription = new MemberSubscription { MemberId = "MEMBER1" };
            _mockRepository.Setup(repo => repo.CreateAsync(subscription)).ReturnsAsync(subscription);

            var result = await _service.CreateSubscriptionAsync(subscription);

            _mockRepository.Verify(repo => repo.CreateAsync(subscription), Times.Once);
            Assert.AreEqual("MEMBER1", result.MemberId);
        }

       /* [Test]
        public async Task GetAllSubscriptionsAsync_ShouldReturnSubscriptions()
        {
            var subscriptions = new List<MemberSubscription>
            {
                new MemberSubscription { MemberId = "MEMBER1" },
                new MemberSubscription { MemberId = "MEMBER2" }
            };
            _mockRepository.Setup(repo => repo.GetAllSubscriptionsAsync()).ReturnsAsync(subscriptions);

            var result = await _service.GetAllSubscriptionsAsync();

            Assert.AreEqual(2, result.Count);
        }*/

        [Test]
        public async Task DeleteSubscriptionAsync_ShouldCallRepository()
        {
            var subscriptionId = 1;
            _mockRepository.Setup(repo => repo.DeleteAsync(subscriptionId)).ReturnsAsync(true);

            await _service.DeleteSubscriptionAsync(subscriptionId);

            _mockRepository.Verify(repo => repo.DeleteAsync(subscriptionId), Times.Once);
        }
    }
}
