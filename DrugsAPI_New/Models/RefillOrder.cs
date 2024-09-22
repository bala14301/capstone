using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugsAPI_New.Models
{
    public class RefillOrder
    {
        [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       
        public int SubscriptionId { get; set; }

        [Required]
        public DateTime RefillDate { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }

      
        public int RefillOrderItemId { get; set; }

        [Required]
        public string QuantityStatus { get; set; }
        [Required]
        public string MemberId { get; set; }

        [Required]
        public string Status { get; set; }

        public RefillOrder() { }

        public RefillOrder(int subscriptionId = 1, DateTime? refillDate = null, DateTime? orderDate = null, 
            int refillOrderItemId = 1, string quantityStatus = "Sufficient", DateTime? startDate = null, 
            DateTime? endDate = null, string memberId = "MEMBER1", string status = "Shipped")
        {
            MemberId = memberId;
            OrderDate = orderDate ?? DateTime.Now.AddDays(-5);
            Status = status;
            SubscriptionId = subscriptionId;
            RefillDate = refillDate ?? DateTime.Now.AddDays(-4);
            StartDate = startDate ?? DateTime.Now.AddDays(-5);
            EndDate = endDate ?? DateTime.Now.AddMonths(1);
            RefillOrderItemId = refillOrderItemId;
            QuantityStatus = quantityStatus;
        }

        
    }
}
