using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteReviewCommand(int id)
        {
            Id = id;
        }

    }
}
