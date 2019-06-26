using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cotizador.UI.Models;

namespace Cotizador.UI.Controllers
{
    public class UsuarioController : Controller
    {
        BDConnectModel connectModel = new BDConnectModel();

        // GET: Usuario
        public ActionResult Index()
        {
            ViewBag.List = connectModel.BitRole.ToList();
            return View("Usuarios", connectModel.BitUser.ToList());
        }

        /*public ActionResult Create()
        {
            ViewBag.List = connectModel.BitRole.ToList();
            return View("InsUsuarios");
        }*/
        [HttpPost]
        public ActionResult Insert([Bind(Include = "AdditionalInfo,Email,EmailConfirmed,PasswordHash,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,AccessFailedCount,UserName")] BitUser bitUser, ICollection<Guid> ids)
        {
            if(ModelState.IsValid)
            {
                connectModel.BitUser.Add(bitUser);
                connectModel.SaveChanges();
                foreach(var id in ids)
                {
                    BitUserRole bitUserRole = new BitUserRole();
                    bitUserRole.BitUserId = bitUser.BitUserId;
                    bitUserRole.BitRoleId = id;
                    connectModel.BitUserRole.Add(bitUserRole);
                }
                connectModel.SaveChanges();
                //return RedirectToAction("Index");
                var result = connectModel.BitUser.Select(u => new { u.BitUserId,u.UserName,u.Email,u.PhoneNumber,u.AdditionalInfo});
                return Json(result);
            }
            return Json(false);
            //return View("Usuarios");
        }

        [HttpPost]
        public ActionResult Edit(Guid id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = connectModel.BitUser.Select(u => new { u.BitUserId, u.UserName,u.PhoneNumber,u.Email, u.AdditionalInfo, u.BitUserRole}).Where(u=>u.BitUserId==id);
            if (result == null)
            {
                return Json("'result' : 'error' + HttpNotFound()");
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult Update([Bind(Include = "BitUserId,AdditionalInfo,Email,EmailConfirmed,PasswordHash,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,AccessFailedCount,UserName")] BitUser bitUser, ICollection<Guid> ids)
        {
            if(ModelState.IsValid)
            {
                var consult = connectModel.BitUserRole.Where(u => u.BitUserId == bitUser.BitUserId).ToList();
                if (consult.Count() != 0)
                {
                    connectModel.BitUserRole.RemoveRange(consult);
                    connectModel.SaveChanges();
                }
                connectModel.Entry(bitUser).State = EntityState.Modified;
                connectModel.SaveChanges();
                foreach (var id in ids)
                {
                    BitUserRole bitUserRole = new BitUserRole();
                    bitUserRole.BitUserId = bitUser.BitUserId;
                    bitUserRole.BitRoleId = id;
                    connectModel.BitUserRole.Add(bitUserRole); ;
                }
                connectModel.SaveChanges();
                //return RedirectToAction("Index");
                var result = connectModel.BitUser.Select(u => new { u.BitUserId, u.UserName, u.Email, u.PhoneNumber, u.AdditionalInfo });
                return Json(result);
            }
            return Json(false);
            //return View("Usuarios");
        }
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            BitUser user = connectModel.BitUser.Find(id);
            connectModel.BitUser.Remove(user);
            connectModel.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}