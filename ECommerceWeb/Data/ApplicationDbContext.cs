using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}