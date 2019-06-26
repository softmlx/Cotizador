using Cotizador.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cotizador.UI.Controllers.Productos
{
    public class CostodeProductoController : Controller
    {
        // GET: CostodeProducto
        BDConnectModel costos = new BDConnectModel();
        public ActionResult Index()
        {
            return View("CostodeProducto", costos.InformacionComercialProducto.ToList());
        }
        public ActionResult agregarCosto()
        {
            return View("AgregarCosto");
        }
        public ActionResult editarCosto()
        {
            return View("EditarCosto");
        }
        /*[HttpPost]
        public ActionResult Insert([Bind(Include = "Costo,Precio,Margen,ProveedorId,MonedaId,ProductoId,Estatus")] InformacionComercialProducto cost)
        {
            if (ModelState.IsValid)
            {
                costos.InformacionComercialProducto.Add(cost);
                costos.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("AgregarCosto");
        }*/
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //InformacionComercialProducto editar = costos.InformacionComercialProducto.Find(id);
            /*if (editar == null)
            {
                return HttpNotFound();
            }*/
            return View("EditarCosto");//, editar);
        }
        /*[HttpPost]

        public ActionResult Update([Bind(Include = "InformacionComercialProductoId,Costo,Precio,Margen,ProveedorId,MonedaId,ProductoId,Estatus")] InformacionComercialProducto cos)
        {
            if (ModelState.IsValid)
            {
                costos.Entry(cos).State = System.Data.Entity.EntityState.Modified;
                costos.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("EditarCosto");
        }
        */
        public ActionResult Delete(Guid id)
        {
            InformacionComercialProducto c = costos.InformacionComercialProducto.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            costos.InformacionComercialProducto.Remove(c);
            costos.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}