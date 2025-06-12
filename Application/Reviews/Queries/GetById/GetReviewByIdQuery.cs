using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reviews.Queries.GetById
{
    public class GetReviewByIdQuery : IRequest <ReviewDto>
    {
        public int Id { get; set; }

        public GetReviewByIdQuery(int id)
        {
            Id = id;
        }
    }
}

