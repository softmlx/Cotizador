using Cotizador.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cotizador.UI.Controllers.Producto
{
    public class InformacionComercialProductoController : Controller
    {
        // GET: InformacionComercialProducto
        BDConnectModel inf = new BDConnectModel();
        public ActionResult Index()
        {
            ViewBag.ListaProveedor = inf.Proveedor.ToList();
            ViewBag.ListaMoneda = inf.Moneda.ToList();
            ViewBag.ListaProducto = inf.Product.ToList();
            return View("InformacionComercialProducto",inf.InformacionComercialProducto.ToList());
        }
        [HttpPost]
        public ActionResult Insert([Bind(Include = "Costo,Precio,Margen,ProveedorId,MonedaId,ProductoId,Estatus")] InformacionComercialProducto icp)
        {
            if (ModelState.IsValid)
            {
                inf.InformacionComercialProducto.Add(icp);
                inf.SaveChanges();
                var data = inf.InformacionComercialProducto.Select(p => new {p.InformacionComercialProductoId, p.Costo, p.Precio, p.Margen, p.ProveedorId, p.MonedaId, p.ProductoId, p.Estatus });
                return Json(data);
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
            var data = inf.InformacionComercialProducto.Select(p => new { p.InformacionComercialProductoId, p.Costo, p.Precio, p.Margen, p.ProveedorId, p.MonedaId, p.ProductoId, p.Estatus }).Where(p => p.InformacionComercialProductoId == id);
            if (data == null)
            {
                return Json("'data' : 'error' + HttpNotFound()");
            }
            return Json(data);
        }
        [HttpPost]
        public ActionResult Update([Bind(Include = "InformacionComercialProductoId,Costo,Precio,Margen,ProveedorId,MonedaId,ProductoId,Estatus")] InformacionComercialProducto icp)
        {
            if (ModelState.IsValid)
            {
                inf.Entry(icp).State = EntityState.Modified;
                inf.SaveChanges();
                var data = inf.InformacionComercialProducto.Select(p => new { p.InformacionComercialProductoId, p.Costo, p.Precio, p.Margen, p.ProveedorId, p.MonedaId, p.ProductoId, p.Estatus });
                return Json(data);
            }
            return Json(false);
        }
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            //Console.WriteLine(id);
            InformacionComercialProducto i = inf.InformacionComercialProducto.Find(id);
            if (i == null)
            {
                return HttpNotFound();
            }
            //var consultProducto = inf.Product.Where(p => p.ProductoId == id);
            //if (consultProducto.Count() == 0)
            //{
            //inf.InformacionComercialProducto.Remove(i);
            //inf.SaveChanges();
            //}
            inf.InformacionComercialProducto.Remove(i);
            inf.SaveChanges();
            return Json(id.ToString());
        }
    }
}