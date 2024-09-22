using System.Collections.Generic;
using System.Threading.Tasks;
using Drugs_API.Data;
using DrugsAPI_New.Models;
using Microsoft.EntityFrameworkCore;

namespace DrugsAPI_New.Repositories
{
    public class DrugRepository : IDrugRepository
    {
        private readonly ApplicationDbContext _context;

        public DrugRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Drug>> GetAllAsync()
        {
            return await _context.Drugs.ToListAsync();
        }

        public async Task<Drug> GetByIdAsync(int id)
        {
            return await _context.Drugs.FindAsync(id);
        }

        public async Task<Drug> CreateAsync(Drug drug)
        {
            drug.Id = 0;
            
            _context.Drugs.Add(drug);
            await _context.SaveChangesAsync();
            
            await _context.Entry(drug).ReloadAsync();
            
            return drug;
        }

        public async Task<Drug> UpdateAsync(int id, Drug drug)
        {
            _context.Entry(drug).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return drug;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var drug = await _context.Drugs.FindAsync(id);
            if (drug == null)
                return false;

            _context.Drugs.Remove(drug);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Drug>> CheckAvailabilityAsync(string location)
        {
            return await _context.Drugs.Where(d => d.Location == location).ToListAsync();
        }

        public async Task<Drug> CheckAvailabilityAsyncById(int drugId, string location)
        {
            return await _context.Drugs
                .FirstOrDefaultAsync(d => d.Id == drugId && d.Location == location);
        }

        public async Task<List<Drug>> CreateMultipleDrugsAsync(List<Drug> drugs)
    {
        await _context.Drugs.AddRangeAsync(drugs);
        await _context.SaveChangesAsync();
        return drugs;
    }
    }
}
