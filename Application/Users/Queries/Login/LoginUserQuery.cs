using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.Login
{
    public class LoginUserQuery :IRequest<UserDto>
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

        public LoginUserQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }


}
