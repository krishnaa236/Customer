using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagementAPI.Controllers
{
    public class HomeController : Controller
    {
        [Route("CheckId/{id}")]
        [HttpGet]
        public ActionResult CheckId(int id)
        {
            if (id < 10)
            {
                int a = 1;
                int b = 0;
                int c = a / b;
            }
            else if (id < 20)
            {
                throw new HttpException((int)System.Net.HttpStatusCode.BadRequest, "Bad Request");
            }
            else if (id < 30)
            {
                var response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new System.Net.Http.StringContent(string.Format("No Employee found with ID = {0}", 10)),
                    ReasonPhrase = "Employee Not Found"
                };

                throw new HttpException((int)System.Net.HttpStatusCode.BadRequest, "Employee Not Found");
            }

            return Json(id, JsonRequestBehavior.AllowGet);
        }
    }
}