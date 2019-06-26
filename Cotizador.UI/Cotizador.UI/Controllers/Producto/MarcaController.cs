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
    public class MarcaController : Controller
    {
        // GET: Marca
        BDConnectModel marcas = new BDConnectModel();
        public ActionResult Index()
        {
            return View("Marca", marcas.Marca.ToList());
        }

        [HttpPost] //Protocolo para guardar datos
        public ActionResult Insert([Bind(Include = "Clave,Nombre,Estatus")] Marca m)
        {
            if (ModelState.IsValid) //Si concuerda con el modelo
            {
                marcas.Marca.Add(m);
                marcas.SaveChanges();
                var data = marcas.Marca.Select(p => new { p.MarcaId, p.Clave, p.Nombre, p.Estatus });
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
            var data = marcas.Marca.Select(p => new { p.MarcaId, p.Clave, p.Nombre, p.Estatus }).Where(p => p.MarcaId == id);
            //Categoria editar = categorias.Categoria.Find(id);
            if (data == null)
            {
                return Json("'data' : 'error' + HttpNotFound()");
            }
            return Json(data);
        }
        [HttpPost]
        public ActionResult Update([Bind(Include = "MarcaId , Clave,Nombre,Estatus")] Marca m)
        {
            if (ModelState.IsValid)
            {
                marcas.Entry(m).State = EntityState.Modified;
                marcas.SaveChanges();
                var data = marcas.Marca.Select(p => new { p.MarcaId, p.Clave, p.Nombre, p.Estatus });
                return Json(data);
            }
            return Json(false);
        }
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            Marca mar = marcas.Marca.Find(id);
            if (mar == null)
            {
                return HttpNotFound();
            }
            //var consult = Marca.Product.Where(p => p.MarcaId == id); //Consulto la id en la tabla donde se relaciona
            //if (consult.Count() == 0)
            //{
            //    marcas.Marca.Remove(mar);
            //    marcas.SaveChanges();
            //}
            marcas.Marca.Remove(mar);
            marcas.SaveChanges();
            return Json(id.ToString());
        }
    }
}