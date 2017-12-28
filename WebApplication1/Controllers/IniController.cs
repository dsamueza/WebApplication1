using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.@base;
namespace WebApplication1.Controllers
{
    public class IniController : Controller
    {

        private consulta c = new consulta();
        //
        // GET: /Ini/
        public ActionResult Index()
      {
            return View();
        }

            public ActionResult img(string key,string uri)
            {

        
                return View();
        }
          
	}
}