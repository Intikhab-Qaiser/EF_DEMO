using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatabaseFirst.Models
{
    public class ProductsList
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public string Number { get; set; }

        public string Category { get; set; }

        public string Model { get; set; }

        public decimal StandardCost { get; set; }

        public decimal ListPrice { get; set; }
    }
}