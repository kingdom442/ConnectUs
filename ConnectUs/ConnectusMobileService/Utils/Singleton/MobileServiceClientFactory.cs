using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.Utils.Singleton
{
    public sealed class MobileServiceClientFactory
    {
        static readonly MobileServiceClient _instance = new MobileServiceClient("http://localhost:62028/", "blunznkoenig");
        internal static MobileServiceClient Instance
        {
            get
            {
                return _instance;
            }
        }
        MobileServiceClientFactory()
        {
            // Initialize.
        }
    }

}