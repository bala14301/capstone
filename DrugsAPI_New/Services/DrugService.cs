using DrugsAPI_New.Models;
using DrugsAPI_New.Repositories;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace DrugsAPI_New.Services
{
public class DrugService : IDrugService
{
    private readonly IDrugRepository _drugRepository;

    public DrugService(IDrugRepository drugRepository)
    {
        _drugRepository = drugRepository;
    }

    public async Task<IEnumerable<Drug>> GetAllDrugsAsync()
    {
        return await _drugRepository.GetAllAsync();
    }

    public async Task<Drug> GetDrugByIdAsync(int id)
    {
        return await _drugRepository.GetByIdAsync(id);
    }

    public async Task<Drug> CreateDrugAsync(Drug drug)
    {
        try
        {
            return await _drugRepository.CreateAsync(drug);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating drug: {ex.Message}");
            throw;
        }
    }

     public async Task<List<Drug>> CreateMultipleDrugsAsync(List<Drug> drugs)
    {
        return await _drugRepository.CreateMultipleDrugsAsync(drugs);
    }

    public async Task<Drug> UpdateDrugAsync(int id, Drug drug)
    {
        try
        {
            return await _drugRepository.UpdateAsync(id, drug);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating drug: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> DeleteDrugAsync(int id)
    {
        try
        {
            return await _drugRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting drug: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<Drug>> CheckDrugAvailability(string location)
    {
        return await _drugRepository.CheckAvailabilityAsync(location);
    }

    public async Task<IEnumerable<Drug>> SearchDrugsByNameAsync(string name)
    {
        var allDrugs = await GetAllDrugsAsync();

        return allDrugs.Where(d => d.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Drug> CheckDrugAvailabilityAsync(int drugId, string location)
    {
        var drug = await _drugRepository.GetByIdAsync(drugId);

        if (drug == null)
        {
            return null;
        }

        if (drug.Location.Equals(location, StringComparison.OrdinalIgnoreCase) && drug.QuantityAvailable > 0)
        {
            return drug;
        }

        return null;
    }

    public async Task<Drug> AddDrugAsync(Drug drug)
    {
        if (drug == null)
        {
            throw new ArgumentNullException(nameof(drug));
        }

        try
        {
            return await _drugRepository.CreateAsync(drug);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding drug: {ex.Message}");
            throw new Exception($"An error occurred while adding the drug: {ex.Message}", ex);
        }
    }
}
}
