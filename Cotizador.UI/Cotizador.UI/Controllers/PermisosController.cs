using Cotizador.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cotizador.UI.Controllers
{
    public class PermisosController : Controller
    {
        // GET: Permisos
        BDConnectModel i = new BDConnectModel();
        public ActionResult Index()
        {
            ViewBag.ListaPermisos = i.BitPermission.ToList();
            return View("Permisos", i.BitPermission.ToList());
        }


        //GET Roles/Create
        [HttpPost]
        public ActionResult Insert([Bind(Include = "FriendlyName,GroupedBy")] BitPermission bitPermission)
        {
            if (ModelState.IsValid)
            {
                i.BitPermission.Add(bitPermission);
                i.SaveChanges();

                var result = i.BitPermission.Select(p => new { p.BitPermissionId, p.FriendlyName, p.GroupedBy });
                return Json(result);

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
            var result = i.BitPermission.Select(p => new { p.BitPermissionId, p.FriendlyName, p.GroupedBy }).Where(p => p.BitPermissionId == id);

            if (result == null)
            {
                return Json("'result' : 'error' + HttpNotFound()");
            }
            return Json(result);
        }
        [HttpPost]
        public ActionResult Update([Bind(Include = "BitPermissionId,FriendlyName, GroupedBy")] BitPermission bitPermission)
        {
            if (ModelState.IsValid)
            {
                i.Entry(bitPermission).State = EntityState.Modified;
                i.SaveChanges();
                var result = i.BitPermission.Select(p => new {p.BitPermissionId, p.FriendlyName, p.GroupedBy });
                return Json(result);
            }
            return Json(false);
        }
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            BitPermission bitPermission = i.BitPermission.Find(id);
            if (bitPermission == null)
            {
                return HttpNotFound();
            }
            var consult = i.BitRolePermission.Where(p => p.BitPermissionId == id).ToList();
            if (consult.Count() == 0)
            {

                i.BitPermission.Remove(bitPermission);
                i.SaveChanges();
                return Json(id.ToString());
            }
            
            return Json(false);
        }
    }
}
