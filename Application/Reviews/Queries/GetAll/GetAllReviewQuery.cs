using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reviews.Queries.GetAll
{
    public class GetAllReviewQuery : IRequest<List<ReviewDto>>
    {
       
    }
}
