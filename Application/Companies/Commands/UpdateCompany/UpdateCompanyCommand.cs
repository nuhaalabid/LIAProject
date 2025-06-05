using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommand : IRequest <Unit>
    {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
        }
    }


