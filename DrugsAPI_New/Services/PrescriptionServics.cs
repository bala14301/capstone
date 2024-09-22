using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;
using DrugsAPI_New.Repositories;

namespace DrugsAPI_New.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionService(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        public async Task<IEnumerable<MemberPrescription>> GetAllPrescriptionsAsync()
        {
            return await _prescriptionRepository.GetAllPrescriptionsAsync();
        }

        public async Task<MemberPrescription> GetPrescriptionByIdAsync(int id)
        {
            return await _prescriptionRepository.GetPrescriptionByIdAsync(id);
        }

        public async Task<MemberPrescription> AddPrescriptionAsync(MemberPrescription prescription)
        {
            return await _prescriptionRepository.AddPrescriptionAsync(prescription);
        }

        public async Task<MemberPrescription> UpdatePrescriptionAsync(int id, MemberPrescription prescription)
        {
            return await _prescriptionRepository.UpdatePrescriptionAsync(id, prescription);
        }

        public async Task<bool> DeletePrescriptionAsync(int id)
        {
            return await _prescriptionRepository.DeletePrescriptionAsync(id);
        }
    }
}
