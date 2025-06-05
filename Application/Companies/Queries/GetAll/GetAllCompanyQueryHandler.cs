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

namespace Application.Companies.Queries.GetAll
{
    public class GetAllCompanyQueryHandler : IRequestHandler<GetAllCompanyQuery, List<CompanyDto>>
    {
        private readonly ICompanyRepository _repository;
        private readonly ILogger<GetAllCompanyQueryHandler> _logger;

        public GetAllCompanyQueryHandler(ICompanyRepository repository, ILogger<GetAllCompanyQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<CompanyDto>> Handle(GetAllCompanyQuery request, CancellationToken cancellationToken)
        {
            var companies = await _repository.GetAllAsync();

            _logger.LogInformation("Retrieved {Count} companies.", companies.Count);

            var result = companies.Select(c => new CompanyDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            return result;
        }
    }
}
