using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Drugs_API.DTO;
using DrugsAPI_New.Services;
using DrugsAPI_New.Models;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DrugsController : ControllerBase
{
     private readonly IDrugService _drugService;

      public DrugsController(IDrugService drugService)
      {
           _drugService = drugService;
      }

    [HttpGet("searchDrugsByID")]
    [Authorize]
    public async Task<IActionResult> SearchDrugsByID(int drugId)
    {
        if (drugId<=0)
        {
            return BadRequest("Drug ID is required.");
        }

        try
        {
            var drug = await _drugService.GetDrugByIdAsync(drugId);
            if (drug == null)
            {
                return NotFound($"Drug with ID {drugId} not found.");
            }
            return Ok(drug);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while searching for the drug. {ex.Message}");
        }
    }

    [HttpGet("searchDrugsByName")]
    [Authorize]
    public async Task<IActionResult> SearchDrugsByName(string drugName)
    {
        if (string.IsNullOrWhiteSpace(drugName))
        {
            return BadRequest("Drug name is required.");
        }

        try
        {
            var drugs = await _drugService.SearchDrugsByNameAsync(drugName);
            if (drugs == null || !drugs.Any())
            {
                return NotFound($"No drugs found matching the name '{drugName}'.");
            }
            return Ok(drugs);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while searching for drugs.");
        }
    }

    [HttpPost("checkAvailability")]
    [Authorize]
    public async Task<IActionResult> CheckAvailability([FromBody] AvailabilityRequest request)
    {
        if (request.DrugId<=0 || string.IsNullOrWhiteSpace(request.Location))
        {
            return BadRequest("Invalid request. DrugId and Location are required.");
        }

        try
        {
            var availability = await _drugService.CheckDrugAvailabilityAsync(request.DrugId, request.Location);
            if (availability == null)
            {
                return NotFound($"No availability details found for drug with ID {request.DrugId} at location '{request.Location}'.");
            }
            return Ok(availability);    
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while checking drug availability.");
        }
    }

    [HttpGet("checkAvailabilityByLocation")]
    [Authorize]
    public async Task<IActionResult> CheckAvailabilityByLocation([FromQuery] string location)
    {
        if (string.IsNullOrWhiteSpace(location))
        {
            return BadRequest("Location is required.");
        }

        try
        {
            var availableDrugs = await _drugService.CheckDrugAvailability(location);
            if (availableDrugs == null || !availableDrugs.Any())
            {
                return NotFound($"No drugs found at the location '{location}'.");
            }
            return Ok(availableDrugs);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while checking drug availability.");
        }
    }

[HttpPost("addMultiple")]
[Authorize]
public async Task<IActionResult> AddMultipleDrugs([FromBody] List<Drug> drugs)
{
    if (drugs == null || !drugs.Any())
  {
        return BadRequest("Invalid drug data. Please provide a list of drugs.");
    }

    try
    {
        var addedDrugs = await _drugService.CreateMultipleDrugsAsync(drugs);
        return Ok(new { Message = "Successfully Added Drugs", DrugCount = addedDrugs.Count, DrugNames = addedDrugs.Select(d => d.Name) });
    }
    catch (Exception ex)
    {
        return StatusCode(500, "An error occurred while adding the drugs.");
    }
}
    [HttpPost("add")]
    [Authorize]
    public async Task<IActionResult> AddDrug([FromBody] Drug drug)
    {
        if (drug == null)
        {
            return BadRequest("Invalid drug data.");
        }

        try
        {
            var addedDrug = await _drugService.CreateDrugAsync(drug);
            return Ok(new { Message = "Successfully Added Drug", DrugName = addedDrug.Name });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while adding the drug.");
        }
    }

    [HttpGet("getAllDrugs")]
    [Authorize]
    public async Task<IActionResult> GetAllDrugs()
    {
        try
        {
            var drugs = await _drugService.GetAllDrugsAsync();
            if (drugs == null || !drugs.Any())
            {
                return NotFound("No drugs available.");
            }
            return Ok(drugs);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving the drugs.");
        }
    }
}
