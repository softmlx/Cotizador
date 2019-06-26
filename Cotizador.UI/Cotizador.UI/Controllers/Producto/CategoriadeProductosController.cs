using Cotizador.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cotizador.UI.Controllers.Productos
{
    public class CategoriadeProductosController : Controller
    {
        // GET: CategoriadeProductos

        BDConnectModel categorias = new BDConnectModel();

        public ActionResult Index()
        {
            return View("CategoriadeProductos", categorias.Categoria.ToList());
        }
        public ActionResult agregarCategoria()
        {
            return View("AgregarCategoria");
        }

        public ActionResult eliminarCategoria()
        {
            return View();
        }

        public ActionResult editarCategoria()
        {
            return View("EditarCategoria");
        }
        [HttpPost] //Protocolo para guardar datos
        public ActionResult Insert([Bind(Include = "Clave,Nombre,Estatus")] Categoria categoria)
        {
            if (ModelState.IsValid) //Si concuerda con el modelo
            {
                categorias.Categoria.Add(categoria);
                categorias.SaveChanges();
                var data = categorias.Categoria.Select(p => new { p.CategoriaId, p.Clave, p.Nombre, p.Estatus });
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
            var data = categorias.Categoria.Select(p => new { p.CategoriaId, p.Clave, p.Nombre, p.Estatus }).Where(p => p.CategoriaId == id);
            //Categoria editar = categorias.Categoria.Find(id);
            if (data == null)
            {
                return Json("'data' : 'error' + HttpNotFound()");
            }
            return Json(data);
        }
        [HttpPost]
        public ActionResult Update([Bind(Include = "CategoriaId , Clave,Nombre,Estatus")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                categorias.Entry(categoria).State = EntityState.Modified;
                categorias.SaveChanges();
                var data = categorias.Categoria.Select(p => new { p.CategoriaId, p.Clave, p.Nombre, p.Estatus });
                return Json(data);
            }
            return Json(false);
        }
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            Categoria cat = categorias.Categoria.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }
            var consult = categorias.Product.Where(p => p.CategoriaId == id); //Consulto la id en la tabla donde se relaciona
            if (consult.Count() == 0)
            {
                categorias.Categoria.Remove(cat);
                categorias.SaveChanges();
            }
            return Json(id.ToString());
        }
    }
}
