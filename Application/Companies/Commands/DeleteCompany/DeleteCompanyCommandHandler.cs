using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Companies.Commands.DeleteCompany
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Unit>
    {

        private readonly ICompanyRepository _repository;
        private readonly ILogger<DeleteCompanyCommandHandler> _logger;

        public DeleteCompanyCommandHandler(ICompanyRepository repository, ILogger<DeleteCompanyCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var company = await _repository.GetByIdAsync(request.Id);
                if (company == null)
                {
                    _logger.LogWarning(" Delete failed. Company not found with ID: {Id}", request.Id);
                    throw new Exception($"Company with ID {request.Id} not found.");
                }

                await _repository.DeleteAsync(request.Id);

                _logger.LogInformation("Company deleted with ID: {Id}", request.Id);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Error deleting company with ID: {Id}", request.Id);
                throw;
            }
        }
    }
}

