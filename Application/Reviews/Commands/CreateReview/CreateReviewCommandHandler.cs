using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewDto>
    {
        private readonly IReviewRepository _repository;
        private readonly ILogger<CreateReviewCommandHandler> _logger;

        public CreateReviewCommandHandler(
            IReviewRepository repository,
            ILogger<CreateReviewCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = new Review
            {
                Title = request.Title,
                Comment = request.Comment,
                Rating = request.Rating,
                CompanyId = request.CompanyId
            };

            await _repository.AddAsync(review);

            _logger.LogInformation("Review created for company ID {CompanyId}", review.CompanyId);

            // Return as DTO
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
