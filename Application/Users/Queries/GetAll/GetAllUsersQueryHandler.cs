using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<GetAllUsersQueryHandler> _logger;

        public GetAllUsersQueryHandler(IUserRepository repository, ILogger<GetAllUsersQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();

            _logger.LogInformation("Fetched {Count} users", users.Count);

            return users.Select(u => new UserDto
            {
                Email = u.Email,
                Name = u.Name,
                Role = u.Role
            }).ToList();
        }
    }
}

