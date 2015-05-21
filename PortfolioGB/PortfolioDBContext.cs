using PortfolioGB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PortfolioGB
{
    public class PortfolioDBContext : DbContext
    {
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}