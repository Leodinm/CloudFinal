﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.IO.Compression;
using CloudFinal.Models;
using Microsoft.AspNetCore.Hosting;

namespace CloudFinal.Erpservices
{
    
    public class Erpservice : IErpservices
    {

        private readonly static string UrlErp = "http://pharmacyonenew.oncloud.gr/s1services/JS/updateItems/cloudOnTest"; //deifne url
        [Obsolete]
        private IHostingEnvironment _env; // this is for local use  demo/txt

        [Obsolete]
        public Erpservice(IHostingEnvironment env)
        {
            _env = env;
        }

        public  async Task<List<Products>> GetproductsErp()
        {
            

        jsonclassproduct.Production myvar = new jsonclassproduct.Production();

            using (var httpClient = new HttpClient())
            {
               

                try
                { 

                using (var response = await httpClient.GetAsync(UrlErp)) //http client connect to api

                {
                    //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    //var encode = Encoding.GetEncoding("Windows-1253");                //define text econding

                    //    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    //    using (var deflateStream = new GZipStream(responseStream, CompressionMode.Decompress)) //deGzip
                            var path = Path.Combine(_env.ContentRootPath, "Demo/myjson.txt");
                        using (var streamReader = new StreamReader(path))
                        //using (var streamReader = new StreamReader(deflateStream, encode, true))
                        {
                            var str = streamReader.ReadToEnd();

                            var jsonData = JsonConvert.SerializeObject(str);  


                            myvar = JsonConvert.DeserializeObject<jsonclassproduct.Production>(str); 

                            if (!myvar.Success)         //if dosent succes
                            {
                                if(myvar.Error!=null)
                                {
                                    throw new ErpServiceApiExceptions(myvar.Error);         // if api define the error
                                }else
                                    throw new ErpServiceApiExceptions("No data avalible");


                            }



                        }

                    }
                }
                catch (Exception ex)
                {           // unknown exception error message  (connect problem decompress or something else)
                    throw  new ErpServiceApiExceptions(ex.Message);


                }
                //}

                return (myvar.Products); // return list products

            }

        }

    }
}