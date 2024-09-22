using DrugsAPI_New.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drugs_API.DTO
{
    public class SubscriptionRequest
    {
        public MemberPrescription PrescriptionDetails { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public string MemberId { get; set; }
    }
}