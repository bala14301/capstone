using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugsAPI_New.Models
{
    public class MemberSubscription
    {
        [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string MemberId { get; set; }

        [Required]
        public DateTime SubscriptionDate { get; set; }

        [Required]
        public int PrescriptionId { get; set; }

        [Required]
        public string RefillOccurrence { get; set; }

        [Required]
        public string MemberLocation { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int SubscriptionStatus { get; set; }

        public MemberSubscription() { }

        public MemberSubscription(string memberId, DateTime subscriptionDate, int prescriptionId, 
                                  string refillOccurrence, string memberLocation, int subscriptionStatus, DateTime endDate, DateTime startDate)
        {
            MemberId = memberId;
            SubscriptionDate = subscriptionDate;
            PrescriptionId = prescriptionId;
            RefillOccurrence = refillOccurrence;
            MemberLocation = memberLocation;
            SubscriptionStatus = subscriptionStatus;
            EndDate = endDate;
            StartDate = startDate;
        }
    }
}