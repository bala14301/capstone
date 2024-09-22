using Microsoft.EntityFrameworkCore;
using DrugsAPI_New.Models;
using System;
using System.Linq;

namespace Drugs_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase(databaseName: "AuthorDb");
        }

        public DbSet<RefillOrderLineItem> RefillOrderLineItems { get; set; }
        public DbSet<RefillOrder> RefillOrders { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<MemberPrescription> MemberPrescriptions { get; set; }
        public DbSet<MemberSubscription> MemberSubscriptions { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<User> Users { get; set; } // New DbSet for Users
        public DbSet<Member> Members { get; set; } // New DbSet for Members

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Drug>().Property(d => d.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<MemberPrescription>().Property(mp => mp.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<MemberSubscription>().Property(ms => ms.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<RefillOrder>().Property(ro => ro.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<RefillOrderLineItem>().Property(roli => roli.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd(); // Add this line for User entity
            SeedData(modelBuilder);
        }
        
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Drugs
            var drugs = new[]
            {
                new Drug { Id = 1,Name = "Aspirin", Manufacturer = "Bayer", ManufacturedDate = DateTime.Now.AddMonths(-6), ExpiryDate = DateTime.Now.AddYears(2), TotalQuantity = 1000, Location = "Mumbai", Description = "Pain reliever", Price = 5.99m, DosageForm = "Tablet", Strength = "325mg", QuantityAvailable = 950 },
                new Drug { Id = 2,Name = "Ibuprofen", Manufacturer = "Advil", ManufacturedDate = DateTime.Now.AddMonths(-5), ExpiryDate = DateTime.Now.AddYears(3), TotalQuantity = 1500, Location = "Delhi", Description = "Anti-inflammatory", Price = 7.99m, DosageForm = "Capsule", Strength = "200mg", QuantityAvailable = 1400 },
                new Drug { Id = 3,Name = "Amoxicillin", Manufacturer = "Pfizer", ManufacturedDate = DateTime.Now.AddMonths(-4), ExpiryDate = DateTime.Now.AddYears(1), TotalQuantity = 800, Location = "Bangalore", Description = "Antibiotic", Price = 15.99m, DosageForm = "Capsule", Strength = "500mg", QuantityAvailable = 750 },
                new Drug { Id = 4,Name = "Lisinopril", Manufacturer = "Merck", ManufacturedDate = DateTime.Now.AddMonths(-3), ExpiryDate = DateTime.Now.AddYears(2), TotalQuantity = 1200, Location = "Chennai", Description = "Blood pressure medication", Price = 12.99m, DosageForm = "Tablet", Strength = "10mg", QuantityAvailable = 1100 },
                new Drug { Id = 5,Name = "Metformin", Manufacturer = "Novartis", ManufacturedDate = DateTime.Now.AddMonths(-2), ExpiryDate = DateTime.Now.AddYears(3), TotalQuantity = 2000, Location = "Hyderabad", Description = "Diabetes medication", Price = 9.99m, DosageForm = "Tablet", Strength = "500mg", QuantityAvailable = 1900 },
                new Drug { Id = 6,Name = "Omeprazole", Manufacturer = "AstraZeneca", ManufacturedDate = DateTime.Now.AddMonths(-1), ExpiryDate = DateTime.Now.AddYears(2), TotalQuantity = 1000, Location = "Kolkata", Description = "Acid reflux medication", Price = 14.99m, DosageForm = "Capsule", Strength = "20mg", QuantityAvailable = 950 },
                new Drug { Id = 7,Name = "Atorvastatin", Manufacturer = "Lipitor", ManufacturedDate = DateTime.Now.AddMonths(-7), ExpiryDate = DateTime.Now.AddYears(1), TotalQuantity = 1500, Location = "Pune", Description = "Cholesterol medication", Price = 18.99m, DosageForm = "Tablet", Strength = "40mg", QuantityAvailable = 1400 },
                new Drug { Id = 8,Name = "Sertraline", Manufacturer = "Zoloft", ManufacturedDate = DateTime.Now.AddMonths(-8), ExpiryDate = DateTime.Now.AddYears(2), TotalQuantity = 1000, Location = "Ahmedabad", Description = "Antidepressant", Price = 16.99m, DosageForm = "Tablet", Strength = "50mg", QuantityAvailable = 900 },
                new Drug { Id = 9,Name = "Albuterol", Manufacturer = "GlaxoSmithKline", ManufacturedDate = DateTime.Now.AddMonths(-9), ExpiryDate = DateTime.Now.AddYears(1), TotalQuantity = 500, Location = "Jaipur", Description = "Asthma inhaler", Price = 24.99m, DosageForm = "Inhaler", Strength = "90mcg", QuantityAvailable = 450 }
            };
            modelBuilder.Entity<Drug>().HasData(drugs);

           

            // Seed MemberSubscriptions
            var subscriptions = new[]
            {
                new MemberSubscription { Id=1, MemberId = "MEMBER1", SubscriptionDate = DateTime.Now, PrescriptionId = 1, RefillOccurrence = "Monthly", MemberLocation = "New York", SubscriptionStatus = 1, EndDate = DateTime.Now.AddMonths(11), StartDate = DateTime.Now.AddDays(-30) },
                new MemberSubscription { Id=2, MemberId = "MEMBER2", SubscriptionDate = DateTime.Now, PrescriptionId = 2, RefillOccurrence = "Bi-weekly", MemberLocation = "Los Angeles", SubscriptionStatus = 1, EndDate = DateTime.Now.AddMonths(10), StartDate = DateTime.Now.AddDays(-60) },
                new MemberSubscription { Id=3, MemberId = "MEMBER3", SubscriptionDate = DateTime.Now, PrescriptionId = 3, RefillOccurrence = "Weekly", MemberLocation = "Chicago", SubscriptionStatus = 1, EndDate = DateTime.Now.AddMonths(9), StartDate = DateTime.Now.AddDays(-90) },
                new MemberSubscription { Id=4, MemberId = "MEMBER4", SubscriptionDate = DateTime.Now, PrescriptionId = 4, RefillOccurrence = "Monthly", MemberLocation = "Houston", SubscriptionStatus = 1, EndDate = DateTime.Now.AddMonths(8), StartDate = DateTime.Now.AddDays(-120) },
                new MemberSubscription { Id=5, MemberId = "MEMBER5", SubscriptionDate = DateTime.Now, PrescriptionId = 5, RefillOccurrence = "Bi-weekly", MemberLocation = "Phoenix", SubscriptionStatus = 1, EndDate = DateTime.Now.AddMonths(7), StartDate = DateTime.Now.AddDays(-150) },
                new MemberSubscription { Id=6, MemberId = "MEMBER6", SubscriptionDate = DateTime.Now, PrescriptionId = 6, RefillOccurrence = "Weekly", MemberLocation = "Philadelphia", SubscriptionStatus = 1, EndDate = DateTime.Now.AddMonths(6), StartDate = DateTime.Now.AddDays(-180) },
                new MemberSubscription { Id=7, MemberId = "MEMBER7", SubscriptionDate = DateTime.Now, PrescriptionId = 7, RefillOccurrence = "Monthly", MemberLocation = "San Antonio", SubscriptionStatus = 1, EndDate = DateTime.Now.AddMonths(5), StartDate = DateTime.Now.AddDays(-210) },
                new MemberSubscription { Id=8, MemberId = "MEMBER8", SubscriptionDate = DateTime.Now, PrescriptionId = 8, RefillOccurrence = "Bi-weekly", MemberLocation = "San Diego", SubscriptionStatus = 1, EndDate = DateTime.Now.AddMonths(4), StartDate = DateTime.Now.AddDays(-240) },
                new MemberSubscription { Id=9, MemberId = "MEMBER9", SubscriptionDate = DateTime.Now, PrescriptionId = 9, RefillOccurrence = "Weekly", MemberLocation = "Dallas", SubscriptionStatus = 1, EndDate = DateTime.Now.AddMonths(3), StartDate = DateTime.Now.AddDays(-270) },
                new MemberSubscription { Id=10, MemberId = "MEMBER10", SubscriptionDate = DateTime.Now, PrescriptionId = 10, RefillOccurrence = "Monthly", MemberLocation = "San Jose", SubscriptionStatus = 1, EndDate = DateTime.Now.AddMonths(2), StartDate = DateTime.Now.AddDays(-300) }
            };
            modelBuilder.Entity<MemberSubscription>().HasData(subscriptions);

            // Seed RefillOrders
            var refillOrders = new[]
            {
                new RefillOrder {Id=1, MemberId = "MEMBER1", OrderDate = DateTime.Now.AddDays(-5), Status = "Shipped", SubscriptionId = 1, RefillDate = DateTime.Now.AddDays(-4), StartDate = DateTime.Now.AddDays(-5), EndDate = DateTime.Now.AddMonths(1), RefillOrderItemId = 1, QuantityStatus = "Sufficient" },
                new RefillOrder {Id=2, MemberId = "MEMBER2", OrderDate = DateTime.Now.AddDays(-4), Status = "Processing", SubscriptionId = 2, RefillDate = DateTime.Now.AddDays(-3), StartDate = DateTime.Now.AddDays(-4), EndDate = DateTime.Now.AddMonths(1), RefillOrderItemId = 2, QuantityStatus = "Low" },
                new RefillOrder {Id=3, MemberId = "MEMBER3", OrderDate = DateTime.Now.AddDays(-3), Status = "Pending", SubscriptionId = 3, RefillDate = DateTime.Now.AddDays(-2), StartDate = DateTime.Now.AddDays(-3), EndDate = DateTime.Now.AddMonths(1), RefillOrderItemId = 3, QuantityStatus = "Sufficient" },
                new RefillOrder {Id=4, MemberId = "MEMBER4", OrderDate = DateTime.Now.AddDays(-2), Status = "Shipped", SubscriptionId = 4, RefillDate = DateTime.Now.AddDays(-1), StartDate = DateTime.Now.AddDays(-2), EndDate = DateTime.Now.AddMonths(1), RefillOrderItemId = 4, QuantityStatus = "Sufficient" },
                new RefillOrder {Id=5, MemberId = "MEMBER5", OrderDate = DateTime.Now.AddDays(-1), Status = "Processing", SubscriptionId = 5, RefillDate = DateTime.Now, StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddMonths(1), RefillOrderItemId = 5, QuantityStatus = "Low" },
                new RefillOrder {Id=6, MemberId = "MEMBER6", OrderDate = DateTime.Now, Status = "Pending", SubscriptionId = 6, RefillDate = DateTime.Now.AddDays(1), StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), RefillOrderItemId = 6, QuantityStatus = "Sufficient" },
                new RefillOrder {Id=7, MemberId = "MEMBER7", OrderDate = DateTime.Now.AddDays(1), Status = "Scheduled", SubscriptionId = 7, RefillDate = DateTime.Now.AddDays(2), StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddMonths(2), RefillOrderItemId = 7, QuantityStatus = "Sufficient" },
                new RefillOrder {Id=8, MemberId = "MEMBER8", OrderDate = DateTime.Now.AddDays(2), Status = "Scheduled", SubscriptionId = 8, RefillDate = DateTime.Now.AddDays(3), StartDate = DateTime.Now.AddDays(2), EndDate = DateTime.Now.AddMonths(2), RefillOrderItemId = 8, QuantityStatus = "Low" },
                new RefillOrder {Id=9, MemberId = "MEMBER9", OrderDate = DateTime.Now.AddDays(3), Status = "Scheduled", SubscriptionId = 9, RefillDate = DateTime.Now.AddDays(4), StartDate = DateTime.Now.AddDays(3), EndDate = DateTime.Now.AddMonths(2), RefillOrderItemId = 9, QuantityStatus = "Sufficient" },
                new RefillOrder {Id=10, MemberId = "MEMBER10", OrderDate = DateTime.Now.AddDays(4), Status = "Scheduled", SubscriptionId = 10, RefillDate = DateTime.Now.AddDays(5), StartDate = DateTime.Now.AddDays(4), EndDate = DateTime.Now.AddMonths(2), RefillOrderItemId = 10, QuantityStatus = "Sufficient" }
            };
            modelBuilder.Entity<RefillOrder>().HasData(refillOrders);

            // Seed RefillOrderLineItems
            var lineItems = new[]
            {
                new RefillOrderLineItem {Id=1, RefillOrderId = 1, DrugId = 1, SubscriptionId = 1, Quantity = 30 },
                new RefillOrderLineItem {Id=2, RefillOrderId = 2, DrugId = 2, SubscriptionId = 2, Quantity = 60 },
                new RefillOrderLineItem {Id=3, RefillOrderId = 3, DrugId = 3, SubscriptionId = 3, Quantity = 20 },
                new RefillOrderLineItem {Id=4, RefillOrderId = 4, DrugId = 4, SubscriptionId = 4, Quantity = 90 },
                new RefillOrderLineItem {Id=5, RefillOrderId = 5, DrugId = 5, SubscriptionId = 5, Quantity = 60 },
                
                new RefillOrderLineItem {Id=6, RefillOrderId = 6, DrugId = 6, SubscriptionId = 6, Quantity = 30 },
                new RefillOrderLineItem {Id=7, RefillOrderId = 7, DrugId = 7, SubscriptionId = 7, Quantity = 30 },
                new RefillOrderLineItem {Id=8, RefillOrderId = 8, DrugId = 8, SubscriptionId = 8, Quantity = 30 },
                new RefillOrderLineItem {Id=9, RefillOrderId = 9, DrugId = 9, SubscriptionId = 9, Quantity = 30 },
                new RefillOrderLineItem {Id=10, RefillOrderId = 10, DrugId = 10, SubscriptionId = 10, Quantity = 1 }
            };
            modelBuilder.Entity<RefillOrderLineItem>().HasData(lineItems);
        }
    }
}
