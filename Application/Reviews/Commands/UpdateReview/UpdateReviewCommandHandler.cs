using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ReviewDto>
    {
        private readonly IReviewRepository _repository;
        private readonly ILogger<UpdateReviewCommandHandler> _logger;

        public UpdateReviewCommandHandler(IReviewRepository repository, ILogger<UpdateReviewCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ReviewDto> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _repository.GetByIdAsync(request.Id);

            if (review == null)
            {
                _logger.LogWarning("Review with ID {Id} not found for update", request.Id);
                throw new Exception($"Review with ID {request.Id} not found.");
            }

            review.Title = request.Title;
            review.Comment = request.Comment;
            review.Rating = request.Rating;

            await _repository.UpdateAsync(review);

            _logger.LogInformation("Review updated: ID {Id}", review.Id);

            return new ReviewDto
            {
                Id = review.Id,
                Title = review.Title,
                Comment = review.Comment,
                Rating = review.Rating,
                CompanyId = review.CompanyId
            };
        }
    }

}

