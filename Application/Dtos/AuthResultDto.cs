using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class AuthResultDto
    {
        public UserDto User { get; set; } = new();
        public string Token { get; set; } = "";
    }
}
