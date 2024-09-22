using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;

namespace DrugsAPI_New.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllMembersAsync();
        Task<Member> GetMemberByIdAsync(int id);
        Task<Member> AddMemberAsync(Member member);
        Task<Member> UpdateMemberAsync(int id, Member member);
        Task<bool> DeleteMemberAsync(int id);
    }
}
