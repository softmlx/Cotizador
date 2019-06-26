using Cotizador.UI.Models;
using Cotizador.UI.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Cotizador.UI.Controllers.Cotizador
{
    public class CotizacionsController : Controller
    {
        BDConnectModel conection = new BDConnectModel();
        // GET: Cotizacions
        public ActionResult Index()
        {
            return View("Cotizacions", conection.Cotizacion.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MonedaBase = conection.ConfiguracionSistema.FirstOrDefault().UrlBaseImagenes;
            ViewBag.Categorias = Json(conection.Categoria.Select(c => new { c.CategoriaId, c.Clave, c.Nombre, c.Estatus }).ToList());
            ViewBag.Clientes = Json(conection.Cliente.Select(c => new { c.ClienteId, c.Nombre }).ToList());
            ViewBag.Monedas = getTiposDeCambio();
            ViewBag.IVA = conection.ConfiguracionSistema.FirstOrDefault().Iva;
            return View("CotizacionsCreate", conection.Cotizacion.ToList());
        }

        [HttpGet]
        public ActionResult Edit(Models.Cotizacion cotizacion)
        {
            ViewBag.Categorias = Json(conection.Categoria.Select(c => new { c.CategoriaId, c.Clave, c.Nombre, c.Estatus }).ToList());
            ViewBag.Clientes = Json(conection.Cliente.Select(c => new { c.ClienteId, c.Nombre }).ToList());
            ViewBag.Monedas = getTiposDeCambio();
            ViewBag.IVA = conection.ConfiguracionSistema.FirstOrDefault().Iva;
            ViewBag.Cotizacion = Json(conection.Cotizacion
                .Where(c => c.CotizacionId == cotizacion.CotizacionId)
                .AsEnumerable()
                .Select(p => new Models.ViewModels.Cotizacion
                {
                    Fecha = p.FechaCotizacion.ToString("yyyy'-'MM'-'dd"),
                    Proyecto = p.Proyecto,
                    Subtotal = p.Subtotal,
                    IVA = p.ImporteIva,
                    Total = p.ImporteTotal,
                    MonedaId = p.MonedaId,
                    ClienteId = p.ClienteId,
                    ContactoClienteId = p.ContactoClienteId,
                    CotizacionId = p.CotizacionId
                }));
            var products = conection.DetalleCotizacion
                .Where(d => d.CotizacionId == cotizacion.CotizacionId)
                .Select(p => new {
                    p.Cantidad,
                    p.Costo,
                    p.Margen,
                    p.ProductoId,
                    p.Precio,
                    Marca = p.Producto.Marca.Nombre,
                    p.Producto.modelo,
                    p.Producto.DescripcionCorta,
                    p.DetalleCotizacionId,
                    p.Producto.CategoriaId,
                    Categoria = p.Producto.Categoria.Nombre
                }).ToList();
            var str = JsonConvert.SerializeObject(products);
            ViewBag.Productos = str;
            //ViewData["Productos"] = enc.GetString(enc.GetBytes(str));
            return View("CotizacionsEdit");
        }

        [HttpPost]
        public JsonResult getContactos(Models.Cliente cliente)
        {
            var consult = conection.ContactoCliente
                .Select(c => new { c.ClienteId, c.Nombre, c.Apellidos, c.Celular, c.ContactoClienteId, c.CorreoElectronico, c.Titulo, c.Telefono })
                .Where(q => q.ClienteId == cliente.ClienteId).ToList();
            return Json(consult);
        }

        [HttpPost]
        public JsonResult getProductos(Categoria categoria)
        {

            var result = new List<dynamic>();

            var consult = conection.Product
                .Where(q => q.CategoriaId == categoria.CategoriaId && q.Estatus == true)
                .Select(s => new { s.ProductoId, s.CategoriaId, s.MarcaId, s.Clave, s.Nombre, s.DescripcionCorta, s.modelo, Marca = s.Marca.Nombre, Categoria = s.Categoria.Nombre })
                .ToList();

            foreach(var p in consult)
            {
                var infoComercial = conection.InformacionComercialProductos
                    .Where(i => i.ProductoId == p.ProductoId)
                    .Select(inf => new { inf.Precio, inf.Margen, inf.Costo })
                    .ToList();

                dynamic info = null;

                if (infoComercial.Count > 0)
                {
                    info = infoComercial[0];
                }
                result.Add( new {
                    p.CategoriaId,
                    p.ProductoId,
                    p.MarcaId,
                    p.Marca,
                    p.Clave,
                    p.Nombre,
                    p.DescripcionCorta,
                    p.Categoria,
                    p.modelo,
                    Precio = info != null ? info.Precio : 0D,
                    Margen = info != null ? info.Margen : 0D,
                    Costo = info != null ? info.Costo : 0D,
                });
            }
            
            return Json(result);
        }

        [HttpPost]
        public JsonResult getMarca(Product producto)
        {
            var consult = conection.Marca.Where(q => q.MarcaId == producto.MarcaId).ToList();
            return Json(consult);
        }

        [HttpPost]
        public JsonResult saveCotizacion(Models.ViewModels.Cotizacion cotizacion)
        {
            int codeResult = 200;
            Guid MonedaBase = Guid.Parse(conection.ConfiguracionSistema.First().UrlBaseImagenes);
            double factor = 1;

            if (MonedaBase != cotizacion.MonedaId)
            {
                var tipoCambio = conection.TipoCambio
                    .Where(q => q.Moneda1Id == MonedaBase && q.Moneda2Id == cotizacion.MonedaId)
                    .Select(e => new { e.Factor })
                    .FirstOrDefault();
                factor = tipoCambio.Factor;
            }

            int totalProductos = 0;
            cotizacion.Partidas.ForEach(item => totalProductos += item.Cantidad);

            Models.Cotizacion newCotizacion = new Models.Cotizacion
            {
                FechaRegistro = DateTime.Now,
                FechaCotizacion = DateTime.Parse(cotizacion.Fecha),
                Proyecto = cotizacion.Proyecto,
                TipoCambio = factor,
                TotalProductos = totalProductos,
                Subtotal = cotizacion.Subtotal,
                ImporteTotal = cotizacion.Total,
                ImporteIva = cotizacion.IVA,
                MonedaId = cotizacion.MonedaId,
                ClienteId = cotizacion.ClienteId,
                Estatus = true,
                ContactoClienteId = cotizacion.ContactoClienteId
            };
            try
            {
                var added = conection.Cotizacion.Add(newCotizacion);
                conection.SaveChanges();
                cotizacion.Partidas.ForEach(p => {
                    var detalle = new DetalleCotizacion
                    {
                        Cantidad = p.Cantidad,
                        Precio = p.Precio != null ? p.Precio.Value : 0,
                        Costo = p.Costo != null ? p.Costo.Value : 0,
                        Margen = p.Margen != null ? p.Margen.Value : 0,
                        CotizacionId = added.CotizacionId,
                        ProductoId = p.ProductoId
                    };
                    conection.DetalleCotizacion.Add(detalle);
                });

                conection.SaveChanges();
            }
            catch (Exception)
            {
                codeResult = 500;
            }
            return Json(codeResult);
        }

        [HttpPost]
        public JsonResult updateCotizacion(Models.ViewModels.Cotizacion cotizacion)
        {
            int codeResult = 200;
            Guid MonedaBase = Guid.Parse(conection.ConfiguracionSistema.First().UrlBaseImagenes);
            double factor = 1;

            if (MonedaBase != cotizacion.MonedaId)
            {
                var tipoCambio = conection.TipoCambio
                    .Where(q => q.Moneda1Id == MonedaBase && q.Moneda2Id == cotizacion.MonedaId)
                    .Select(e => new { e.Factor })
                    .FirstOrDefault();
                factor = tipoCambio.Factor;
            }

            int totalProductos = 0;
            cotizacion.Partidas.ForEach(item => totalProductos += item.Cantidad);

            try
            {
                Models.Cotizacion updated = conection.Cotizacion.Find(cotizacion.CotizacionId.Value);
                updated.FechaCotizacion = DateTime.Parse(cotizacion.Fecha);
                updated.Proyecto = cotizacion.Proyecto;
                updated.TipoCambio = factor;
                updated.TotalProductos = totalProductos;
                updated.Subtotal = cotizacion.Subtotal;
                updated.ImporteTotal = cotizacion.Total;
                updated.ImporteIva = cotizacion.IVA;
                updated.MonedaId = cotizacion.MonedaId;
                updated.ClienteId = cotizacion.ClienteId;
                updated.Estatus = true;
                updated.ContactoClienteId = cotizacion.ContactoClienteId;
                conection.SaveChanges();
                List<Guid> partidasToDelete = new List<Guid>();
                List<Partida> partidas = conection.DetalleCotizacion
                    .Where(p => p.CotizacionId == cotizacion.CotizacionId.Value)
                    .Select(p => new Partida
                    {
                        DetalleCotizacionId = p.DetalleCotizacionId,
                        ProductoId = p.ProductoId
                    }).ToList();
                partidas.ForEach(p => {
                    if (!cotizacion.Partidas.Exists(item => item.DetalleCotizacionId == p.DetalleCotizacionId.Value))
                    {
                        if (!cotizacion.Partidas.Exists(item => item.ProductoId == p.ProductoId))
                        {
                            partidasToDelete.Add(p.DetalleCotizacionId.Value);
                        }
                    }
                });
                cotizacion.Partidas.ForEach(p => {
                    DetalleCotizacion detalle;
                    if (p.DetalleCotizacionId.HasValue)
                    {
                        detalle = conection.DetalleCotizacion.Find(p.DetalleCotizacionId);
                        detalle.Cantidad = p.Cantidad;
                        detalle.Precio = p.Precio != null ? p.Precio.Value : 0;
                        detalle.Costo = p.Costo != null ? p.Costo.Value : 0;
                        detalle.Margen = p.Margen != null ? p.Margen.Value : 0;
                        detalle.CotizacionId = updated.CotizacionId;
                        detalle.ProductoId = p.ProductoId;
                    }
                    else
                    {
                        if (partidas.Exists(item => item.ProductoId == p.ProductoId))
                        {
                            var partida = partidas.Find(item => item.ProductoId == p.ProductoId);
                            detalle = conection.DetalleCotizacion.Find(partida.DetalleCotizacionId);
                            detalle.Cantidad = p.Cantidad;
                            detalle.Precio = p.Precio != null ? p.Precio.Value : 0;
                            detalle.Costo = p.Costo != null ? p.Costo.Value : 0;
                            detalle.Margen = p.Margen != null ? p.Margen.Value : 0;
                            detalle.CotizacionId = updated.CotizacionId;
                            detalle.ProductoId = p.ProductoId;
                        }
                        else
                        {
                            detalle = new DetalleCotizacion
                            {
                                Cantidad = p.Cantidad,
                                Precio = p.Precio != null ? p.Precio.Value : 0,
                                Costo = p.Costo != null ? p.Costo.Value : 0,
                                Margen = p.Margen != null ? p.Margen.Value : 0,
                                CotizacionId = cotizacion.CotizacionId.Value,
                                ProductoId = p.ProductoId
                            };
                            conection.DetalleCotizacion.Add(detalle);
                        }
                    }
                    conection.SaveChanges();
                });
                partidasToDelete.ForEach(id => {
                    var partida = conection.DetalleCotizacion.Find(id);
                    conection.DetalleCotizacion.Remove(partida);
                    conection.SaveChanges();
                });
            }
            catch (Exception)
            {
                codeResult = 500;
            }
            return Json(codeResult);
        }

        [HttpPost]
        public JsonResult deleteCotizacion(Models.Cotizacion cotizacion)
        {
            int codeResult = 200;
            var toDelete = conection.Cotizacion.Find(cotizacion.CotizacionId);
            conection.Cotizacion.Remove(toDelete);
            try
            {
                conection.SaveChanges();
            }
            catch (Exception)
            {
                codeResult = 500;
                //throw;
            }
            return Json(codeResult);
        }

        private List<Moneda> getTiposDeCambio()
        {
            var monedaBase = conection.ConfiguracionSistema.FirstOrDefault().UrlBaseImagenes;
            Guid monedaId = Guid.Parse(monedaBase);
            var tipoCambios = conection.TipoCambio
                .Select(tc => new { tc.Moneda1Id, tc.Moneda2Id, tc.Factor })
                .Where(m => m.Moneda1Id == monedaId).ToList();
            List<Moneda> monedas = new List<Moneda>
            {
                conection.Moneda.Find(monedaId)
            };
            tipoCambios.ForEach(tc => {
                monedas.Add(conection.Moneda.Find(tc.Moneda2Id));
            });
            return monedas;
        }


    }
}