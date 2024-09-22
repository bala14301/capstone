using System.Collections.Generic;
using System.Threading.Tasks;
using Drugs_API.Data;
using DrugsAPI_New.Models;
using Microsoft.EntityFrameworkCore;

namespace DrugsAPI_New.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            return await _context.Members.FindAsync(id);
        }

        public async Task<Member> AddMemberAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<Member> UpdateMemberAsync(int id, Member member)
        {
            if (id != member.Id)
            {
                return null;
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return member;
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return false;
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }
    }
}
