using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }                
        public int CompanyId { get; set; }        
        public string Title { get; set; } = "";
        public string Comment { get; set; } = "";
        public int Rating { get; set; }  // 1 till 5

    }
}
