using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;

namespace PortfolioGB.Models
{
    public class Portfolio
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
    }

    public class PortfolioDBContext : DbContext
    {
        public DbSet<Portfolio> Portfolios { get; set; }
    }
}