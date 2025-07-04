using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.AddUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserRepository repository, ILogger<CreateUserCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = HashPassword(request.Password);

            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                PasswordHash = hashedPassword,
                Role = request.Role
            };

            await _repository.AddAsync(user);

            _logger.LogInformation("User created: {Email}", user.Email);

            return new UserDto
            {
                Email = user.Email,
                Name = user.Name,
                Role = user.Role
            };
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }

}

