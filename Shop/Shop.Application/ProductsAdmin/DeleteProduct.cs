﻿using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    // This feature should only be available for admin
    public class DeleteProduct
    {
        private ApplicationDbContext _context;

        public DeleteProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Do(int Id)
        {
            var Product = _context.Products.FirstOrDefault(x => x.Id == Id);
            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();

            return true;
        }
    }

    
}