using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Cotizador.UI.Models;
using Newtonsoft.Json;

namespace Cotizador.UI.Controllers.Cliente
{
    public class ClienteController : Controller
    {
        BDConnectModel connectModel = new BDConnectModel();
        // GET: Cliente
        public ActionResult Index()
        {
            ViewBag.List = connectModel.Pai.ToList();
            return View("Clientes", connectModel.Cliente.ToList());
        }

        [HttpPost]
        public ActionResult Insert(Models.Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                UbicacionGeografica ubicacionGeografica = new UbicacionGeografica();
                Direccion dir = new Direccion();
                Models.Cliente clien = new Models.Cliente();
                

                ubicacionGeografica.MunicipioId = cliente.Direccion.UbicacionGeografica.MunicipioId;
                ubicacionGeografica.EstadoId = cliente.Direccion.UbicacionGeografica.EstadoId;
                ubicacionGeografica.PaiId = cliente.Direccion.UbicacionGeografica.PaiId;
                connectModel.UbicacionGeografica.Add(ubicacionGeografica);
                connectModel.SaveChanges();

                
                dir.Calle = cliente.Direccion.Calle;
                dir.NumeroExterior = cliente.Direccion.NumeroExterior;
                dir.NumeroInterior = cliente.Direccion.NumeroInterior;
                dir.Colonia = cliente.Direccion.Colonia;
                dir.Ciudad = cliente.Direccion.Ciudad;
                dir.CodigoPostal = cliente.Direccion.CodigoPostal;
                dir.UbicacionGeograficaId = ubicacionGeografica.UbicacionGeograficaId;
                connectModel.Direccion.Add(dir);
                connectModel.SaveChanges();

                
                clien.Nombre = cliente.Nombre;
                clien.Telefono = cliente.Telefono;
                clien.Extension = cliente.Extension;
                clien.Celular = cliente.Celular;
                clien.DireccionId = dir.DireccionId;
                connectModel.Cliente.Add(clien);
                connectModel.SaveChanges();


                foreach (var contactoC in cliente.ContactoCliente)
                {
                    ContactoCliente contacto = new ContactoCliente();
                    contacto.Titulo = contactoC.Titulo;
                    contacto.Nombre = contactoC.Nombre;
                    contacto.Apellidos = contactoC.Apellidos;
                    contacto.Telefono = contactoC.Telefono;
                    contacto.Extension = contactoC.Extension;
                    contacto.Celular = contactoC.Celular;
                    contacto.CorreoElectronico = contactoC.CorreoElectronico;
                    contacto.Puesto = contactoC.Puesto;
                    contacto.ClienteId = clien.ClienteId;
                    contacto.Estatus = true;
                    connectModel.ContactoCliente.Add(contacto);
                }
                connectModel.SaveChanges();
                
                foreach (var informacionF in cliente.InformacionFiscal)
                {
                    Direccion direccionf = new Direccion();
                    UbicacionGeografica ubicacionGeograficaf = new UbicacionGeografica();
                    InformacionFiscal informacionfisc = new InformacionFiscal();


                    ubicacionGeograficaf.MunicipioId = informacionF.Direccion.UbicacionGeografica.MunicipioId;
                    ubicacionGeograficaf.EstadoId = informacionF.Direccion.UbicacionGeografica.EstadoId;
                    ubicacionGeograficaf.PaiId = informacionF.Direccion.UbicacionGeografica.PaiId;
                    connectModel.UbicacionGeografica.Add(ubicacionGeograficaf);
                    connectModel.SaveChanges();

                    direccionf.Calle = informacionF.Direccion.Calle;
                    direccionf.NumeroExterior = informacionF.Direccion.NumeroExterior;
                    direccionf.NumeroInterior = informacionF.Direccion.NumeroInterior;
                    direccionf.Colonia = informacionF.Direccion.Colonia;
                    direccionf.Ciudad = informacionF.Direccion.Ciudad;
                    direccionf.CodigoPostal = informacionF.Direccion.CodigoPostal;
                    direccionf.UbicacionGeograficaId = ubicacionGeograficaf.UbicacionGeograficaId;
                    connectModel.Direccion.Add(direccionf);
                    connectModel.SaveChanges();

                    
                    informacionfisc.RazonSocial = informacionF.RazonSocial;
                    informacionfisc.RFC = informacionF.RFC;
                    informacionfisc.DireccionId = direccionf.DireccionId;
                    informacionfisc.Estatus = true;
                    connectModel.InformacionFiscal.Add(informacionfisc);
                    connectModel.SaveChanges();
                    
                }
                var result = connectModel.Cliente.Select(c => new { c.ClienteId, c.Nombre, c.Telefono, c.Extension, c.Celular });
                return Json(result);
            }
            return Json(false);
        }

        public ActionResult loadStates (Guid paiId)
        {
            return Json(connectModel.Estado.Where(s => s.PaisId == paiId).Select(s=>new {EstadoId = s.EstadoId, Nombre = s.Nombre}).ToList(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadMunicipio(Guid estadoId)
        {
            return Json(connectModel.Municipio.Where(s => s.EstadoId == estadoId).Select(e => new { MunicipioId = e.MunicipioId, Nombre = e.Nombre }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadStatesI(Guid paiId)
        {
            return Json(connectModel.Estado.Where(s => s.PaisId == paiId).Select(s => new { EstadoId = s.EstadoId, Nombre = s.Nombre }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadMunicipioI(Guid estadoId)
        {
            return Json(connectModel.Municipio.Where(s => s.EstadoId == estadoId).Select(e => new { MunicipioId = e.MunicipioId, Nombre = e.Nombre }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}