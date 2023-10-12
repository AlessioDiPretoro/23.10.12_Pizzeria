using InForno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace InForno.Controllers
{
    public class UsersController : Controller
    {
        public List<SelectListItem> TipiUser
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>
                {
                    new SelectListItem { Text = "--- ruolo ---", Value = "" },
                    new SelectListItem { Text = "SuperAdmin", Value = "SuperAdmin" },
                    new SelectListItem { Text = "Admin", Value = "Admin" },
                    new SelectListItem { Text = "User", Value = "User" }
                };
                return list;
            }
        }

        // GET: Users
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users u)
        {
            if (ModelState.IsValid)
            {
                u.role = "User";
                ApiInterfaceController.Register(u);
                FormsAuthentication.SetAuthCookie(u.username, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult RegisterBySuperAdmin()
        {
            ViewBag.ListUser = TipiUser;
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public ActionResult RegisterBySuperAdmin(Users u)
        {
            if (ModelState.IsValid)
            {
                ApiInterfaceController.Register(u);
            }
            ViewBag.ListUser = TipiUser;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUser l)
        {
            if (ModelState.IsValid)
            {
                bool loginResult = ApiInterfaceController.Login(l);
                if (loginResult)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Login = "Utente o password errati";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}