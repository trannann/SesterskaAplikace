using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Vsb.UrgentApp.API.App_Start
{
    public class RouteConfig
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {

            //// APP status resource: GET
            //// GET http://localhost:28888/v1/app/status
            //routes.MapHttpRoute(
            //    name: "Test",
            //    routeTemplate: "v1/app/status",
            //    defaults: new { controller = "Test" }
            //);
        }
    }
}