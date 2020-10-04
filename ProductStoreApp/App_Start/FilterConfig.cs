﻿using ProductStoreApp.Filter;
using System.Web;
using System.Web.Mvc;

namespace ProductStoreApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
