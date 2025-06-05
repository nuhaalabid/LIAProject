using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Unit>
    {
        private readonly ICompanyRepository _repository;
        private readonly ILogger<UpdateCompanyCommandHandler> _logger;

        public UpdateCompanyCommandHandler(
            ICompanyRepository repository,
            ILogger<UpdateCompanyCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var company = await _repository.GetByIdAsync(request.Id);

                if (company == null)
                {
                    _logger.LogWarning(" Company with ID {Id} not found for update.", request.Id);
                    throw new Exception($"Company with ID {request.Id} not found.");
                }

                company.Name = request.Name;
                company.Description = request.Description;

                await _repository.UpdateAsync(company);

                _logger.LogInformation("Company updated: ID {Id}, Name {Name}", company.Id, company.Name);

                return Unit.Value ;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating company with ID {Id}", request.Id);
                throw;
            }
        }
    }
}

