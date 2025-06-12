using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reviews.Queries.GetById
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewDto?>
    {
        private readonly IReviewRepository _repository;
        private readonly ILogger<GetReviewByIdQueryHandler> _logger;

        public GetReviewByIdQueryHandler(IReviewRepository repository, ILogger<GetReviewByIdQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ReviewDto?> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var review = await _repository.GetByIdAsync(request.Id);

            if (review == null)
            {
                _logger.LogWarning("Review not found with ID: {Id}", request.Id);
                return null;
            }

            _logger.LogInformation("Fetched review with ID: {Id}", request.Id);

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

