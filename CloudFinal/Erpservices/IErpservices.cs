using System;
using CloudFinal.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudFinal.Erpservices
{
   public interface IErpservices
    {
         Task <List<Products>> GetproductsErp();
    }
}
