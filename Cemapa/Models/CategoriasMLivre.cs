using System.Collections.Generic;

namespace Cemapa.Models
{
    public class PathFromRoot
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ChildrenCategory
    {
        public string id { get; set; }
        public string name { get; set; }
        public string total_items_in_this_category { get; set; }
    }

    public class Settings
    {
        public bool adult_content { get; set; }
        public bool buying_allowed { get; set; }
        public List<string> buying_modes { get; set; }
        public string catalog_domain { get; set; }
        public string coverage_areas { get; set; }
        public List<string> currencies { get; set; }
        public bool fragile { get; set; }
        public string immediate_payment { get; set; }
        public List<string> item_conditions { get; set; }
        public bool items_reviews_allowed { get; set; }
        public bool listing_allowed { get; set; }
        public string max_description_length { get; set; }
        public string max_pictures_per_item { get; set; }
        public string max_pictures_per_item_var { get; set; }
        public string max_sub_title_length { get; set; }
        public string max_title_length { get; set; }
        public object maximum_price { get; set; }
        public string minimum_price { get; set; }
        public object mirror_category { get; set; }
        public object mirror_master_category { get; set; }
        public List<object> mirror_slave_categories { get; set; }
        public string price { get; set; }
        public string reservation_allowed { get; set; }
        public List<object> restrictions { get; set; }
        public bool rounded_address { get; set; }
        public string seller_contact { get; set; }
        public List<string> shipping_modes { get; set; }
        public List<string> shipping_options { get; set; }
        public string shipping_profile { get; set; }
        public bool show_contact_information { get; set; }
        public string simple_shipping { get; set; }
        public string stock { get; set; }
        public object sub_vertical { get; set; }
        public bool subscribable { get; set; }
        public List<object> tags { get; set; }
        public object vertical { get; set; }
        public string vip_subdomain { get; set; }
        public List<string> buyer_protection_programs { get; set; }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public object permalink { get; set; }
        public string total_items_in_this_category { get; set; }
        public List<PathFromRoot> path_from_root { get; set; }
        public List<ChildrenCategory> children_categories { get; set; }
        public string attribute_types { get; set; }
        public Settings settings { get; set; }
        public object meta_categ_id { get; set; }
        public bool attributable { get; set; }
    }

    public class Categoria
    {
        public string id { get; set; }
        public string descricao { get; set; }
        public List<Categoria> filhos = new List<Categoria>();
    }
}