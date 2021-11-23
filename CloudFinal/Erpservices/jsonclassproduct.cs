using CloudFinal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudFinal.Erpservices
{
    public class jsonclassproduct
    {


        public class Production
        {
            [JsonProperty("success")]
            public bool Success { get; set; }
            [JsonProperty("error")]
            public String Error { get; set; }

            [JsonProperty("data")]
            public List<Products> Products { get; set; }
        }


       
    }
}
