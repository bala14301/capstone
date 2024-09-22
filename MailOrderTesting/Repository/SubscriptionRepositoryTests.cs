using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;
using DrugsAPI_New.Repositories;
using Drugs_API.Data; // Ensure this namespace is included for ApplicationDbContext
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests
{
    public class SubscriptionRepositoryTests
    {
        private ApplicationDbContext _context;
        private SubscriptionRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new SubscriptionRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose(); // Dispose of the context after each test
        }

        [Test]
        public async Task CreateAsync_ShouldAddSubscription()
        {
            var subscription = new MemberSubscription { Id = 1, MemberId = "MEMBER1", SubscriptionDate = DateTime.Now, PrescriptionId = 1, RefillOccurrence = "Monthly", MemberLocation = "New York", SubscriptionStatus = 1, EndDate = DateTime.Now.AddMonths(11), StartDate = DateTime.Now.AddDays(-30) };

            var result = await _repository.CreateAsync(subscription);

            Assert.IsNotNull(result);
            Assert.AreEqual("MEMBER1", result.MemberId);
        }

        /*  [Test]
          public async Task GetAllSubscriptionsAsync_ShouldReturnAllSubscriptions()
          {
              await _repository.CreateAsync(new MemberSubscription { MemberId = "MEMBER1" });
              await _repository.CreateAsync(new MemberSubscription { MemberId = "MEMBER2" });

              var result = await _repository.GetAllSubscriptionsAsync();

              Assert.AreEqual(2, result.Count);
          }*/

        [Test]
        public async Task DeleteAsync_ShouldRemoveSubscription()
        {
            var subscription = await _repository.CreateAsync(new MemberSubscription { Id = 1, MemberId = "MEMBER1", SubscriptionDate = DateTime.Now, PrescriptionId = 1, RefillOccurrence = "Monthly", MemberLocation = "New York", SubscriptionStatus = 1, EndDate = DateTime.Now.AddMonths(11), StartDate = DateTime.Now.AddDays(-30) });
            var result = await _repository.DeleteAsync(subscription.Id);

            Assert.IsTrue(result);
            Assert.IsNull(await _repository.GetByIdAsync(subscription.Id));
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateSubscription()
        {
            var subscription = await _repository.CreateAsync(new MemberSubscription { Id = 1, MemberId = "MEMBER1", SubscriptionDate = DateTime.Now, PrescriptionId = 1, RefillOccurrence = "Monthly", MemberLocation = "New York", SubscriptionStatus = 1, EndDate = DateTime.Now.AddMonths(11), StartDate = DateTime.Now.AddDays(-30) });
            subscription.MemberId = "MEMBER_UPDATED";

            var result = await _repository.UpdateAsync(subscription);

            Assert.AreEqual("MEMBER_UPDATED", result.MemberId);
        }
    }
}
