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
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberPrescription>>> GetAllPrescriptions()
        {
            var prescriptions = await _prescriptionService.GetAllPrescriptionsAsync();
            return Ok(prescriptions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberPrescription>> GetPrescriptionById(int id)
        {
            var prescription = await _prescriptionService.GetPrescriptionByIdAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }
            return Ok(prescription);
        }

        [HttpPost]
        public async Task<ActionResult<MemberPrescription>> AddPrescription(MemberPrescription prescription)
        {
            var createdPrescription = await _prescriptionService.AddPrescriptionAsync(prescription);
            return CreatedAtAction(nameof(GetPrescriptionById), new { id = createdPrescription.Id }, createdPrescription);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrescription(int id, MemberPrescription prescription)
        {
            if (id != prescription.Id)
            {
                return BadRequest();
            }

            var updatedPrescription = await _prescriptionService.UpdatePrescriptionAsync(id, prescription);
            if (updatedPrescription == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var result = await _prescriptionService.DeletePrescriptionAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
