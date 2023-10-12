using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InForno.Models
{
    public class OrdersToJson
    {
        public int id { get; set; }
        public string buyerUsername { get; set; }
        public double tot { get; set; }
    }
}