using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;

namespace DrugsAPI_New.Services
{
    public interface IPrescriptionService
    {
        Task<IEnumerable<MemberPrescription>> GetAllPrescriptionsAsync();
        Task<MemberPrescription> GetPrescriptionByIdAsync(int id);
        Task<MemberPrescription> AddPrescriptionAsync(MemberPrescription prescription);
        Task<MemberPrescription> UpdatePrescriptionAsync(int id, MemberPrescription prescription);
        Task<bool> DeletePrescriptionAsync(int id);
    }
}