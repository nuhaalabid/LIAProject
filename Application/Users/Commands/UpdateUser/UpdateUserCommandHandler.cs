using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(IUserRepository repository, ILogger<UpdateUserCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);

            if (user == null)
            {
                _logger.LogWarning("User with ID {Id} not found.", request.Id);
                throw new Exception($"User with ID {request.Id} not found.");
            }

            user.Email = request.Email;
            user.Name = request.Name;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Role = request.Role;

            await _repository.UpdateAsync(user);

            _logger.LogInformation("User updated: {Email}", user.Email);

            return new UserDto
            {
                Email = user.Email,
                Name = user.Name,
                Role = user.Role
            };

        }


    }
}

