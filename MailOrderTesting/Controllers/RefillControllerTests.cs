using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Controllers;
using DrugsAPI_New.Models;
using DrugsAPI_New.Services;

namespace Tests
{
    public class RefillControllerTests
    {
        private Mock<ISubscriptionService> _mockSubscriptionService;
        private Mock<IDrugService> _mockDrugService;
        private Mock<IRefillService> _mockRefillService;
        private RefillController _controller;

        [SetUp]
        public void Setup()
        {
            _mockSubscriptionService = new Mock<ISubscriptionService>();
            _mockDrugService = new Mock<IDrugService>();
            _mockRefillService = new Mock<IRefillService>();
            _controller = new RefillController(_mockSubscriptionService.Object, _mockDrugService.Object, _mockRefillService.Object);
        }

        [Test]
        public async Task RequestRefill_ShouldReturnOk_WhenValidRequest()
        {
            var request = new RefillOrderLineItem { SubscriptionId = 1, Quantity = 10, DrugId = 1 };

            _mockRefillService.Setup(service => service.CreateRefillAsync(request)).ReturnsAsync(request);

            var result = await _controller.RequestRefill(request) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task ViewRefillStatus_ShouldReturnOk_WhenValidSubscriptionId()
        {
            var subscriptionId = 1;
            var refillOrder = new RefillOrder { SubscriptionId = subscriptionId };
            _mockSubscriptionService.Setup(service => service.GetLastRefillStatus(subscriptionId)).ReturnsAsync(refillOrder);

            var result = await _controller.ViewRefillStatus(subscriptionId) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(refillOrder, result.Value);
        }

        [Test]
        public async Task GetAllRefills_ShouldReturnOk_WhenRefillsExist()
        {
            var refillOrders = new List<RefillOrderLineItem>
            {
                new RefillOrderLineItem { SubscriptionId = 1, Quantity = 30, DrugId = 1 },
                new RefillOrderLineItem { SubscriptionId = 2, Quantity = 60, DrugId = 2 }
            };

            _mockRefillService.Setup(service => service.GetAllRefillsAsync()).ReturnsAsync(refillOrders);

            var result = await _controller.GetAllRefills();

            Assert.IsNotNull(result, "Expected result to be not null");
            Assert.IsInstanceOf<ActionResult<IEnumerable<RefillOrderLineItem>>>(result, "Expected result to be of type ActionResult<IEnumerable<RefillOrderLineItem>>");

            var okResult = result.Result as OkObjectResult;

            Assert.IsNotNull(okResult, "Expected okResult to be not null");
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(refillOrders, okResult.Value);
        }

        [Test]
        public async Task RequestRefill_ShouldReturnOk_WhenSuccessful()
        {
            var refillOrder = new RefillOrderLineItem { SubscriptionId = 1, Quantity = 30, DrugId = 1 };

            _mockRefillService.Setup(service => service.CreateRefillAsync(refillOrder)).ReturnsAsync(refillOrder);

            var result = await _controller.RequestRefill(refillOrder) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }
    }
}
