using System.Collections.Generic;
using System;

public class RefillDetailsDto
{
    public string RefillId { get; set; }
    public DateTime RefillDate { get; set; }
    public string Status { get; set; }
    public List<DrugRefillInfo> Drugs { get; set; }
}