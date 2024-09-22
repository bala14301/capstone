using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drugs_API.DTO
{
  public class UnsubscriptionRequest
    {
        public int MemberId { get; set; }
        public int SubscriptionId { get; set; }
    }
}