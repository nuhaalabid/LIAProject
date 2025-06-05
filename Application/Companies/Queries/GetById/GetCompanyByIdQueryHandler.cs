using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Companies.Queries.GetById
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDto?>
    {
        private readonly ICompanyRepository _repository;
        private readonly ILogger<GetCompanyByIdQueryHandler> _logger;

        public GetCompanyByIdQueryHandler(ICompanyRepository repository, ILogger<GetCompanyByIdQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<CompanyDto?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _repository.GetByIdAsync(request.Id);

            if (company == null)
            {
                _logger.LogWarning("Company not found with ID: {Id}", request.Id);
                return null;
            }

            _logger.LogInformation("Found company with ID: {Id}", request.Id);

            return new CompanyDto
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description
            };
        }
    }
}
