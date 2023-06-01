
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CustomerManagementSystem.Areas.CustomerCRUD
{
    public class CustomerCRUDAreaRegistration:AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "UserCRUD";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
        //    context.MapRoute(
        //    "Customer_CRUD",
        //    "User/{action}",
        //    new { controller = "User", action = "AddUser" }
        //); 

            // ... other routes

            
            context.MapRoute(
                "Customer_CRUD",
                "User/{action}/{id}",
                new { controller = "User", action = "AddUser", id = UrlParameter.Optional });

           
        }
    }
}