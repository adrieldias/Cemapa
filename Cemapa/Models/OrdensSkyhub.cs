using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cemapa.Models
{
    public class OrdensSkyhub
    {
        public int total { get; set; }
        public List<Order> orders { get; set; }
    }

    public class Error
    {
        public string error { get; set; }
    }

    public class Order
    {
        public DateTime updated_at { get; set; }
        public double total_ordered { get; set; }
        public List<object> tags { get; set; }
        public string sync_status { get; set; }
        public Status status { get; set; }
        public string shipping_method { get; set; }
        public double shipping_cost { get; set; }
        public string shipping_carrier { get; set; }
        public ShippingAddress shipping_address { get; set; }
        public string shipped_date { get; set; }
        public List<Shipment> shipments { get; set; }
        public DateTime placed_at { get; set; }
        public List<Payment> payments { get; set; }
        public List<Item> items { get; set; }
        public List<object> invoices { get; set; }
        public double interest { get; set; }
        public ImportInfo import_info { get; set; }
        public object estimated_delivery_shift { get; set; }
        public DateTime estimated_delivery { get; set; }
        public double discount { get; set; }
        public string delivery_contract_type { get; set; }
        public object delivered_date { get; set; }
        public Customer customer { get; set; }
        public string code { get; set; }
        public string channel { get; set; }
        public object calculation_type { get; set; }
        public BillingAddress billing_address { get; set; }
        public object approved_date { get; set; }
    }

    public class Track
    {
        public string code { get; set; }
        public string carrier { get; set; }
        public string method { get; set; }
        public string url { get; set; }
    }

    public class Shipment
    {
        public string code { get; set; }
        public Track track { get; set; }
    }

    public class Sefaz
    {
        public string type_integration { get; set; }
        public string payment_indicator { get; set; }
        public string name_payment { get; set; }
        public string name_card_issuer { get; set; }
        public string id_payment { get; set; }
        public string id_card_issuer { get; set; }
    }

    public class Payment
    {
        public double value { get; set; }
        public object transaction_date { get; set; }
        public object status { get; set; }
        public Sefaz sefaz { get; set; }
        public int parcels { get; set; }
        public string method { get; set; }
        public string description { get; set; }
        public string card_issuer { get; set; }
        public string autorization_id { get; set; }
    }

    public class Status
    {
        public string type { get; set; }
        public string label { get; set; }
        public string code { get; set; }
    }

    public class ShippingAddress
    {
        public string street { get; set; }
        public string secondary_phone { get; set; }
        public string region { get; set; }
        public string reference { get; set; }
        public string postcode { get; set; }
        public string phone { get; set; }
        public string number { get; set; }
        public string neighborhood { get; set; }
        public string full_name { get; set; }
        public string detail { get; set; }
        public string country { get; set; }
        public string complement { get; set; }
        public string city { get; set; }
    }

    public class Item
    {
        public double special_price { get; set; }
        public object shipping_cost { get; set; }
        public int qty { get; set; }
        public string product_id { get; set; }
        public double original_price { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public object detail { get; set; }
    }

    public class ImportInfo
    {
        public string ss_name { get; set; }
        public object remote_id { get; set; }
        public string remote_code { get; set; }
    }

    public class Customer
    {
        public string vat_number { get; set; }
        public List<string> phones { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string state_registration { get; set; }
        public string date_of_birth { get; set; }
    }

    public class BillingAddress
    {
        public string street { get; set; }
        public string secondary_phone { get; set; }
        public string region { get; set; }
        public string reference { get; set; }
        public string postcode { get; set; }
        public string phone { get; set; }
        public string number { get; set; }
        public string neighborhood { get; set; }
        public string full_name { get; set; }
        public string detail { get; set; }
        public string country { get; set; }
        public string complement { get; set; }
        public string city { get; set; }
    }

    public class Invoice
    {
        public string key { get; set; }
    }
}