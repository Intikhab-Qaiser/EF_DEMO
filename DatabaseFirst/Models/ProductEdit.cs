using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatabaseFirst.Models
{
    public class ProductEdit
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public int ProductCategoryID { get; set; }

        public string Category { get; set; }

        public int ProductModelID { get; set; }

        public string Model { get; set; }

        public decimal StandardCost { get; set; }

        public decimal ListPrice { get; set; }

        public DateTime SellStart { get; set; }
    }
}