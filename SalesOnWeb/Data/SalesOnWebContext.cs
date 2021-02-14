using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesOnWeb.Models;

namespace SalesOnWeb.Data
{
    public class SalesOnWebContext : DbContext
    {
        public SalesOnWebContext (DbContextOptions<SalesOnWebContext> options)
            : base(options)
        {
        }

        public DbSet<SalesOnWeb.Models.Department> Department { get; set; }
    }
}
