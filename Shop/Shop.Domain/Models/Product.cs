using Shop.Domain.Enums;
using System.Collections.Generic;

namespace Shop.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get;set; }
        public decimal Value { get; set; }

        public ICollection<Stock> Stock { get; set;}
    }
}
