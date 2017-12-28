using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.@base;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class viewimagesController : Controller
    {
        private consulta c = new consulta();
        // GET: viewimages
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult load(string key, string uri)
        {

            if (key != null && uri != null)
            {
                if (!key.Equals("") & !uri.Equals(""))
                {
                    var model = c.getimag(key, uri);
                    return View(model);
                }
            }
            return View();
        }

        public ActionResult Validate(string key, string uri)
        {
      
            if (key != null && uri != null)
            {
                if (!key.Equals("") & !uri.Equals(""))
                {
                    var model = c.getimag(key, uri);

                    return View(model);
                }
            }
            return View();
        }
   
    [HttpPost]
        public JsonResult ValidateSave(string uri )
        {
         
            var rows = from x in c.GetVAlidate(uri)
                       select new
                       {
                           id = x.AggregateUri,
                           estado = x.typeObs,
                           observacion = x.description,
                         
                       };
            var jsondata = rows.ToArray();
            return Json(jsondata);
        }
        [HttpPost]
        public JsonResult ValidateSaveDt(string uri, string stat, string des)
        {
            bool val = false;
            if (stat.Equals("1")) {
                val = true;
            }
            ValidateModel model = new ValidateModel();
            var remoteIpAddress = Request.UserHostAddress;
            model.AggregateUri = uri; 
            
            model.typeObs = val;
            model.description = des;
            model.user_web = remoteIpAddress;

           var u= c.SAveVAlidate(model);
            return Json(u);
        }
        public ActionResult _PartialValidate(string uri)
        {
            var model = c.GetVAlidate(uri);
            return View(model);
        }
    }
}