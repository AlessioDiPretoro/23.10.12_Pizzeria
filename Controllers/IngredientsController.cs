using InForno.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InForno.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class IngredientsController : Controller
    {
        // GET: Ingredients
        public ActionResult CreateIngredient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateIngredient(Ingredients i)
        {
            ApiInterfaceController.AddIngredient(i);
            ModelState.Clear();
            return View();
        }

        public ActionResult GetAllIngredients()
        {
            return PartialView("_GetAllIngredients", ApiInterfaceController.GetAllIngredient());
        }

        public ActionResult EditIngredient(int id)
        {
            Ingredients toEdit = ApiInterfaceController.db.Ingredients.Find(id);
            return View(toEdit);
        }

        //edit e delete hanno problema con relazione in altre tabelle
        [HttpPost]
        public ActionResult EditIngredient(Ingredients i)
        {
            Ingredients toEdit = ApiInterfaceController.db.Ingredients.Find(i.id);
            toEdit.description = i.description;
            ApiInterfaceController.db.Entry(toEdit).State = System.Data.Entity.EntityState.Modified;
            ApiInterfaceController.db.SaveChanges();
            return RedirectToAction("CreateIngredient");
        }

        public ActionResult DeleteIngredient(int id)
        {
            Ingredients toDelete = ApiInterfaceController.db.Ingredients.Find(id);
            ApiInterfaceController.db.Ingredients.Remove(toDelete);
            ApiInterfaceController.db.SaveChanges();
            return RedirectToAction("CreateIngredient");
        }
    }
}