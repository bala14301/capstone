using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drugs_API.DTO
{
    public class AvailabilityRequest
    {
        public int DrugId { get; set; }
        public string Location { get; set; }
    }
}