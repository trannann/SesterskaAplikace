using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Vsb.UrgentApp.API.CastleWindsor;
using Vsb.UrgentApp.Common.Helpers;
using Vsb.UrgentApp.Infrastructure;

namespace Vsb.UrgentApp.API
{
    public static class WebApiConfig
    {
		public static void Init()
		{
			Infrastructure.Db.FireBirdConnection.InitializeFirebird();

			ConfigureJsonFormatter();

			// castle windsor
			IWindsorContainer container = new WindsorContainer();
			container.Install(FromAssembly.This());

			// register components
			ComponentRegistrar.AddComponentsTo(container);

			// service locator
			ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));

			// dependency resolver
			GlobalConfiguration.Configuration.DependencyResolver = new CastleWindsor.DependencyResolver(container.Kernel);
		}


		public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Auth",
                routeTemplate: "v1/auth",
                defaults: new { controller = "Auth" }
            );

            config.Routes.MapHttpRoute(
                 name: "Patient",
                 routeTemplate: "v1/patients",
                 defaults: new { controller = "Patient" }
             );

            config.Routes.MapHttpRoute(
                 name: "Tag",
                 routeTemplate: "v1/tags",
                 defaults: new { controller = "Tag" }
             );

            config.Routes.MapHttpRoute(
                 name: "TagEvent",
                 routeTemplate: "v1/tagevents",
                 defaults: new { controller = "TagEvent" }
             );

            config.Routes.MapHttpRoute(
                 name: "TagRegistration",
                 routeTemplate: "v1/tagregistrations",
                 defaults: new { controller = "TagRegistration" }
             );

			config.Routes.MapHttpRoute(
                 name: "Test",
                 routeTemplate: "v1/test",
                 defaults: new { controller = "Test" }
             );
        }

        private static void ConfigureJsonFormatter()
        {
            var cfg = GlobalConfiguration.Configuration;

            // remove other formatters (the service uses only JSON response)
            // removed: XmlMediaTypeFormatter, JsonMediaTypeFormatter, FormUrlEncodedMediaTypeFormatter, JQueryMvcFormUrlEncodedFormatter
            cfg.Formatters.Clear();
            cfg.Formatters.Add(new JsonMediaTypeFormatter());

            var formatterSettings = cfg.Formatters.JsonFormatter.SerializerSettings;

            FileHelper.SetUpJsonSerializerSettings(formatterSettings);
        }
    }
}
