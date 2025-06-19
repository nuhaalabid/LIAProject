using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Application.Users.Queries.Login.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.Login
{

    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, AuthResultDto?>
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<LoginUserQueryHandler> _logger;
        private readonly TokenHelper _tokenHelper;

        public LoginUserQueryHandler(
            IUserRepository repository,
            ILogger<LoginUserQueryHandler> logger,
            TokenHelper tokenHelper)
        {
            _repository = repository;
            _logger = logger;
            _tokenHelper = tokenHelper;
        }
        public async Task<AuthResultDto?> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                _logger.LogWarning("Login failed for email: {Email}", request.Email);
                return null;
            }

            var token = _tokenHelper.GenerateJwtToken(user);

            return new AuthResultDto
            {
                User = new UserDto
                {
                    Email = user.Email,
                    Name = user.Name,
                    Role = user.Role
                },
                Token = token
            };
        }
    }

}

