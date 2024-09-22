using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;
using DrugsAPI_New.Repositories;
using DrugsAPI_New.Services;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class DrugServiceTests
    {
        private Mock<IDrugRepository> _mockRepository;
        private DrugService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IDrugRepository>();
            _service = new DrugService(_mockRepository.Object);
        }

        [Test]
        public async Task CreateDrugAsync_ShouldCallRepository()
        {
            var drug = new Drug { Name = "Aspirin" };

            _mockRepository.Setup(repo => repo.CreateAsync(drug)).ReturnsAsync(drug);

            var result = await _service.CreateDrugAsync(drug);

            Assert.IsNotNull(result);
            _mockRepository.Verify(repo => repo.CreateAsync(drug), Times.Once);
        }

        [Test]
        public async Task GetAllDrugsAsync_ShouldReturnAllDrugs()
        {
            var drugs = new List<Drug> { new Drug { Name = "Aspirin" }, new Drug { Name = "Ibuprofen" } };
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(drugs);

            var result = await _service.GetAllDrugsAsync();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task DeleteDrugAsync_ShouldCallRepository()
        {
            var drugId = 1;
            _mockRepository.Setup(repo => repo.DeleteAsync(drugId)).ReturnsAsync(true);

            var result = await _service.DeleteDrugAsync(drugId);

            Assert.IsTrue(result);
            _mockRepository.Verify(repo => repo.DeleteAsync(drugId), Times.Once);
        }
    }
}
