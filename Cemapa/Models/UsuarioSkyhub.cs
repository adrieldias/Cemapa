using System.Collections.Generic;

namespace Cemapa.Models.Skyhub
{

    public class Auth
    {
        public string user_email { get; set; }
        public string api_key { get; set; }
    }

    public class Token
    {
        public string token { get; set; }
    }

    public class Action
    {
        public List<long> skus = new List<long>();
        public string sale_system { get; set; } 
        public string type { get; set; }
        public List<object> specifications = new List<object>();
        public List<object> previous_specifications = new List<object>();
    }
}