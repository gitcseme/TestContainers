using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Services;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    public CompaniesController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    private readonly ICompanyService _companyService;

    [HttpGet]
    public async Task<IActionResult> GetCompanies()
    {
        return Ok(await _companyService.GetCompaniesAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompany(int id)
    {
        return Ok(await _companyService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompany(CompanyCreateRequest request)
    {
        var result = await _companyService.CreateCompanyAsync(request);
        return Ok(result);
    }
}
