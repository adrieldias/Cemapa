using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cemapa.Models
{
    public class RetornoJson
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Mensagem { get; set; }
        public DateTime Data { get; set; }
        public string Complemento { get; set; }
        public List<string> Corpo { get; set; }
    }
}
