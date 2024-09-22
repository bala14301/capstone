using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugsAPI_New.Models
{
    public class RefillOrderLineItem
    {
        [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        public int SubscriptionId { get; set; }

       
        public int RefillOrderId { get; set; }

        [Required]
        public int DrugId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public RefillOrderLineItem() { }

        public RefillOrderLineItem(int subscriptionId, int refillOrderId, int drugId, int quantity)
        {
            SubscriptionId = subscriptionId;
            RefillOrderId = refillOrderId;
            DrugId = drugId;
            Quantity = quantity;
        }
    }
}
