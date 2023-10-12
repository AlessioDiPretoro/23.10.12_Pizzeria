using InForno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace InForno.Controllers
{
    [Authorize]
    public class ApiInterfaceController : ApiController
    {
        public static ModelDbContext db = new ModelDbContext();

        [AllowAnonymous]
        [Route("api/register")]
        [HttpPost]
        public static void Register([FromBody] Users u)
        {
            db.Users.Add(u);
            db.SaveChanges();
        }

        [AllowAnonymous]
        [Route("api/login")]
        [HttpPost]
        public static bool Login([FromBody] LoginUser l)
        {
            var user = db.Users.FirstOrDefault(x => x.username == l.username && x.password == l.password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(l.username, true);
                return true;
            }
            else
            {
                return false; // Restituisci una risposta HTTP 404 se l'utente non è stato trovato
            }
        }

        [Authorize(Roles = ("Admin, SuperAdmin"))]
        [Route("api/ingredients")]
        [HttpPost]
        public static void AddIngredient([FromBody] Ingredients i)
        {
            db.Ingredients.Add(i);
            db.SaveChanges();
        }

        [Authorize(Roles = ("Admin, SuperAdmin"))]
        [Route("api/getAllingredients")]
        [HttpGet]
        public static List<Ingredients> GetAllIngredient()
        {
            return db.Ingredients.ToList();
        }

        [Authorize(Roles = ("Admin, SuperAdmin"))]
        [Route("api/product")]
        [HttpPost]
        public static void AddProduct([FromBody] Products p)
        {
            db.Products.Add(p);
            db.SaveChanges();
        }

        [Authorize]
        [Route("api/getAllProducts")]
        [HttpGet]
        public static List<Products> GetAllProducts()
        {
            return db.Products.ToList();
        }

        [Authorize]
        [Route("api/getProducts/{id}")]
        [HttpGet]
        public static Products GetProductById(int id)
        {
            return db.Products.Where(x => x.id == id).FirstOrDefault();
        }

        [Authorize]
        [Route("api/getUser/{id}")]
        [HttpGet]
        public static Users GetUserByUsername(string username)
        {
            return db.Users.Where(x => x.username == username).FirstOrDefault();
        }


        [AllowAnonymous]
        [Route("api/getOrders")]
        [HttpGet]
        public static List<Orders> GetAllOrders()
        {
            return db.Orders.ToList();
        }

        [AllowAnonymous]
        [Route("api/getOrder/{id}")]
        [HttpGet]
        public static Orders GetOrderById(int id)
        {
            return db.Orders.Where(x => x.id == id).FirstOrDefault();
        }

        [AllowAnonymous]
        [Route("api/getOrders/{id}")]
        [HttpGet]
        public static List<Orders> GetOrdersByUserId(int id)
        {
            return db.Orders.Where(x => x.idBuyer == id).ToList();
        }
    }
}