using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudFinal.Models
{
    public interface IRepositoryProducts
    {
        Task<Products> GetProduct(uint externalId);
        Task<Products> GetProductByCode(string code);
        Task<Products> GetProductbyBarcode(long barcode);
        Task<IEnumerable<Products>> GetProducts();
        Task<Products> AddProducts(Products Product);
        Task<Products> UpdateProduct(Products Product);
        Task DeleteProductse(uint externalId);
    }
}
