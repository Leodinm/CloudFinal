using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudFinal.Erpservices
{
    public class ErpServiceApiExceptions:Exception
    {
       public ErpServiceApiExceptions()
        {

        }
       public ErpServiceApiExceptions(string message) : base($"ErpService responded with {message}")
        {

        }

    }
}
