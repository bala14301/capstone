using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;
using DrugsAPI_New.Repositories;

namespace DrugsAPI_New.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _doctorRepository.GetDoctorByIdAsync(id);
        }

        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            return await _doctorRepository.AddDoctorAsync(doctor);
        }

        public async Task<Doctor> UpdateDoctorAsync(int id, Doctor doctor)
        {
            return await _doctorRepository.UpdateDoctorAsync(id, doctor);
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            return await _doctorRepository.DeleteDoctorAsync(id);
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _doctorRepository.GetAllDoctorsAsync();
        }
    }
}
