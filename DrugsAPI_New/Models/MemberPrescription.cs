using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugsAPI_New.Models
{
    public class MemberPrescription
    {
        [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string MemberId { get; set; }
        [Required]
        public List<int> DrugIds { get; set; }
        [Required]
        public string Dosage { get; set; }
        [Required]
        public string Frequency { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int Refills { get; set; }
        public DateTime? LastRefillDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string PrescribedBy { get; set; }

        public MemberPrescription() { }

        public MemberPrescription(string memberId, List<int> drugIds, string dosage, string frequency, 
            DateTime startDate, DateTime endDate, int refills, DateTime? lastRefillDate, 
            bool isActive, string prescribedBy)
        {
            MemberId = memberId;
            DrugIds = drugIds;
            Dosage = dosage;
            Frequency = frequency;
            StartDate = startDate;
            EndDate = endDate;
            Refills = refills;
            LastRefillDate = lastRefillDate;
            IsActive = isActive;
            PrescribedBy = prescribedBy;
        }
    }
}