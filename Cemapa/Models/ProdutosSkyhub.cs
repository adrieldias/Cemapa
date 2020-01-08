using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cemapa.Models.Skyhub
{
    public class ProdutoSkyhub
    {

        public long sku { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public int qty { get; set; }
        public double price { get; set; }
        public double weight { get; set; }
        public double height { get; set; }
        public double width { get; set; }
        public double length { get; set; }
        public string brand { get; set; }
        public string ean { get; set; }
        public string nbm { get; set; }
        public List<Category> categories = new List<Category>();
        public List<string> images = new List<string>();
        public List<string> variation_attributes = new List<string>();
        public List<Specification> specifications = new List<Specification>();
        public List<Variation> variations = new List<Variation>();
        public List<Association> associations { get; set; }
        
        public void AddSpecificationsCustom(string nKey, string nValue)
        {
            if (!String.IsNullOrEmpty(nValue))
            {
                specifications.Add(
                    new Specification
                    {
                        key = nKey,
                        value = nValue
                    }
                );
            }
        }

        public void AddImagesCustom(string nLink)
        {
            if (!String.IsNullOrEmpty(nLink))
            {
                images.Add(nLink);
            }
        }
    }

    public class Association
    {
        public string platform { get; set; }
        public string status { get; set; }

    }

    public class Specification
    {
        public string key { get; set; }
        public string value { get; set; }

    }

    public class Category
    {
        public long code { get; set; }
        public string name { get; set; }

    }

    public class Variation
    {
        public string ean { get; set; }
        public int qty { get; set; }
        public long sku { get; set; }
        public double price { get; set; }
        public List<string> images = new List<string>();
        public List<Specification> specifications = new List<Specification>();

        public void AddSpecificationsCustom(string nKey, string nValue)
        {
            if (!String.IsNullOrEmpty(nValue))
            {
                specifications.Add(
                    new Specification
                    {
                        key = nKey,
                        value = nValue
                    }
                );
            }
        }

        public void AddImagesCustom(string nLink)
        {
            if (!String.IsNullOrEmpty(nLink))
            {
                images.Add(nLink);
            }
        }
    }

    public class Products
    {
        public List<ProdutoSkyhub> products { get; set; }
        public int total { get; set; }
        public string next { get; set; }

    }
}