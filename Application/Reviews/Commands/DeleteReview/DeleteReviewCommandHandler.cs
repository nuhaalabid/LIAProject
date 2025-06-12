using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Unit>
    {
        private readonly IReviewRepository _repository;
        private readonly ILogger<DeleteReviewCommandHandler> _logger;

        public DeleteReviewCommandHandler(IReviewRepository repository, ILogger<DeleteReviewCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _repository.GetByIdAsync(request.Id);
            if (review == null)
            {
                _logger.LogWarning("Review with ID {Id} not found for deletion", request.Id);
                throw new Exception($"Review with ID {request.Id} not found.");
            }

            await _repository.DeleteAsync(request.Id);
            _logger.LogInformation("Review deleted with ID: {Id}", request.Id);

            return Unit.Value;
        }
    }

}

