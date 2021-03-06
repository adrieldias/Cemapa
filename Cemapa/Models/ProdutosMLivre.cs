﻿using Cemapa.Models.MercadoLivre.Usuario;
using System;
using System.Collections.Generic;

namespace Cemapa.Models.MercadoLivre.Products
{
    public class Search
    {
        public string query { get; set; }
        public List<string> results { get; set; }
        public Sort sort { get; set; }
        public List<AvailableSort> available_sorts { get; set; }
        public List<AvailableFilter> available_filters { get; set; }
        public List<object> filters { get; set; }
        public Paging paging { get; set; }
        public string display { get; set; }
    }
    
    public class Sort
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class AvailableSort
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class AvailableFilter
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Value> values { get; set; }
    }

    public class Picture
    {
        public string id { get; set; }
        public string url { get; set; }
        public string source { get; set; }
        public string secure_url { get; set; }
        public string size { get; set; }
        public string max_size { get; set; }
        public string quality { get; set; }

        public Picture(string link)
        {
            url = link;
            source = link;
        }
    }

    public class Description
    {
        public string id { get; set; }
        public string plain_text { get; set; }

        public Description(string texto)
        {
            plain_text = texto;
        }
    }

    public class Shipping
    {
        public string mode { get; set; }
        public List<object> methods { get; set; }
        public List<object> tags { get; set; }
        public object dimensions { get; set; }
        public bool local_pick_up { get; set; }
        public bool free_shipping { get; set; }
        public string logistic_type { get; set; }
        public bool store_pick_up { get; set; }
    }

    public class City
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class State
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Country
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class City2
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class State2
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class SearchLocation
    {
        public City2 city { get; set; }
        public State2 state { get; set; }
    }

    public class SellerAddress
    {
        public City city { get; set; }
        public State state { get; set; }
        public Country country { get; set; }
        public SearchLocation search_location { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int id { get; set; }
    }

    public class Location
    {
    }

    public class Geolocation
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Attribute
    {
        public string id { get; set; }
        public string name { get; set; }
        public string value_id { get; set; }
        public string value_name { get; set; }
        public object value_struct { get; set; }
        public List<Value> values { get; set; }
        public string attribute_group_id { get; set; }
        public string attribute_group_name { get; set; }

        public Attribute(string codigo, string valor)
        {
            id = codigo;
            value_name = valor;
        }
    }

    public class SaleTerms
    {
        public string id { get; set; }
        public string name { get; set; }
        public string value_name { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string site_id { get; set; }
        public string title { get; set; }
        public object subtitle { get; set; }
        public string category_id { get; set; }
        public object official_store_id { get; set; }
        public double price { get; set; }
        public object original_price { get; set; }
        public string currency_id { get; set; }
        public int available_quantity { get; set; }
        public List<SaleTerms> sale_terms = new List<SaleTerms>();
        public string buying_mode { get; set; }
        public string listing_type_id { get; set; }
        public string condition { get; set; }
        public string permalink { get; set; }
        public string thumbnail { get; set; }
        public string secure_thumbnail { get; set; }
        public List<Picture> pictures = new List<Picture>();
        public object video_id { get; set; }
        public Description description { get; set; }
        public List<object> non_mercado_pago_payment_methods;
        public Shipping shipping { get; set; }
        public SellerAddress seller_address { get; set; }
        public object seller_contact { get; set; }
        public Location location { get; set; }
        public Geolocation geolocation { get; set; }
        public List<object> coverage_areas = new List<object>();
        public List<Attribute> attributes = new List<Attribute>();
        public string listing_source { get; set; }
        public List<object> variations = new List<object>();
        public string status { get; set; }
        public List<string> tags = new List<string>();
        public string warranty { get; set; }
        public object catalog_product_id { get; set; }
        public string domain_id { get; set; }
        public string parent_item_id { get; set; }
        public object differential_pricing { get; set; }
        public object health { get; set; }
        public string seller_custom_field { get; set; }

        public Item()
        {
            currency_id = "BRL";
            tags.Add("immediate_payment");
        }

        public void AddAttributesCustom(Attribute attribute)
        {
            if (!String.IsNullOrEmpty(attribute.value_name) &&
                !String.IsNullOrEmpty(attribute.id))
            {
                attributes.Add(attribute);
            }
        }

        public void AddImagesCustom(Picture picture)
        {
            if (!String.IsNullOrEmpty(picture.url))
            {
                pictures.Add(picture);
            }
        }
    }
}