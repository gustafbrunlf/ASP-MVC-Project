using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace PortfolioGB.Models
{
    public class Portfolio
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public string Link { get; set; }
        public virtual ICollection<FilePath> FilePaths { get; set; }
    }

    public class PortfolioDBContext : DbContext
    {
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }
    }
}