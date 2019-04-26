using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cemapa.Models
{
    class RetornoJson
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Mensagem { get; set; }
        public DateTime Data { get; set; }
        public string Complemento { get; set; }

        public RetornoJson(string status, string mensagem, string complemento = "", int id = 0)
        {
            Id = id;
            Status = status;
            Mensagem = mensagem;
            Data = DateTime.Now;
            Complemento = complemento;
        }
    }
}
