using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Companies.Queries.GetAll
{
    public class GetAllCompanyQuery : IRequest<List<CompanyDto>>
    {

    }
}
