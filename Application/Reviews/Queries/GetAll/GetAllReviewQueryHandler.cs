using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reviews.Queries.GetAll
{
    public class GetAllReviewQueryHandler : IRequestHandler<GetAllReviewQuery, List<ReviewDto>>
    {
        private readonly IReviewRepository _repository;
        private readonly ILogger<GetAllReviewQueryHandler> _logger;

        public GetAllReviewQueryHandler(IReviewRepository repository, ILogger<GetAllReviewQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<ReviewDto>> Handle(GetAllReviewQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _repository.GetAllAsync();

            _logger.LogInformation("Fetched {Count} reviews", reviews.Count);

            return reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                Title = r.Title,
                Comment = r.Comment,
                Rating = r.Rating,
                CompanyId = r.CompanyId
            }).ToList();
        }
    }
}

