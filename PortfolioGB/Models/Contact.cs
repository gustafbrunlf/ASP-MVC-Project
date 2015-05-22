using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioGB.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}