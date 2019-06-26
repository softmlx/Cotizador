using Cotizador.UI.Models;
using Cotizador.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cotizador.UI.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        BDConnectModel i = new BDConnectModel();


        public ActionResult Index()
        {
            //return View("Rol", i.BitRole.ToList());
            ViewBag.ListaPermisos = i.BitPermission.ToList();
            /*   var rols = i.BitRole.ToList();
               foreach(BitRole role in rols)
               {

                   var permisosDeUsuario = i.BitRolePermission.Where(d => d.BitRoleId.CompareTo(role.BitRoleId)==0).ToList();


               }*/
            return View("Roles", i.BitRole.ToList());
        }

        //GET Roles/Create
        [HttpPost]
        public ActionResult Insert([Bind(Include = "Name,DisplayName")] BitRole bitRole, ICollection<Guid> ids)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    i.BitRole.Add(bitRole);
                    foreach (var id in ids)
                    {
                        BitRolePermission bp = new BitRolePermission();
                        bp.BitPermissionId = id;
                        bp.BitRoleId = bitRole.BitRoleId;
                        i.BitRolePermission.Add(bp);
                        i.SaveChanges();
                    }
                    var result = i.BitRole.Select(p => new { p.BitRoleId, p.Name, p.DisplayName });
                    return Json(result);
                }
                return Json(false);
            }
            catch (Exception e)
            {
                return Json(false);
            }
            
        }
        
        /*    return Json(false);
        }*/
        [HttpPost]
        public ActionResult Update([Bind(Include = "BitRoleId, Name, DisplayName")] BitRole bitRole, ICollection<Guid> ids)
        {
           
            if (ModelState.IsValid)
            {              
                var consult = i.BitRolePermission.Where(p => p.BitRoleId == bitRole.BitRoleId).ToList();

                if (consult.Count() != 0)
                {
                    i.BitRolePermission.RemoveRange(consult);
                    i.SaveChanges();
                }

                i.Entry(bitRole).State = EntityState.Modified;
                i.SaveChanges();

                try
                {

                    foreach (var id in ids)
                    {
                        BitRolePermission brp = new BitRolePermission();
                        brp.BitRoleId = bitRole.BitRoleId;
                        brp.BitPermissionId = id;
                        i.BitRolePermission.Add(brp);
                    }
                    i.SaveChanges();
                    var result = i.BitRole.Select(p => new { p.BitRoleId, p.Name, p.DisplayName });
                    return Json(result);
                }catch(Exception e)
                {
                    return Json(false);
                }
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

            var result = i.BitRole.Select(p => new { p.BitRoleId, p.Name, p.DisplayName }).Where(p => p.BitRoleId == id);
            if (result == null)
            {
                return Json("'result' : 'error' + HttpNotFound()");
            }
            return Json(result);
        }
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            BitRole bitRole = i.BitRole.Find(id);
           
            if (bitRole == null)
            {
                return HttpNotFound();
            }
            var consult = i.BitRolePermission.Where(p => p.BitRoleId == id).ToList();
            if (consult.Count() != 0)
            {
              
                i.BitRole.Remove(bitRole);
                i.SaveChanges();
                return Json(id.ToString());
            }
      
            return Json(false);

        }

    }
}
