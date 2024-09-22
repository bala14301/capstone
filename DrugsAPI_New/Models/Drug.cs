using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugsAPI_New.Models
{
    public class Drug
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public Drug(string name, string manufacturer, DateTime manufacturedDate, DateTime expiryDate, 
                    int totalQuantity, string location, string description, decimal price, 
                    string dosageForm, string strength, int quantityAvailable)
        {
            Name = name;
            Manufacturer = manufacturer;
            ManufacturedDate = manufacturedDate;
            ExpiryDate = expiryDate;
            TotalQuantity = totalQuantity;
            Location = location;
            Description = description;
            Price = price;
            DosageForm = dosageForm;
            Strength = strength;
            QuantityAvailable = quantityAvailable;
        }

        public Drug()
        {
        }

        [Required]
        [StringLength(100)]
        public string Manufacturer { get; set; }

        [Required]
        public DateTime ManufacturedDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public int TotalQuantity { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string DosageForm { get; set; }

        public string Strength { get; set; }

        public int QuantityAvailable { get; set; }
    }
}