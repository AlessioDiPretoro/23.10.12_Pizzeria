using InForno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InForno.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        public ActionResult ShowCart()
        {
            if (Session["cart"] == null)
            {
                ViewBag.Nocart = "Nessun prodotto nel carrello";
            }
            else
            {
                Cart cart = (Cart)Session["cart"];
                List<DetailsOrders> detailsOrdersList = new List<DetailsOrders>();
                int index = 0;
                foreach (Cart cartItem in cart.carts)
                {
                    index++;
                    DetailsOrders detailsOrders = new DetailsOrders
                    {
                        id = index,
                        idProduct = cartItem.IdProduct,
                        quatity = cartItem.qta,
                        Products = ApiInterfaceController.GetProductById(cartItem.IdProduct),
                    };
                    detailsOrdersList.Add(detailsOrders);
                }
                return View(detailsOrdersList);
            }
            return View();
        }

        public ActionResult RemoveItem(int id)
        {
            Cart cart = (Cart)Session["cart"];
            cart.carts.RemoveAt(id - 1);
            Session["cart"] = cart;
            return RedirectToAction("ShowCart");
        }
    }
}