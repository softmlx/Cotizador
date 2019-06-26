using Cotizador.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cotizador.UI.Controllers.Proveedor
{
    public class ProveedorController : Controller
    {
        BDConnectModel i = new BDConnectModel();

        // GET: Proveedor
        public ActionResult Index()
        {
            ViewBag.List = i.Pai.ToList();
            return View("Proveedor", i.Proveedor.ToList());
        }
        [HttpPost]
        public ActionResult Insert([Bind(Include = "MunicipioId,EstadoId,PaiId,Estatus")] Models.UbicacionGeografica ubicacion, [Bind(Include = "Calle,NumeroExterior,NumeroInterior,Colonia,Ciudad,CodigoPostal,UbicacionGeograficaId,Estatus")] Direccion direccion,/* [Bind(Include = "Titulo,Nombre,Apellidos,Telefono,Extension,Celular,CorreoElectronico,DireccionId,Puesto,Estatus")] Models.ContactoProveedor contactoproveedor,*/ ICollection<Guid> ids, [Bind(Include = "Nombre,ContactoProveedorId,Estatus")] Models.Proveedor proveedor, InformacionFiscal info, [Bind(Include = "RazonSocial,RFC,DireccionId,Estatus")] Models.InformacionFiscal informacionFiscal)
        {
            if (ModelState.IsValid)
            {

                Direccion dir = new Direccion();
                Models.Proveedor p = new Models.Proveedor();
                //Models.ContactoProveedor contprov = new Models.ContactoProveedor();
                Models.InformacionFiscal infis = new Models.InformacionFiscal();

                i.UbicacionGeografica.Add(ubicacion);
                i.SaveChanges();

                dir.Calle = direccion.Calle;
                dir.NumeroExterior = direccion.NumeroExterior;
                dir.NumeroInterior = direccion.NumeroInterior;
                dir.Colonia = direccion.Colonia;
                dir.Ciudad = direccion.Ciudad;
                dir.CodigoPostal = direccion.CodigoPostal;
                dir.UbicacionGeograficaId = ubicacion.UbicacionGeograficaId;
                i.Direccion.Add(dir);
                i.SaveChanges();


                /*contprov.Titulo = contactoproveedor.Titulo;
                contprov.Nombre = contactoproveedor.Nombre;
                contprov.Apellidos = contactoproveedor.Apellidos;
                contprov.Telefono = contactoproveedor.Telefono;
                contprov.Extension = contactoproveedor.Extension;
                contprov.Celular = contactoproveedor.Celular;
                contprov.CorreoElectronico = contactoproveedor.CorreoElectronico;
                contprov.Puesto = contactoproveedor.Puesto;
                contprov.Estatus = contactoproveedor.Estatus;
                i.ContactoProveedor.Add(contprov);
                i.SaveChanges();*/

                proveedor.Nombre = proveedor.Nombre;
                //proveedor.ContactoProveedorId = contprov.ContactoProveedorId;
                proveedor.Estatus = proveedor.Estatus;

                i.Proveedor.Add(proveedor);
                i.SaveChanges();

                infis.RazonSocial = informacionFiscal.RazonSocial;
                infis.RFC = informacionFiscal.RFC;
                infis.DireccionId = dir.DireccionId;
                infis.Estatus = dir.Estatus;
                i.InformacionFiscal.Add(infis);
                i.SaveChanges();

                InformacionFiscalProveedor inf = new InformacionFiscalProveedor();
                //inf.ProveedorId = proveedor.ProveedorId;
                i.InformacionFiscalProveedores.Add(inf);
                i.SaveChanges();


                var result = i.Proveedor.Select(c => new { c.Nombre, c.Estatus });
                return Json(result);
                //return RedirectToAction("Index");
            }
            return Json(false);
            //return View("Clientes");
        }

        public ActionResult loadStates(Guid paiId)
        {
            return Json(i.Estado.Where(s => s.PaisId == paiId).Select(s => new { EstadoId = s.EstadoId, nombre = s.Nombre }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadMunicipio(Guid estadoId)
        {
            return Json(i.Municipio.Where(s => s.EstadoId == estadoId).Select(e => new { MunicipioId = e.MunicipioId, Nombre = e.Nombre }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadStatesI(Guid paiId)
        {
            return Json(i.Estado.Where(s => s.PaisId == paiId).Select(s => new { EstadoId = s.EstadoId, Nombre = s.Nombre }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadMunicipioI(Guid estadoId)
        {
            return Json(i.Municipio.Where(s => s.EstadoId == estadoId).Select(e => new { MunicipioId = e.MunicipioId, Nombre = e.Nombre }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}