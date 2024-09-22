using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;

namespace DrugsAPI_New.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(int id);
        Task<Doctor> AddDoctorAsync(Doctor doctor);
        Task<Doctor> UpdateDoctorAsync(int id, Doctor doctor);
        Task<bool> DeleteDoctorAsync(int id);
    }
}
