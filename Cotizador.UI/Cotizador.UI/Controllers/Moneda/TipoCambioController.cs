using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cotizador.UI.Models;
using Cotizador.UI.Models.ViewModels;

namespace Cotizador.UI.Controllers
{
    public class TipoCambioController : Controller
    {
        private BDConnectModel db = new BDConnectModel();
        
        

        // GET: TipoCambio
        public ActionResult Index()
        {
            ViewBag.List = db.Moneda.ToList();
            return View("ViewTipoCambio", db.TipoCambio.ToList());
        }

        /*public ActionResult Create()
        {
            ViewBag.List = db.TipoCambio.ToList();
            return View("InsTipoCambio");
        }*/

        [HttpPost]
        public ActionResult Insert([Bind(Include = "Moneda1Id, Moneda2Id, Factor")] Models.TipoCambio tipoCambio)
        {
            if (ModelState.IsValid && tipoCambio.Factor > 0)
            {
                db.TipoCambio.Add(tipoCambio);
                try
                {
                    db.SaveChanges();
                } catch(Exception e){
                    return Json(false);
                }
                
                object[] result = new object[2];
                result[0] = db.Moneda.Select(t => new { t.MonedaId, t.Nombre });
                result[1] = db.TipoCambio.Select(t => new { t.Moneda1Id, t.Moneda2Id, t.Factor });
                //var data = db.TipoCambio.Select(t => new { t.Moneda1Id, t.Moneda2Id, t.Factor, t.Moneda, t.Moneda1 });               
                return Json(result);
                
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult Edit(Guid id1, Guid id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            object[] result = new object[2];
            result[0] = db.Moneda.Select(t => new { t.MonedaId, t.Nombre });
            result[1] = db.TipoCambio.Select(t => new { t.Moneda1Id, t.Moneda2Id, t.Factor }).Where(t => id1 == t.Moneda1Id && id2 == t.Moneda2Id);
            

            
            if (result == null)
            {
                return Json("'result' : 'error' + HttpNotFound()");
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult Update([Bind(Include = "Moneda1Id, Moneda2Id, Factor")] Models.TipoCambio tipoCambio)
        {
            if(ModelState.IsValid && tipoCambio.Factor > 0)
            {
                db.Entry(tipoCambio).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return Json(false);
                }
                object[] result = new object[2];
                result[0] = db.Moneda.Select(t => new { t.MonedaId, t.Nombre });
                result[1] = db.TipoCambio.Select(t => new { t.Moneda1Id, t.Moneda2Id, t.Factor });
                return Json(result);
                
            }

            return Json(false);
        }

        [HttpPost]
        public ActionResult Delete(Guid id1, Guid id2)
        {

            if (id1 == null || id2 == null)
            {
                return HttpNotFound();
            }
            var tc = db.TipoCambio.Where(t => t.Moneda1Id == id1 && t.Moneda2Id == id2).ToList();
            db.TipoCambio.RemoveRange(tc);
            db.SaveChanges();
            var id = ""+id1.ToString()+":"+id2.ToString()+"";
            
            return Json(id.ToString());
        }
    }
}