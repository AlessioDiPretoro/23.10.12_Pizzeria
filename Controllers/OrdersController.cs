using InForno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InForno.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private static ModelDbContext db = new ModelDbContext();

        public ActionResult CreateOrder(string address, string note)
        {
            if (address == "")
            {
                TempData["MissingAddress"] = "Inserire indirizzo di consegna";
                return RedirectToAction("ShowCart", "Cart");
            }
            else
            {
                int id = ApiInterfaceController.GetUserByUsername(User.Identity.Name).id;
                Cart cart = (Cart)Session["cart"];
                Orders order = new Orders
                {
                    idBuyer = id,
                    processed = false,
                    data = DateTime.Now,
                    notes = note,
                    address = address,
                };

                foreach (Cart cartItem in cart.carts)
                {
                    order.DetailsOrders.Add(new DetailsOrders
                    {
                        idProduct = cartItem.IdProduct,
                        quatity = cartItem.qta,
                    });
                }

                db.Orders.Add(order);
                db.SaveChanges();
                Session.Remove("cart");
                return RedirectToAction("ShowCart", "Cart"); //poi creare pagina di riepilogo ordini per utente
            }
        }

        public ActionResult AllOrdersByClient()
        {
            int id = db.Users.Where(x => x.username == User.Identity.Name).FirstOrDefault().id;
            List<Orders> orders = db.Orders.Where(x => x.idBuyer == id).OrderByDescending(x => x.data).ToList();
            return View(orders);
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult AllOrders()
        {
            List<Orders> orders = db.Orders.OrderByDescending(x => x.data).ToList();
            return View(orders);
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult OrderPrepared(int id)
        {
            Orders order = db.Orders.Where(x => x.id == id).FirstOrDefault();
            order.processed = true;
            db.Entry(order).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AllOrders");
        }

        [HttpPost]
        public JsonResult ProcessedToday()
        {
            try
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);
                List<Orders> tp = db.Orders.Where(x => x.processed == true && x.data >= today && x.data < tomorrow).ToList();
                List<OrdersToJson> ordersToJson = new List<OrdersToJson>();

                foreach (Orders ord in tp)
                {
                    ordersToJson.Add(new OrdersToJson
                    {
                        id = ord.id,
                        buyerUsername = db.Users.Where(x => x.id == ord.idBuyer).FirstOrDefault().username,
                    });
                };
                return Json(ordersToJson);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CollectedByDay(DateTime inputVal)
        {
            try
            {
                DateTime tomorrow = inputVal.AddDays(1);
                decimal totMoney = 0;
                List<Orders> tp = db.Orders.Where(x => x.processed == true && x.data >= inputVal && x.data < tomorrow).ToList();
                List<OrdersToJson> ordersToJson = new List<OrdersToJson>();

                foreach (Orders ord in tp)
                {
                    ordersToJson.Add(new OrdersToJson
                    {
                        id = ord.id,
                        buyerUsername = db.Users.Where(x => x.id == ord.idBuyer).FirstOrDefault().username,
                    });

                    foreach (DetailsOrders dt in ord.DetailsOrders)
                    {
                        decimal totDt = dt.quatity * dt.Products.price;
                        totMoney += totDt;
                    }
                };
                return Json(totMoney);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}