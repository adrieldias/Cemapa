using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cemapa.Models
{
    public class ProdutoSkyhub
    {
        public string sku { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public int qty { get; set; }
        public int price { get; set; }
        public int promotional_price { get; set; }
        public int cost { get; set; }
        public int weight { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public int length { get; set; }
        public string brand { get; set; }
        public string ean { get; set; }
        public string nbn { get; set; }

        public List<CategoriaProdutoSkyhub> categories;
        public List<string> images;
        public List<EspecificacoesProdutoSkyhub> specifications;
        public List<ProdutoSkyhub> variations;
    }
}