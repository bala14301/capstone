using DrugsAPI_New.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrugsAPI_New.DTO
{
    public class AdhocRefillRequestDto
    {
      
        public int? PolicyId { get; set; }
        public int? MemberId { get; set; }
        public int SubscriptionId { get; set; }
        public string Location { get; set; }
        public List<Drug> Drugs { get; set; }
    }
}