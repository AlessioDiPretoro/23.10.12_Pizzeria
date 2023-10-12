using InForno.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InForno.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private static ModelDbContext db = new ModelDbContext();

        private List<SelectListItem> ingredients
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                List<Ingredients> listIng = db.Ingredients.ToList();
                foreach (Ingredients ing in listIng)
                {
                    list.Add(new SelectListItem { Text = ing.description, Value = ing.id.ToString() });
                }
                return list;
            }
        }

        private List<string> acceptedFile
        {
            get
            {
                List<string> strings = new List<string>
                {
                    ".jpeg",
                    ".jpg",
                    ".png"
                };

                return strings;
            }
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult CreateProduct()
        {
            ViewBag.ingredients = ingredients;
            return View();
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost]
        public ActionResult CreateProduct(Products p, FormCollection cheks, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                string nomeFile = photo.FileName;
                p.photo = nomeFile;
                string ext = Path.GetExtension(photo.FileName);
                if (acceptedFile.Contains(ext))
                {
                    string pathToSave = Path.Combine(Server.MapPath("~/Content/img"), nomeFile);
                    photo.SaveAs(pathToSave);

                    List<string> selezione = cheks.GetValues("Selezione")?.ToList();
                    foreach (string s in selezione)
                    {
                        p.Products_Ingredients.Add(new Products_Ingredients { idIngredient = Convert.ToInt16(s) });
                    }
                    db.Products.Add(p);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.ingredients = ingredients;
                    ViewBag.Success = "Prodotto inserito con successo";
                    return View();
                }
                else
                {
                    ViewBag.ingredients = ingredients;
                    ViewBag.Error = "Formato file non supportato";
                    return View();
                }
            }
            else
            {
                ViewBag.ingredients = ingredients;
                return View();
            }
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult TableEditProduct()
        {
            List<Products> p = ApiInterfaceController.GetAllProducts();
            return View(p);
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult EditProduct(int id)
        {
            Products p = ApiInterfaceController.GetProductById(id);
            return View(p);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult EditProduct(Products p, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                Products toEdit = ApiInterfaceController.db.Products.Find(p.id);
                toEdit.name = p.name;
                toEdit.price = p.price;
                if (photo != null)
                {
                    string nomeFile = photo.FileName;
                    string ext = Path.GetExtension(photo.FileName);
                    if (acceptedFile.Contains(ext))
                    {
                        toEdit.photo = nomeFile;
                        string pathToSave = Path.Combine(Server.MapPath("~/Content/img"), nomeFile);
                        photo.SaveAs(pathToSave);
                    }
                    else
                    {
                        ViewBag.Error = "Formato file non supportato";
                        return View(p);
                    }
                }
                ApiInterfaceController.db.Entry(toEdit).State = System.Data.Entity.EntityState.Modified;
                ApiInterfaceController.db.SaveChanges();
                return RedirectToAction("TableEditProduct");
            }
            else
            {
                return View(p);
            }
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult DeleteProduct(int id)
        {
            Products pToRemove = db.Products.Find(id);
            db.Products.Remove(pToRemove);
            db.SaveChanges();
            return RedirectToAction("TableEditProduct");
        }

        public ActionResult ShowMenu()
        {
            List<Products> p = ApiInterfaceController.GetAllProducts();
            return View(p);
        }

        public ActionResult AddToCart(int id, int quantity)
        {
            if (Session["cart"] == null)
            {
                Cart cart = new Cart();
                cart.IdProduct = id;
                cart.qta = quantity;
                cart.carts.Add(cart);
                Session["cart"] = cart;
            }
            else
            {
                Cart cart = (Cart)Session["cart"];
                cart.carts.Add(new Cart
                {
                    IdProduct = id,
                    qta = quantity
                });
                Session["cart"] = cart;
            }
            return RedirectToAction("ShowMenu");
        }
    }
}