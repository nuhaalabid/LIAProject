using Application.Companies.Commands.CreateCompany;
using Application.Companies.Commands.DeleteCompany;
using Application.Companies.Commands.UpdateCompany;
using Application.Companies.Queries.GetAll;
using Application.Companies.Queries.GetById;
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


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all companies...");

            var companies = await _mediator.Send(new GetAllCompanyQuery());

            _logger.LogInformation("Retrieved {Count} companies.", companies.Count);

            return Ok(companies);
        }
    

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Request to get company with ID: {Id}", id);

            var result = await _mediator.Send(new GetCompanyByIdQuery(id));

            if (result == null)
            {
                _logger.LogWarning("Company not found with ID: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("Company retrieved with ID: {Id}", id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCompanyCommand command)
        {
            if (command == null)
            {
                _logger.LogWarning("CreateCompanyCommand was null.");
                return BadRequest("Ogiltig förfrågan.");
            }

            try
            {
                var id = await _mediator.Send(command);
                _logger.LogInformation("Company created with ID: {Id}", id);
                return Ok(new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Något gick fel vid skapande av företag.");
                return StatusCode(500, "Internt serverfel.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCompanyCommand command)
        {
            if (id != command.Id)
            {
                _logger.LogWarning("ID mismatch: route ID {RouteId} ≠ command ID {CommandId}", id, command.Id);
                return BadRequest("ID mismatch.");
            }

            await _mediator.Send(command);
            _logger.LogInformation("Company updated: {Id}", id);

            return NoContent(); // 204 - OK, men inget innehåll
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteCompanyCommand(id));

            _logger.LogInformation("Company deleted with ID: {Id}", id);
            return NoContent(); // HTTP 204
        }

    }


}
