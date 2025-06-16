using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.Login
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, UserDto?>
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<LoginUserQueryHandler> _logger;

        public LoginUserQueryHandler(IUserRepository repository, ILogger<LoginUserQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<UserDto?> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                _logger.LogWarning("Login failed for email: {Email}", request.Email);
                return null;
            }

            _logger.LogInformation("User logged in: {Email}", user.Email);

            return new UserDto
            {
                Email = user.Email,
                Name = user.Name,
                Role = user.Role
            };
        }
    }

}

