using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesOnWeb.Models;

namespace SalesOnWeb.Models
{
    public class SalesOnWebContext : DbContext
    {
        public SalesOnWebContext (DbContextOptions<SalesOnWebContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
