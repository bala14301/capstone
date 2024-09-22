using System.Collections.Generic;
using System.Threading.Tasks;
using Drugs_API.Data;
using DrugsAPI_New.Models;
using Microsoft.EntityFrameworkCore;

namespace DrugsAPI_New.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task<Doctor> UpdateDoctorAsync(int id, Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return null;
            }

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DoctorExistsAsync(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return doctor;
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return false;
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> DoctorExistsAsync(int id)
        {
            return await _context.Doctors.AnyAsync(e => e.Id == id);
        }

    }
}