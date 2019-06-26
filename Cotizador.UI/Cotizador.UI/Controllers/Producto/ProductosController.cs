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
    public class ProductosController : Controller
    {
        // GET: Productos
        BDConnectModel produ = new BDConnectModel();

        public ActionResult Index()
        {
            ViewBag.ListMarca = produ.Marca.ToList();
            ViewBag.ListaUnimedJson = Json(produ.UnidadMedida.ToList()).ToString();
            ViewBag.ListUnimed = produ.UnidadMedida.ToList();
            ViewBag.ListCategorias = produ.Categoria.ToList();
            return View("Productos", produ.Product.ToList());
        }
        
        [HttpPost]
        public ActionResult Insert([Bind(Include = "Clave,Nombre,DescripcionCorta,DescripcionCompleta,modelo,NumeroSerie,CodigoBarras,CategoriaId,MarcaId,UnidadMedidaId,Estatus")] Product producto)
        {
            if (ModelState.IsValid)
            {
                produ.Product.Add(producto);
                produ.SaveChanges();
                var data = produ.Product.Select(p => new { p.ProductoId, p.Clave, p.Nombre, p.DescripcionCorta, p.DescripcionCompleta, p.modelo, p.NumeroSerie, p.CodigoBarras, p.CategoriaId, p.MarcaId, p.UnidadMedidaId, p.Estatus  });
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
            var data = produ.Product.Select(p => new { p.ProductoId, p.Clave, p.Nombre, p.DescripcionCorta, p.DescripcionCompleta, p.modelo, p.NumeroSerie, p.CodigoBarras, p.CategoriaId, p.MarcaId, p.UnidadMedidaId, p.Estatus }).Where(p => p.ProductoId == id);
            if (data == null)
            {
                return Json("'data' : 'error' + HttpNotFound()");
            }
            return Json(data);
        }
        [HttpPost]
        public ActionResult Update([Bind(Include = "ProductoId,Clave,Nombre,DescripcionCorta,DescripcionCompleta,modelo,NumeroSerie,CodigoBarras,CategoriaId,MarcaId,UnidadMedidaId,Estatus")] Product produc)
        {
            if (ModelState.IsValid)
            {
                produ.Entry(produc).State = EntityState.Modified;
                produ.SaveChanges();
                var data = produ.Product.Select(p => new { p.ProductoId, p.Clave, p.Nombre, p.DescripcionCorta, p.DescripcionCompleta, p.modelo, p.NumeroSerie, p.CodigoBarras, p.CategoriaId, p.MarcaId, p.UnidadMedidaId, p.Estatus });
                return Json(data);
            }
            return Json(false);
        }
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            //Console.WriteLine(id);
            Product pr = produ.Product.Find(id);
            if (pr == null)
            {
                return HttpNotFound();
            }
            //var consultMarca = produ.Marca.Where(p => p.MarcaId == id);
            //var consultCategoria = produ.Categoria.Where(p => p.CategoriaId == id);
            //var consultMedida = produ.UnidadMedida.Where(p => p.UnidadMedidaId == id);
            //if (consultCategoria.Count() == 0)
            //{
                //produ.Product.Remove(pro);
                //produ.SaveChanges();
            //}
            produ.Product.Remove(pr);
            produ.SaveChanges();
            return Json(id.ToString());
        }
    }
}
