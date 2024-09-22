using System.Collections.Generic;
using System.Threading.Tasks;
using DrugsAPI_New.Models;
using DrugsAPI_New.Repositories;

namespace DrugsAPI_New.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;


        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await _memberRepository.GetAllMembersAsync();
        }

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            return await _memberRepository.GetMemberByIdAsync(id);
        }

        public async Task<Member> AddMemberAsync(Member member)
        {
            return await _memberRepository.AddMemberAsync(member);
        }

        public async Task<Member> UpdateMemberAsync(int id, Member member)
        {
            return await _memberRepository.UpdateMemberAsync(id, member);
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            return await _memberRepository.DeleteMemberAsync(id);
        }
    }
}
