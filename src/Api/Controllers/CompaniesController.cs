using Companies.Core.Interfaces;
using Companies.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Companies.Api.Controllers;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyQueryService _companyQueryService;

    public CompaniesController(ICompanyQueryService companyQueryService)
    {
        _companyQueryService = companyQueryService;
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Company), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromRoute]int id)
    {
        var company = await _companyQueryService.GetByIdAsync(id);
        if (company == null)
        {
            return NotFound(new Error("NotFound", $"Company with Id {id} not found."));
        }
        return Ok(company);
    }
}
