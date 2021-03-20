using System;
using System.Collections.Generic;
using System.Text;

namespace WomegaWebApp.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }         //  TO store it in database
        public string Name { get; set; }    // Name of the Product
        public string Description { get; set; } // Description of the Product
        public decimal Value { get; set; }  // Value or Price or the Product

    }
}
