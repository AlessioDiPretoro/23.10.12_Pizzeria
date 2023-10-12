using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InForno.Models
{
    public class Cart
    {
        public int IdProduct { get; set; }
        public int qta { get; set; }

        public List<Cart> carts = new List<Cart>();
    }
}