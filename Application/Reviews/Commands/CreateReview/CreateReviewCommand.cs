using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<ReviewDto>
    {
            public int CompanyId { get; set; }
            public string Title { get; set; } = "";
            public string Comment { get; set; } = "";
            public int Rating { get; set; }
        }
    }

