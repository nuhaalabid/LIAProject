using Application.Companies.Commands.CreateCompany;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(IMediator mediator, ILogger<CompaniesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        //public async Task<IActionResult> GetAll()
        //{
        //    var companies = await _mediator.Send(new GetAllCompaniesQuery());
        //    _logger.LogInformation(" Fetched {Count} companies", companies.Count);
        //    return Ok(companies);
        //}



        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var company = await _mediator.Send(new GetCompanyByIdQuery { Id = id });

        //    if (company == null)
        //    {
        //        _logger.LogWarning("Company not found with ID: {Id}", id);
        //        return NotFound();
        //    }

        //    _logger.LogInformation("Company found: {Id}", id);
        //    return Ok(company);
        //}



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCompanyCommand command)
        {
            var id = await _mediator.Send(command);
            _logger.LogInformation("Company created with ID: {Id}", id);
            return Ok(new { Id = id });
        }



        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] UpdateCompanyCommand command)
        //{
        //    if (id != command.Id)
        //    {
        //        _logger.LogWarning("ID mismatch: route ID {RouteId} ≠ command ID {CommandId}", id, command.Id);
        //        return BadRequest("ID mismatch.");
        //    }
        //    await _mediator.Send(command);
        //    _logger.LogInformation("Company updated: {Id}", id);
        //    return NoContent();
        //}



        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _mediator.Send(new DeleteCompanyCommand { Id = id });
        //    _logger.LogInformation("Company deleted: {Id}", id);
        //    return NoContent();
        //}

    }


}
