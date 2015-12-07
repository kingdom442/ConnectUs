using System.Configuration;
using System.Data.Entity.Migrations;
using System.Web.Http;
using System.Web.Routing;

namespace ConnectusMobileService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
    //    /// <summary>
    //    /// The _web API configuration
    //    /// </summary>
    //    private readonly WebApiConfig _webApiConfig;

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="WebApiApplication"/> class.
    //    /// </summary>
    //    public WebApiApplication()
    //    {
    //        _webApiConfig = new WebApiConfig();
    //    }

        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
            WebApiConfig.Register();
        }
    }
}