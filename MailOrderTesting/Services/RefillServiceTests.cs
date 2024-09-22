using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;
using DrugsAPI_New.Repositories;
using DrugsAPI_New.Services;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class RefillServiceTests
    {
        private Mock<IRefillRepository> _mockRepository;
        private RefillService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IRefillRepository>();
            _service = new RefillService(_mockRepository.Object);
        }

        [Test]
        public async Task CreateRefillAsync_ShouldCallRepository()
        {
            var refillOrder = new RefillOrderLineItem { SubscriptionId = 1, Quantity = 30, DrugId = 1 };

            _mockRepository.Setup(repo => repo.AddRefillAsync(refillOrder)).ReturnsAsync(refillOrder);

            var result = await _service.CreateRefillAsync(refillOrder);

            Assert.IsNotNull(result);
            _mockRepository.Verify(repo => repo.AddRefillAsync(refillOrder), Times.Once);
        }

        [Test]
        public async Task GetAllRefillsAsync_ShouldReturnAllRefills()
        {
            var refillOrders = new List<RefillOrderLineItem>
            {
                new RefillOrderLineItem { SubscriptionId = 1, Quantity = 30, DrugId = 1 },
                new RefillOrderLineItem { SubscriptionId = 2, Quantity = 60, DrugId = 2 }
            };

            _mockRepository.Setup(repo => repo.GetAllRefillsAsync()).ReturnsAsync(refillOrders);

            var result = await _service.GetAllRefillsAsync();

            Assert.AreEqual(2, result.Count());
        }
    }
}
