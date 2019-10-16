using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cemapa.Models
{
    public class ProdutoSkyhub
    {
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public long sku { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public string name { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public string description { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public string status { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public int qty { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public double price { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public double weight { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public double height { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public double width { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public double length { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public string brand { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public string ean { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public string nbm { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura

        public List<Category> categories = new List<Category>();
        public List<string> images = new List<string>();
        public List<string> variation_attributes = new List<string>();
        public List<Specification> specifications = new List<Specification>();
        public List<Variation> variations = new List<Variation>();
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public List<Association> associations { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura

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
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public string platform { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public string status { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
    }

    public class Specification
    {
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public string key { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public string value { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
    }

    public class Category
    {
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public long code { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public string name { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
    }

    public class Variation
    {
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public string ean { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public int qty { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public long sku { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public double price { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
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
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public List<ProdutoSkyhub> products { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public int total { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
#pragma warning disable IDE1006 // Estilos de Nomenclatura
        public string next { get; set; }
#pragma warning restore IDE1006 // Estilos de Nomenclatura
    }
}