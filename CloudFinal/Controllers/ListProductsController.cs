using CloudFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudFinal.Controllers
{
    public class ListProductsController : Controller
    {
        private readonly IRepositoryProducts repositoryProducts;


        public ListProductsController(IRepositoryProducts repositoryProducts)
        {
         
            this.repositoryProducts = repositoryProducts;

        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {
            return View();
        }



        public async Task<IActionResult> EditAsync(int id)
    {           
            uint y = (uint)id;

            var result = await repositoryProducts.GetProduct(y); // return edit product to page
            if(result==null)
                return  BadRequest();
            return View(result);
    }

    }
}
