using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Companies.Queries.GetById
{
    public class GetCompanyByIdQuery : IRequest<CompanyDto>
    {
            public int Id { get; set; }

            public GetCompanyByIdQuery(int id)
            {
                Id = id;
            }
        }
    }

