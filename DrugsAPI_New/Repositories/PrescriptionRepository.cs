using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;
using Microsoft.EntityFrameworkCore;
using Drugs_API.Data;

namespace DrugsAPI_New.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MemberPrescription>> GetAllPrescriptionsAsync()
        {
            return await _context.MemberPrescriptions.ToListAsync();
        }

        public async Task<MemberPrescription> GetPrescriptionByIdAsync(int id)
        {
            return await _context.MemberPrescriptions.FindAsync(id);
        }

        public async Task<MemberPrescription> AddPrescriptionAsync(MemberPrescription prescription)
        {
            _context.MemberPrescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            return prescription;
        }

        public async Task<MemberPrescription> UpdatePrescriptionAsync(int id, MemberPrescription prescription)
        {
            if (id != prescription.Id)
            {
                return null;
            }

            _context.Entry(prescription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PrescriptionExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return prescription;
        }

        public async Task<bool> DeletePrescriptionAsync(int id)
        {
            var prescription = await _context.MemberPrescriptions.FindAsync(id);
            if (prescription == null)
            {
                return false;
            }

            _context.MemberPrescriptions.Remove(prescription);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<bool> PrescriptionExists(int id)
        {
            return await _context.MemberPrescriptions.AnyAsync(e => e.Id == id);
        }
    }
}
