using CloudFinal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudFinal.Models
{

            /* all methods  Delte,update,Create,insert to SQL table Products*/
    public class RepositoryProducts : IRepositoryProducts
    {

        private ApplicationDbContext _context;

        public RepositoryProducts(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task DeleteProduct(uint externalId)
        {
            var deleteobj = await _context.Products
              .FirstOrDefaultAsync(e => e.externalId == externalId);

            if (deleteobj != null)
            {
                _context.Products.Remove(deleteobj);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Products> GetProduct(uint externalId)
        {
            return await _context.Products
               .FirstOrDefaultAsync(e => e.externalId == externalId);
        }

        public async Task<IEnumerable<Products>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Products> AddProducts(Products Product)
        {
           

            var result = await _context.Products.AddAsync(Product);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Products> UpdateProduct(Products Product)
        {
            var result = await _context.Products
               .FirstOrDefaultAsync(e => e.externalId == Product.externalId); // need find if exist first
            if (result != null)
            {

                result.name = Product.name;
                result.externalId = Product.externalId;
                result.code = Product.code;
                result.barcode = Product.barcode;
                result.description = Product.description;
                result.price = Product.price;
                result.wholesalePrice = Product.wholesalePrice;
                result.discount = Product.discount;
                await _context.SaveChangesAsync();
                return result;

            }
            return null;
        }

        public async Task<Products> GetProductByCode(string code)
        {
            return await _context.Products
               .FirstOrDefaultAsync(e => e.code == code);
        }

        public async Task<Products> GetProductbyBarcode(long barcode)
        {
            return await _context.Products
               .FirstOrDefaultAsync(e => e.barcode == barcode);
        }
    }
}
