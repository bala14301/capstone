using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugsAPI_New.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DoctorName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Specialization = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    EmailId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MobileNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Drugs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Manufacturer = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ManufacturedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DosageForm = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Strength = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuantityAvailable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drugs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MemberPrescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DrugIds = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dosage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Frequency = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Refills = table.Column<int>(type: "int", nullable: false),
                    LastRefillDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PrescribedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberPrescriptions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    EmailId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MobileNo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Disease = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MemberSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubscriptionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PrescriptionId = table.Column<int>(type: "int", nullable: false),
                    RefillOccurrence = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MemberLocation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SubscriptionStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberSubscriptions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RefillOrderLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    RefillOrderId = table.Column<int>(type: "int", nullable: false),
                    DrugId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefillOrderLineItems", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RefillOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    RefillDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RefillOrderItemId = table.Column<int>(type: "int", nullable: false),
                    QuantityStatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MemberId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefillOrders", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MobileNo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<byte[]>(type: "longblob", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "longblob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Drugs",
                columns: new[] { "Id", "Description", "DosageForm", "ExpiryDate", "Location", "ManufacturedDate", "Manufacturer", "Name", "Price", "QuantityAvailable", "Strength", "TotalQuantity" },
                values: new object[,]
                {
                    { 1, "Pain reliever", "Tablet", new DateTime(2026, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6350), "Mumbai", new DateTime(2024, 3, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6329), "Bayer", "Aspirin", 5.99m, 950, "325mg", 1000 },
                    { 2, "Anti-inflammatory", "Capsule", new DateTime(2027, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6358), "Delhi", new DateTime(2024, 4, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6357), "Advil", "Ibuprofen", 7.99m, 1400, "200mg", 1500 },
                    { 3, "Antibiotic", "Capsule", new DateTime(2025, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6361), "Bangalore", new DateTime(2024, 5, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6361), "Pfizer", "Amoxicillin", 15.99m, 750, "500mg", 800 },
                    { 4, "Blood pressure medication", "Tablet", new DateTime(2026, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6365), "Chennai", new DateTime(2024, 6, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6364), "Merck", "Lisinopril", 12.99m, 1100, "10mg", 1200 },
                    { 5, "Diabetes medication", "Tablet", new DateTime(2027, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6368), "Hyderabad", new DateTime(2024, 7, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6367), "Novartis", "Metformin", 9.99m, 1900, "500mg", 2000 },
                    { 6, "Acid reflux medication", "Capsule", new DateTime(2026, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6370), "Kolkata", new DateTime(2024, 8, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6370), "AstraZeneca", "Omeprazole", 14.99m, 950, "20mg", 1000 },
                    { 7, "Cholesterol medication", "Tablet", new DateTime(2025, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6373), "Pune", new DateTime(2024, 2, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6372), "Lipitor", "Atorvastatin", 18.99m, 1400, "40mg", 1500 },
                    { 8, "Antidepressant", "Tablet", new DateTime(2026, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6376), "Ahmedabad", new DateTime(2024, 1, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6375), "Zoloft", "Sertraline", 16.99m, 900, "50mg", 1000 },
                    { 9, "Asthma inhaler", "Inhaler", new DateTime(2025, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6379), "Jaipur", new DateTime(2023, 12, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6378), "GlaxoSmithKline", "Albuterol", 24.99m, 450, "90mcg", 500 }
                });

            migrationBuilder.InsertData(
                table: "MemberSubscriptions",
                columns: new[] { "Id", "EndDate", "MemberId", "MemberLocation", "PrescriptionId", "RefillOccurrence", "StartDate", "SubscriptionDate", "SubscriptionStatus" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6500), "MEMBER1", "New York", 1, "Monthly", new DateTime(2024, 8, 22, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6501), new DateTime(2024, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6498), 1 },
                    { 2, new DateTime(2025, 7, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6506), "MEMBER2", "Los Angeles", 2, "Bi-weekly", new DateTime(2024, 7, 23, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6507), new DateTime(2024, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6505), 1 },
                    { 3, new DateTime(2025, 6, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6509), "MEMBER3", "Chicago", 3, "Weekly", new DateTime(2024, 6, 23, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6510), new DateTime(2024, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6508), 1 },
                    { 4, new DateTime(2025, 5, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6512), "MEMBER4", "Houston", 4, "Monthly", new DateTime(2024, 5, 24, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6512), new DateTime(2024, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6511), 1 },
                    { 5, new DateTime(2025, 4, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6514), "MEMBER5", "Phoenix", 5, "Bi-weekly", new DateTime(2024, 4, 24, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6515), new DateTime(2024, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6513), 1 },
                    { 6, new DateTime(2025, 3, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6517), "MEMBER6", "Philadelphia", 6, "Weekly", new DateTime(2024, 3, 25, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6518), new DateTime(2024, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6516), 1 },
                    { 7, new DateTime(2025, 2, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6520), "MEMBER7", "San Antonio", 7, "Monthly", new DateTime(2024, 2, 24, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6520), new DateTime(2024, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6519), 1 },
                    { 8, new DateTime(2025, 1, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6522), "MEMBER8", "San Diego", 8, "Bi-weekly", new DateTime(2024, 1, 25, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6523), new DateTime(2024, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6521), 1 },
                    { 9, new DateTime(2024, 12, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6525), "MEMBER9", "Dallas", 9, "Weekly", new DateTime(2023, 12, 26, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6525), new DateTime(2024, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6524), 1 },
                    { 10, new DateTime(2024, 11, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6527), "MEMBER10", "San Jose", 10, "Monthly", new DateTime(2023, 11, 26, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6528), new DateTime(2024, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6526), 1 }
                });

            migrationBuilder.InsertData(
                table: "RefillOrderLineItems",
                columns: new[] { "Id", "DrugId", "Quantity", "RefillOrderId", "SubscriptionId" },
                values: new object[,]
                {
                    { 1, 1, 30, 1, 1 },
                    { 2, 2, 60, 2, 2 },
                    { 3, 3, 20, 3, 3 },
                    { 4, 4, 90, 4, 4 },
                    { 5, 5, 60, 5, 5 },
                    { 6, 6, 30, 6, 6 },
                    { 7, 7, 30, 7, 7 },
                    { 8, 8, 30, 8, 8 },
                    { 9, 9, 30, 9, 9 },
                    { 10, 10, 1, 10, 10 }
                });

            migrationBuilder.InsertData(
                table: "RefillOrders",
                columns: new[] { "Id", "EndDate", "MemberId", "OrderDate", "QuantityStatus", "RefillDate", "RefillOrderItemId", "StartDate", "Status", "SubscriptionId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6553), "MEMBER1", new DateTime(2024, 9, 16, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6550), "Sufficient", new DateTime(2024, 9, 17, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6552), 1, new DateTime(2024, 9, 16, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6552), "Shipped", 1 },
                    { 2, new DateTime(2024, 10, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6557), "MEMBER2", new DateTime(2024, 9, 17, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6555), "Low", new DateTime(2024, 9, 18, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6556), 2, new DateTime(2024, 9, 17, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6557), "Processing", 2 },
                    { 3, new DateTime(2024, 10, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6561), "MEMBER3", new DateTime(2024, 9, 18, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6559), "Sufficient", new DateTime(2024, 9, 19, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6560), 3, new DateTime(2024, 9, 18, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6560), "Pending", 3 },
                    { 4, new DateTime(2024, 10, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6564), "MEMBER4", new DateTime(2024, 9, 19, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6562), "Sufficient", new DateTime(2024, 9, 20, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6563), 4, new DateTime(2024, 9, 19, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6564), "Shipped", 4 },
                    { 5, new DateTime(2024, 10, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6567), "MEMBER5", new DateTime(2024, 9, 20, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6565), "Low", new DateTime(2024, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6566), 5, new DateTime(2024, 9, 20, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6567), "Processing", 5 },
                    { 6, new DateTime(2024, 10, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6570), "MEMBER6", new DateTime(2024, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6569), "Sufficient", new DateTime(2024, 9, 22, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6569), 6, new DateTime(2024, 9, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6570), "Pending", 6 },
                    { 7, new DateTime(2024, 11, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6574), "MEMBER7", new DateTime(2024, 9, 22, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6572), "Sufficient", new DateTime(2024, 9, 23, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6573), 7, new DateTime(2024, 9, 22, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6573), "Scheduled", 7 },
                    { 8, new DateTime(2024, 11, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6577), "MEMBER8", new DateTime(2024, 9, 23, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6575), "Low", new DateTime(2024, 9, 24, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6576), 8, new DateTime(2024, 9, 23, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6577), "Scheduled", 8 },
                    { 9, new DateTime(2024, 11, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6580), "MEMBER9", new DateTime(2024, 9, 24, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6578), "Sufficient", new DateTime(2024, 9, 25, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6579), 9, new DateTime(2024, 9, 24, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6580), "Scheduled", 9 },
                    { 10, new DateTime(2024, 11, 21, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6584), "MEMBER10", new DateTime(2024, 9, 25, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6582), "Sufficient", new DateTime(2024, 9, 26, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6582), 10, new DateTime(2024, 9, 25, 14, 16, 11, 604, DateTimeKind.Local).AddTicks(6583), "Scheduled", 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropTable(
                name: "MemberPrescriptions");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "MemberSubscriptions");

            migrationBuilder.DropTable(
                name: "RefillOrderLineItems");

            migrationBuilder.DropTable(
                name: "RefillOrders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
