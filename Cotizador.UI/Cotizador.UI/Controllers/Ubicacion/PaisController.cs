using Cotizador.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cotizador.UI.Controllers.Ubicacion
{
    // GET: Permisos
    public class PaisController : Controller
    {
        BDConnectModel i = new BDConnectModel();
        public ActionResult Index() {
            var datos = new List<Pai>();//i.Pai.ToList();
            using (var db = new BDConnectModel())
            {
                datos = db.Pai.ToList();
            }

                return View("Paises", datos);
        }
        [HttpPost]
        public ActionResult Insert([Bind(Include = "Clave,Nombre")] Pai Pai) {
            if (ModelState.IsValid)
            {
                i.Pai.Add(Pai);
                i.SaveChanges();
                var result = i.Pai.Select(p => new { p.PaiId, p.Clave, p.Nombre });
                return Json(result);
            }
            return Json(false);
        }
        [HttpPost]
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = i.Pai.Select(p => new { p.PaiId, p.Clave, p.Nombre }).Where(p => p.PaiId == id);
            if (result == null)
            {
                return Json("'result' : 'error' + HttpNotFound()");
            }
            return Json(result);
        }
    }
}