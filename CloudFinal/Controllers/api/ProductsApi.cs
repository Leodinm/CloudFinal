using CloudFinal.Erpservices;
using CloudFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudFinal.Controllers.api
{
    [Route("api/[controller]")]    //api/ProductsApi
    [ApiController]
    public class ProductsApi : ControllerBase
    {

        private readonly IRepositoryProducts repositoryProducts;
        private readonly IErpservices Erpservices;


        public ProductsApi(IRepositoryProducts repositoryProducts, IErpservices Erpservices) // DI Erpservices,repositoryProducts
        {
            this.repositoryProducts = repositoryProducts;
            this.Erpservices = Erpservices;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                return Ok(await repositoryProducts.GetProducts());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }




        [HttpGet, Route("ErpService")]                           //api/ProductsApi/ErpService
        /* insert by erpservice all products where (externalId ,code,barcode )does not exist  */
        public async Task<IActionResult> GetErpService()
        {
            try
            { 
            var erp = await Erpservices.GetproductsErp();

                foreach (var Product in erp.ToList())
                {
                    var Produ = await repositoryProducts.GetProduct(Product.externalId);

                    var ProductTFindbyCode = await repositoryProducts.GetProductByCode(Product.code);
                    var ProductFindbyBarcode = await repositoryProducts.GetProductbyBarcode(Product.barcode);

                    /*checking existing caulse */
                    if (!(Produ != null || ProductTFindbyCode != null || ProductFindbyBarcode != null))
                    {
                        var ProductTocreate = await repositoryProducts.AddProducts(Product);

                    }
                    else erp.Remove(Product);


                    



                }
                return Ok(erp.Count().ToString() +"  Products Updates By ErpServices");


            }
            catch (Exception ex)
            {
               

               
                return StatusCode(StatusCodes.Status500InternalServerError,
                     ex.Message);

            }


           
        }



        [HttpPost]
        /* insert by a Product  where (externalId ,code,barcode )does not exist  */
        public async Task<IActionResult> CreateProducts( Products Product)
        {
            try
            {
                if (Product == null)
                {
                    ModelState.AddModelError("Product", "Give A Product Correct"); // null Product
                    return BadRequest(ModelState);
                }

                var Produ = await repositoryProducts.GetProduct(Product.externalId);

                var ProductTFindbyCode = await repositoryProducts.GetProductByCode(Product.code);
                var ProductFindbyBarcode = await repositoryProducts.GetProductbyBarcode(Product.barcode);

                /*checking if allready existing */
                if (Produ != null)
                {
                    ModelState.AddModelError("Product", "externalId  already in use");
                    return BadRequest(ModelState);
                }
                if (ProductTFindbyCode != null)
                {
                    ModelState.AddModelError("Product", "Code  already in use");
                    return BadRequest(ModelState);
                }

                if (ProductFindbyBarcode != null)
                {
                    ModelState.AddModelError("Product", "Barcode  already in use");
                    return BadRequest(ModelState);
                }


                var ProductTocreate = await repositoryProducts.AddProducts(Product);

                return Ok(ProductTocreate);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProducts( Products Product)
        {
            try
            {
               

               

                var ProductToUpdate = await repositoryProducts.GetProduct(Product.externalId);

                var ProductToUpdatebyCode = await repositoryProducts.GetProductByCode(Product.code);
                var ProductToUpdatebyBarcode = await repositoryProducts.GetProductbyBarcode(Product.barcode);

              


                if (ProductToUpdate == null)
                {
                    ModelState.AddModelError("Product", "ExternalId  Cant found");
                    return BadRequest(ModelState);
                    
                }


                if (ProductToUpdatebyCode != null && Product.externalId != ProductToUpdatebyCode.externalId) //if code exist to another product
                {
                    ModelState.AddModelError("Product", "Code  already in use");
                    return BadRequest(ModelState);

                }


                if (ProductToUpdatebyBarcode != null && Product.externalId != ProductToUpdatebyBarcode.externalId) //if BArcode exist to another product
                {
                    ModelState.AddModelError("Product", "BArcode  already in use");
                    return BadRequest(ModelState);

                }






                return Ok(await repositoryProducts.UpdateProduct(Product));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Product record");
            }
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProducts(int id)            //getting single product
        {
            try
            {
                uint y = (uint)id;

                var result = await repositoryProducts.GetProduct(y);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }







        [HttpDelete("{id:int}")]        //delte single product serach and delete with external id only
        public async Task<IActionResult> DeleteProducts(uint id)
        {
            try
            {
                var productToDelete = await repositoryProducts.GetProduct(id);

                if (productToDelete == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }

                await repositoryProducts.DeleteProductse(id);

                return Ok($"Employee with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting employee record");
            }
        }











    }
}
