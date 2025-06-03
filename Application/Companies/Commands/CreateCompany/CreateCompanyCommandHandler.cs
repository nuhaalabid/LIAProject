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
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, int>
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

        public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
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

                return company.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create company: {Name}", request.Name);
                throw; 
            }
        }
    }
}


