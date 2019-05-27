using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cemapa.Models
{
    public class ProdutosSkyhub
    {
        public long sku { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public int qty { get; set; }
        public double price { get; set; }
        public double promotional_price { get; set; }
        public double cost { get; set; }
        public double weight { get; set; }
        public double height { get; set; }
        public double width { get; set; }
        public double length { get; set; }
        public string brand { get; set; }
        public string ean { get; set; }
        public string nbm { get; set; }

        public List<CategoriaProdutoSkyhub> categories = new List<CategoriaProdutoSkyhub>();
        public List<string> images = new List<string>();
        public List<EspecificacoesProdutoSkyhub> specifications = new List<EspecificacoesProdutoSkyhub>();
        public List<ProdutosSkyhub> variations;
    }

    public class EspecificacoesProdutoSkyhub
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class CategoriaProdutoSkyhub
    {
        public long code { get; set; }
        public string name { get; set; }
    }
}