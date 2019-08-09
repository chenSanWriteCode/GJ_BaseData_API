﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using GJ_BaseData_API.Filter;

namespace GJ_BaseData_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new IPAuthorAttribute());
            //config.EnableCors(new EnableCorsAttribute("http://117.34.118.30:65128", "*", "*"));
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
