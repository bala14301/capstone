using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;
using DrugsAPI_New.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Drugs_API.Data;

namespace Tests
{
    public class RefillRepositoryTests
    {
        private ApplicationDbContext _context;
        private RefillRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new RefillRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddRefillAsync_ShouldAddRefillOrder()
        {
            var refillOrder = new RefillOrderLineItem { SubscriptionId = 1, Quantity = 30, DrugId = 1 };

            var result = await _repository.AddRefillAsync(refillOrder);

            Assert.IsNotNull(result);
            Assert.AreEqual(30, result.Quantity);
        }

        [Test]
        public async Task GetAllRefillsAsync_ShouldReturnAllRefills()
        {
            await _repository.AddRefillAsync(new RefillOrderLineItem { SubscriptionId = 1, Quantity = 30, DrugId = 1 });
            await _repository.AddRefillAsync(new RefillOrderLineItem { SubscriptionId = 2, Quantity = 60, DrugId = 2 });

            var result = await _repository.GetAllRefillsAsync();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task DeleteRefillAsync_ShouldReturnFalse_WhenRefillNotFound()
        {
            var result = await _repository.DeleteRefillAsync(999); // Non-existing ID

            Assert.IsFalse(result);
        }
    }
}
