using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DrugsAPI_New.Models;
using DrugsAPI_New.Services;
using Microsoft.AspNetCore.Authorization;

namespace DrugsAPI_New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMemberById(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult<Member>> AddMember(Member member)
        {
            var createdMember = await _memberService.AddMemberAsync(member);
            return CreatedAtAction(nameof(GetMemberById), new { id = createdMember.Id }, createdMember);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Member>>> GetAllMembersAsync()
        {
            var members = await _memberService.GetAllMembersAsync();
            return Ok(members);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, Member member)
        {
            if (id != member.Id)
            {
                return BadRequest();
            }

            var updatedMember = await _memberService.UpdateMemberAsync(id, member);
            if (updatedMember == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var result = await _memberService.DeleteMemberAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
