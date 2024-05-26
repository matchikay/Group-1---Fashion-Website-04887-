using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LaFlor.Models;

namespace LaFlor.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext (DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public DbSet<LaFlor.Models.Customer> Customer { get; set; } = default!;
        public DbSet<LaFlor.Models.Flowers> Flowers { get; set; } = default!;
        public DbSet<LaFlor.Models.Cart> Cart { get; set; } = default!;
        public DbSet<LaFlor.Models.Orders> Orders { get; set; } = default!;
    }
}
