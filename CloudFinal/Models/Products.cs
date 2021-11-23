using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CloudFinal.Models
{
    /*All Validation Maked on fornt-end dosent used dataannotations */
    public class Products
    {
        
        [Key]
        public uint externalId { get; set; }
       
        public string code { get; set; }
      
        public string description { get; set; }
        
        public string name { get; set; }
       
        public long barcode { get; set; }
       
        public decimal price { get; set; }
        
        public decimal wholesalePrice { get; set; }
       
        public int discount { get; set; }

    }
}
