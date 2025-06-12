using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public class DatabaseContext :DbContext
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
