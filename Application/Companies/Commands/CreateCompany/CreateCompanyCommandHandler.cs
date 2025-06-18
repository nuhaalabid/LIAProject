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

namespace Application.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
    {
        private readonly ICompanyRepository _repository;
        private readonly ILogger<CreateCompanyCommandHandler> _logger;

        public CreateCompanyCommandHandler(
            ICompanyRepository repository,
            ILogger<CreateCompanyCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var company = new Company
                {
                    Name = request.Name,
                    Description = request.Description
                };

                await _repository.AddAsync(company);

                _logger.LogInformation("Company created: {Name} (ID: {Id})", company.Name, company.Id);

                return new CompanyDto
                {
                    Id = company.Id,
                    Name = company.Name,
                    Description = company.Description
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create company: {Name}", request.Name);
                throw;
            }
        }
    }
}


