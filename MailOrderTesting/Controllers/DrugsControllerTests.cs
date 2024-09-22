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
    public class DrugsControllerTests
    {
        private Mock<IDrugService> _mockService;
        private DrugsController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IDrugService>();
            _controller = new DrugsController(_mockService.Object);
        }

        [Test]
        public async Task GetAllDrugs_ShouldReturnOkResult()
        {
            var drugs = new List<Drug> { new Drug { Id = 1, Name = "Aspirin", Manufacturer = "Bayer", ManufacturedDate = DateTime.Now.AddMonths(-6), ExpiryDate = DateTime.Now.AddYears(2), TotalQuantity = 1000, Location = "Mumbai", Description = "Pain reliever", Price = 5.99m, DosageForm = "Tablet", Strength = "325mg", QuantityAvailable = 950 }, new Drug { Id = 2, Name = "Ibuprofen", Manufacturer = "Advil", ManufacturedDate = DateTime.Now.AddMonths(-5), ExpiryDate = DateTime.Now.AddYears(3), TotalQuantity = 1500, Location = "Delhi", Description = "Anti-inflammatory", Price = 7.99m, DosageForm = "Capsule", Strength = "200mg", QuantityAvailable = 1400 } };
            _mockService.Setup(service => service.GetAllDrugsAsync()).ReturnsAsync(drugs);

            var result = await _controller.GetAllDrugs();

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(drugs, okResult.Value);
        }



        [Test]
        public async Task SearchDrugsByID_ShouldReturnNotFound_WhenDrugNotFound()
        {
            int drugId = 999;
            _mockService.Setup(service => service.GetDrugByIdAsync(drugId)).ReturnsAsync((Drug)null);

            var result = await _controller.SearchDrugsByID(drugId);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
