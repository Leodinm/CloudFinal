using System;
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

        private readonly IHttpClientFactory _clientFactory;

        public Erpservice(IHttpClientFactory clientFactory)     //DI httpclient
        {
            this._clientFactory = clientFactory;


        }


        public async Task<List<Products>> GetproductsErp()
        {


            //jsonclassproduct.Production myvar = new jsonclassproduct.Production();

           

                try
                {
                var client = _clientFactory.CreateClient("ErpService");
                var response = await client.GetAsync("");

                
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                        var encode = Encoding.GetEncoding("Windows-1253");                //define text econding

                        using (var responseStream = await response.Content.ReadAsStreamAsync())
                        using (var deflateStream = new GZipStream(responseStream, CompressionMode.Decompress)) //deGzip

                        using (var streamReader = new StreamReader(deflateStream, encode, true))
                        {
                            var str = streamReader.ReadToEnd();

                            var jsonData = JsonConvert.SerializeObject(str);


                            var myvar = JsonConvert.DeserializeObject<jsonclassproduct.Production>(str);

                            if (!myvar.Success)         //if does not succes
                            {
                                if (myvar.Error != null)
                                {
                                    throw new ErpServiceApiExceptions(myvar.Error);         // if api define the error
                                }
                                else
                                    throw new ErpServiceApiExceptions("No data avalible");


                            }

                            else
                                return (myvar.Products);
                        }

                    

                }
                catch (Exception ex)
                {           // unknown exception error message  (connect problem decompress or something else)
                    throw new ErpServiceApiExceptions(ex.Message);


                }
                //}



            

        }

    }
}