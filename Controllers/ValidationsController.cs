using InForno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InForno.Controllers
{
    public class ValidationsController : Controller
    {
        private static ModelDbContext db = new ModelDbContext();

        public ActionResult IsUsernameValid(string username)
        {
            bool isValid = db.Users.All(x => x.username != username); ;

            return Json(isValid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsIngredienValid(string description)
        {
            bool isValid = db.Ingredients.All(x => x.description != description); ;
            return Json(isValid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsProductNameValid(string name)
        {
            bool isValid = db.Products.All(x => x.name != name); ;
            return Json(isValid, JsonRequestBehavior.AllowGet);
        }
    }
}