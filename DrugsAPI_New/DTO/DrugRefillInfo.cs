using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

public class DrugRefillInfo
{
    public string DrugName { get; set; }
    public int Quantity { get; set; }
    public string DosageInstructions { get; set; }
}
