using System;
using System.Collections.Generic;
using System.Text;
using WomegaWebApp.Database;
using WomegaWebApp.Domain.Models;

namespace WomegaWebApp.Application.Products
{
    public class CreateProduct
    {
        private ApplicationDbContext _context;

        public CreateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Do(int Id, string Name, string Description)
        {
            _context.Products.Add(new Product
            {
                Id = Id,
                Name = Name,
                Description = Description
            });
        }
    }
}
