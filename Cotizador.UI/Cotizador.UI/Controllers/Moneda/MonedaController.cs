using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Cotizador.UI.Models;

namespace Cotizador.UI.Controllers
{


    public class MonedaController : Controller
    {
        private BDConnectModel db = new BDConnectModel();

        // GET: Moneda
        public ActionResult Index()
        {
            return View("ViewMoneda", db.Moneda.ToList());
        }

        public ActionResult Create()
        {
            return View("InsMoneda");
        }

        public ActionResult Edited()
        {
            return View("UpdMoneda");
        }


        //SET: Moneda
        [HttpPost]
        public ActionResult Insert([Bind(Include ="Nombre,Simbolo,Clave,Valor,Estatus")] Models.Moneda moneda)
        {
            if (ModelState.IsValid && moneda.Valor > 0)
            {
                db.Moneda.Add(moneda);
                db.SaveChanges();
                var data = db.Moneda.Select(m => new { m.MonedaId, m.Nombre, m.Simbolo, m.Clave, m.Valor, m.Estatus});
                return Json(data);
                //return RedirectToAction("Index");
            }
            return Json(false);
            //return View("ViewMoneda", db.Moneda.ToList());
        }

        //Obtener los valores
        [HttpPost]
        public ActionResult Edit(Guid id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var result = db.Moneda.Select(m => new { m.MonedaId, m.Nombre, m.Simbolo, m.Clave, m.Valor, m.Estatus }).Where(m => m.MonedaId == id);
            if (result == null)
            {
                return Json("'result' : 'error' + HttpNotFound()");
            }
            return Json(result);
           
        }

        //Guardar cambios del edit
        [HttpPost]
        public ActionResult Update([Bind(Include = "MonedaId, Nombre, Simbolo, Clave, Valor, Estatus")] Models.Moneda moneda)
        {
            if (ModelState.IsValid && moneda.Valor > 0)
            {
                db.Entry(moneda).State = EntityState.Modified;
                db.SaveChanges();
                var data = db.Moneda.Select(m => new { m.MonedaId, m.Nombre, m.Simbolo, m.Clave, m.Valor, m.Estatus });
                return Json(data);
                //return RedirectToAction("Index");
            }
            return Json(false);
            //return View("InsMoneda");
        }

        //Delete
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            Models.Moneda moneda = db.Moneda.Find(id);
            if (moneda == null)
            {
                return HttpNotFound();
            }

            db.Moneda.Remove(moneda);
            try
            {
                db.SaveChanges();
                return Json(id.ToString());
            }
            catch (Exception e)
            {
                return Json(false);
            }
            

        }
    }
}