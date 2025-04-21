using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Infrastructure.Persistence.Configurations.Constants
{
    public class ExpirianRequest
    {
        //Expiriean Request token
        public const string REQUEST_TOKEN_URL_UAT = "https://uat-api.experian.com.pe/iam/oauth2/v1/basic/token";
        public const string REQUEST_TOKEN_URL_PRODUCTION = "https://api.experian.com.pe/iam/oauth2/v1/basic/token";
        public const string HEADER_CLIENT_ID = "";
        public const string HEADER_CLIENT_SECRET = "";




        //Experian Web Service
        public const string WEB_SERVICE_URL_UAT = "https://uat-api.experian.com.pe/modular/credit-history/v1/hdc/gethistory";
        public const string WEB_SERVICE_URL_PRODUCTION = "https://api.experian.com.pe/modular/credit-history/v1/hdc/gethistory";
        public const string WEB_SERVICE_HEADER_GX_EMAIL = "";
        public const string WEB_SERVICE_HEADER_GX_KEY = "";
        public const string WEB_SERVICE_HEADER_GX_USER = "";




    }
}
