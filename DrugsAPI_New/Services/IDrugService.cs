using DrugsAPI_New.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DrugsAPI_New.Services
{   
public interface IDrugService
{
    Task<List<Drug>> CreateMultipleDrugsAsync(List<Drug> drugs);
    Task<IEnumerable<Drug>> GetAllDrugsAsync();
    Task<Drug> GetDrugByIdAsync(int id);
    Task<Drug> CreateDrugAsync(Drug drug);
    Task<Drug> UpdateDrugAsync(int id, Drug drug);
    Task<bool> DeleteDrugAsync(int id);
    Task<IEnumerable<Drug>> CheckDrugAvailability(string location);
    Task<Drug> CheckDrugAvailabilityAsync(int drugId,string location);
    Task<IEnumerable<Drug>> SearchDrugsByNameAsync(string name);
}
}
