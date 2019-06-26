using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cotizador.UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Cotizador
        public ActionResult Index()
        {
            return View();
        }
    }
}