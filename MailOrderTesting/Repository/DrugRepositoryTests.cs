using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;
using DrugsAPI_New.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Drugs_API.Data;

namespace Tests
{
    public class DrugRepositoryTests
    {
        private ApplicationDbContext _context;
        private DrugRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new DrugRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task CreateAsync_ShouldAddDrug()
        {
            var drug = new Drug { Id = 1, Name = "Aspirin", Manufacturer = "Bayer", ManufacturedDate = DateTime.Now.AddMonths(-6), ExpiryDate = DateTime.Now.AddYears(2), TotalQuantity = 1000, Location = "Mumbai", Description = "Pain reliever", Price = 5.99m, DosageForm = "Tablet", Strength = "325mg", QuantityAvailable = 950 };

            var result = await _repository.CreateAsync(drug);

            Assert.IsNotNull(result);
            Assert.AreEqual("Aspirin", result.Name);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllDrugs()
        {
            await _repository.CreateAsync(new Drug { Id = 1, Name = "Aspirin", Manufacturer = "Bayer", ManufacturedDate = DateTime.Now.AddMonths(-6), ExpiryDate = DateTime.Now.AddYears(2), TotalQuantity = 1000, Location = "Mumbai", Description = "Pain reliever", Price = 5.99m, DosageForm = "Tablet", Strength = "325mg", QuantityAvailable = 950 });
            await _repository.CreateAsync(new Drug { Id = 2, Name = "Ibuprofen", Manufacturer = "Advil", ManufacturedDate = DateTime.Now.AddMonths(-5), ExpiryDate = DateTime.Now.AddYears(3), TotalQuantity = 1500, Location = "Delhi", Description = "Anti-inflammatory", Price = 7.99m, DosageForm = "Capsule", Strength = "200mg", QuantityAvailable = 1400 });

            var result = await _repository.GetAllAsync();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnDrug()
        {
            var drug = await _repository.CreateAsync(new Drug {Id = 1, Name = "Aspirin", Manufacturer = "Bayer", ManufacturedDate = DateTime.Now.AddMonths(-6), ExpiryDate = DateTime.Now.AddYears(2), TotalQuantity = 1000, Location = "Mumbai", Description = "Pain reliever", Price = 5.99m, DosageForm = "Tablet", Strength = "325mg", QuantityAvailable = 950 });

            var result = await _repository.GetByIdAsync(drug.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual("Aspirin", result.Name);
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveDrug()
        {
            var drug = await _repository.CreateAsync(new Drug {Id = 1, Name = "Aspirin", Manufacturer = "Bayer", ManufacturedDate = DateTime.Now.AddMonths(-6), ExpiryDate = DateTime.Now.AddYears(2), TotalQuantity = 1000, Location = "Mumbai", Description = "Pain reliever", Price = 5.99m, DosageForm = "Tablet", Strength = "325mg", QuantityAvailable = 950 });

            var result = await _repository.DeleteAsync(drug.Id);

            Assert.IsTrue(result);
            Assert.IsNull(await _repository.GetByIdAsync(drug.Id));
        }
    }
}
