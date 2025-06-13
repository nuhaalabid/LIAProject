using Application.Reviews.Commands.CreateReview;
using Application.Reviews.Commands.DeleteReview;
using Application.Reviews.Commands.UpdateReview;
using Application.Reviews.Queries.GetAll;
using Application.Reviews.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(IMediator mediator, ILogger<ReviewsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _mediator.Send(new GetAllReviewQuery());

            _logger.LogInformation("Returned {Count} reviews", reviews.Count);

            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _mediator.Send(new GetReviewByIdQuery(id));

            if (review == null)
            {
                _logger.LogWarning("Review not found with ID: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("Review retrieved with ID: {Id}", id);
            return Ok(review);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewCommand command)
        {
            if (command == null)
            {
                _logger.LogWarning("Received null CreateReviewCommand.");
                return BadRequest("Invalid review data.");
            }

            var createdReview = await _mediator.Send(command);

            _logger.LogInformation("Review created with ID: {Id}", createdReview.Id);

            return Ok(createdReview); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReviewCommand command)
        {
            if (id != command.Id)
            {
                _logger.LogWarning("ID mismatch: route ID {RouteId} ≠ command ID {CommandId}", id, command.Id);
                return BadRequest("ID mismatch.");
            }
            try
            {
                var updated = await _mediator.Send(command);
                _logger.LogInformation("Review updated: {Id}", id);
                return Ok(updated); // 200 OK with updated ReviewDto
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating review with ID: {Id}", id);
                return NotFound($"Review with ID {id} not found.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _mediator.Send(new DeleteReviewCommand(id));
                _logger.LogInformation("Review deleted successfully with ID: {Id}", id);
                return Ok($"Review with ID {id} was deleted."); // 200 OK with message
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting review with ID: {Id}", id);
                return NotFound($"Review with ID {id} not found.");
            }
        }


    }
}













