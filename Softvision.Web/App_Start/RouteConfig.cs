using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Softvision.All
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // To enable route attribute in controllers
            routes.MapMvcAttributeRoutes();

            //routes.MapRoute(
            //    name: "TextRoute",
            //    url: "{controller}/{action}/{aboutId}/{urlString}",
            //    defaults: new { controller = "Home", action = "Index", aboutId = UrlParameter.Optional, urlString = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "ArticleDetails",
                url: "Article/Details/{id}/{title}",
                defaults: new { controller = "Article", action = "Details", id = @"\d+" }
            );

            routes.MapRoute(
                name: "QuestionDetails",
                url: "Question/Details/{id}/{title}",
                defaults: new { controller = "Question", action = "Details", id = @"\d+" }
            );

            routes.MapRoute(
                name: "QuestionCategory",
                url: "Question/Category/{id}/{title}",
                defaults: new { controller = "Question", action = "Category", id = @"\d+" }
            );

            routes.MapRoute(
                name: "ArticleCategoryDetails",
                url: "Article/Category/{id}/{title}",
                defaults: new { controller = "Article", action = "Category", id = @"\d+" }
            );


            routes.MapRoute(
                name: "QuestionSubcategory",
                url: "Question/Subcategory/{id}/{title}",
                defaults: new { controller = "Question", action = "SubCategory", id = @"\d+" }
            );

            routes.MapRoute(
                name: "ArticleSubcategoryDetails",
                url: "Article/Subcategory/{id}/{title}",
                defaults: new { controller = "Article", action = "SubCategory", id = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "NotFound",
                "{*.}",
                new { controller = "Home", action = "Index" }
            );
        }
    }
}
