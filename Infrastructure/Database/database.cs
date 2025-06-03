using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public class database :DbContext
    {
        public database (DbContextOptions<database> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }

    }
}
