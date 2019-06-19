using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cemapa.Models
{
    public class PLPs
    {
        public List<Plp> plp { get; set; }
    }

    public class ErrorPLP
    {
        public string result { get; set; }
        public string message { get; set; }
        public object sqlerrm { get; set; }
    }

    public class Plp
    {
        public string id { get; set; }
        public string expiration_date { get; set; }
        public bool printed { get; set; }
        public string type { get; set; }
        public List<OrderPLP> orders { get; set; }
    }

    public class OrderPLP
    {
        public string code { get; set; }
        public string customer { get; set; }
        public double value { get; set; }
    }
}