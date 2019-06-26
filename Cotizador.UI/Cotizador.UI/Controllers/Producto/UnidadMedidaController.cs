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
    public class UnidadMedidaController : Controller
    {
        // GET: UnidadMedida
        BDConnectModel unimed = new BDConnectModel();
        public ActionResult Index()
        {
            return View("UnidadMedida", unimed.UnidadMedida.ToList());
        }
        [HttpPost] //Protocolo para guardar datos
        public ActionResult Insert([Bind(Include = "Clave,Simbolo,Nombre,Estatus")] UnidadMedida uni)
        {
            if (ModelState.IsValid) //Si concuerda con el modelo
            {
                unimed.UnidadMedida.Add(uni);
                unimed.SaveChanges();
                var data = unimed.UnidadMedida.Select(p => new { p.UnidadMedidaId, p.Clave, p.Simbolo, p.Nombre, p.Estatus });
                return Json(data);
            }
            return Json(false); //Aquí retorno la vista 
        }
        [HttpPost]
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = unimed.UnidadMedida.Select(p => new { p.UnidadMedidaId, p.Clave, p.Simbolo, p.Nombre, p.Estatus }).Where(p => p.UnidadMedidaId == id);
            //Categoria editar = categorias.Categoria.Find(id);
            if (data == null)
            {
                return Json("'data' : 'error' + HttpNotFound()");
            }
            return Json(data);
        }
        [HttpPost]
        public ActionResult Update([Bind(Include = "UnidadMedidaId, Clave,Simbolo,Nombre,Estatus")] UnidadMedida uni)
        {
            if (ModelState.IsValid)
            {
                unimed.Entry(uni).State = EntityState.Modified;
                unimed.SaveChanges();
                var data = unimed.UnidadMedida.Select(p => new { p.UnidadMedidaId, p.Clave, p.Simbolo, p.Nombre, p.Estatus });
                return Json(data);
            }
            return Json(false);
        }
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            UnidadMedida u = unimed.UnidadMedida.Find(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            //var consult = unimed.Product.Where(p => p.UnidadMedidaId == id); //Consulto la id en la tabla donde se relaciona
            //if (consult.Count() == 0)
            //{
            //    unimed.UnidadMedida.Remove(u);
            //    unimed.SaveChanges();
            //}
            unimed.UnidadMedida.Remove(u);
            unimed.SaveChanges();
            return Json(id.ToString());
        }
    }
}