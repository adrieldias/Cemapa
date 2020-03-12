using Cemapa.Models.MercadoLivre.Usuario;
using System;
using System.Collections.Generic;

namespace Cemapa.Models.MercadoLivre.Orders
{
    public class Phone
    {
        public string number { get; set; }
        public string extension { get; set; }
        public object area_code { get; set; }
        public bool verified { get; set; }
    }

    public class AlternativePhone
    {
        public string number { get; set; }
        public string extension { get; set; }
        public string area_code { get; set; }
    }

    public class Seller
    {
        public Phone phone { get; set; }
        public AlternativePhone alternative_phone { get; set; }
        public string nickname { get; set; }
        public string last_name { get; set; }
        public int id { get; set; }
        public string first_name { get; set; }
        public string email { get; set; }
    }

    public class Collector
    {
        public int id { get; set; }
    }

    public class AtmTransferReference
    {
        public string transaction_id { get; set; }
        public object company_id { get; set; }
    }

    public class Payment
    {
        public string reason { get; set; }
        public object status_code { get; set; }
        public double total_paid_amount { get; set; }
        public string operation_type { get; set; }
        public double transaction_amount { get; set; }
        public DateTime date_approved { get; set; }
        public Collector collector { get; set; }
        public object coupon_id { get; set; }
        public int installments { get; set; }
        public string authorization_code { get; set; }
        public int taxes_amount { get; set; }
        public long id { get; set; }
        public DateTime date_last_modified { get; set; }
        public int coupon_amount { get; set; }
        public List<string> available_actions { get; set; }
        public double shipping_cost { get; set; }
        public double ? installment_amount { get; set; }
        public DateTime date_created { get; set; }
        public object activation_uri { get; set; }
        public int overpaid_amount { get; set; }
        public long ? card_id { get; set; }
        public string status_detail { get; set; }
        public string issuer_id { get; set; }
        public string payment_method_id { get; set; }
        public string payment_type { get; set; }
        public object deferred_period { get; set; }
        public AtmTransferReference atm_transfer_reference { get; set; }
        public string site_id { get; set; }
        public int payer_id { get; set; }
        public long order_id { get; set; }
        public string currency_id { get; set; }
        public string status { get; set; }
        public object transaction_order_id { get; set; }
    }

    public class Taxes
    {
        public object amount { get; set; }
        public object currency_id { get; set; }
    }

    public class OrderRequest
    {
        public object change { get; set; }
        public object @return { get; set; }
    }

    public class Feedback
    {
        public object sale { get; set; }
        public object purchase { get; set; }
    }

    public class EstimatedScheduleLimit
    {
        public object date { get; set; }
    }

    public class EstimatedDeliveryFinal
    {
        public DateTime date { get; set; }
        public int offset { get; set; }
    }

    public class EstimatedDeliveryLimit
    {
        public DateTime date { get; set; }
        public int offset { get; set; }
    }

    public class EstimatedHandlingLimit
    {
        public DateTime date { get; set; }
    }

    public class Offset
    {
        public DateTime? date { get; set; }
        public int? shipping { get; set; }
    }

    public class TimeFrame
    {
        public object from { get; set; }
        public object to { get; set; }
    }

    public class EstimatedDeliveryTime
    {
        public DateTime date { get; set; }
        public object pay_before { get; set; }
        public object schedule { get; set; }
        public string unit { get; set; }
        public Offset offset { get; set; }
        public int shipping { get; set; }
        public TimeFrame time_frame { get; set; }
        public int handling { get; set; }
        public string type { get; set; }
    }

    public class EstimatedDeliveryExtended
    {
        public DateTime date { get; set; }
        public int offset { get; set; }
    }

    public class ShippingOption
    {
        public double cost { get; set; }
        public EstimatedScheduleLimit estimated_schedule_limit { get; set; }
        public int shipping_method_id { get; set; }
        public EstimatedDeliveryFinal estimated_delivery_final { get; set; }
        public double list_cost { get; set; }
        public EstimatedDeliveryLimit estimated_delivery_limit { get; set; }
        public string delivery_type { get; set; }
        public EstimatedHandlingLimit estimated_handling_limit { get; set; }
        public EstimatedDeliveryTime estimated_delivery_time { get; set; }
        public string name { get; set; }
        public long id { get; set; }
        public EstimatedDeliveryExtended estimated_delivery_extended { get; set; }
        public string currency_id { get; set; }
    }

    public class Country
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class City
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Municipality
    {
        public object name { get; set; }
        public object id { get; set; }
    }

    public class State
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Neighborhood
    {
        public string name { get; set; }
        public object id { get; set; }
    }

    public class SenderAddress
    {
        public Country country { get; set; }
        public string address_line { get; set; }
        public List<string> types { get; set; }
        public object agency { get; set; }
        public City city { get; set; }
        public string geolocation_type { get; set; }
        public double latitude { get; set; }
        public Municipality municipality { get; set; }
        public string street_name { get; set; }
        public string zip_code { get; set; }
        public string geolocation_source { get; set; }
        public string street_number { get; set; }
        public string comment { get; set; }
        public int id { get; set; }
        public State state { get; set; }
        public Neighborhood neighborhood { get; set; }
        public DateTime geolocation_last_updated { get; set; }
        public double longitude { get; set; }
    }

    public class DimensionsSource
    {
        public string origin { get; set; }
        public string id { get; set; }
    }

    public class ShippingItem
    {
        public int quantity { get; set; }
        public DimensionsSource dimensions_source { get; set; }
        public string description { get; set; }
        public string id { get; set; }
        public string dimensions { get; set; }
    }

    public class ReceiverAddress
    {
        public Country country { get; set; }
        public string address_line { get; set; }
        public List<string> types { get; set; }
        public object agency { get; set; }
        public City city { get; set; }
        public string geolocation_type { get; set; }
        public double latitude { get; set; }
        public Municipality municipality { get; set; }
        public string street_name { get; set; }
        public string zip_code { get; set; }
        public string geolocation_source { get; set; }
        public string delivery_preference { get; set; }
        public string street_number { get; set; }
        public string receiver_name { get; set; }
        public string comment { get; set; }
        public int ? id { get; set; }
        public State state { get; set; }
        public Neighborhood neighborhood { get; set; }
        public DateTime ? geolocation_last_updated { get; set; }
        public string receiver_phone { get; set; }
        public double longitude { get; set; }
    }

    public class CostComponents
    {
        public double loyal_discount { get; set; }
        public int special_discount { get; set; }
        public int compensation { get; set; }
        public double gap_discount { get; set; }
        public double ratio { get; set; }
    }

    public class Shipping
    {
        public double cost { get; set; }
        public string substatus { get; set; }
        public DateTime date_created { get; set; }
        public int receiver_id { get; set; }
        public DateTime ? date_first_printed { get; set; }
        public int sender_id { get; set; }
        public ShippingOption shipping_option { get; set; }
        public string mode { get; set; }
        public SenderAddress sender_address { get; set; }
        public string shipping_mode { get; set; }
        public int service_id { get; set; }
        public string site_id { get; set; }
        public List<ShippingItem> shipping_items { get; set; }
        public ReceiverAddress receiver_address { get; set; }
        public CostComponents cost_components { get; set; }
        public long id { get; set; }
        public object picking_type { get; set; }
        public string currency_id { get; set; }
        public string shipment_type { get; set; }
        public string status { get; set; }
        public string logistic_type { get; set; }
    }

    public class Item
    {
        public string seller_custom_field { get; set; }
        public string condition { get; set; }
        public string category_id { get; set; }
        public object variation_id { get; set; }
        public List<object> variation_attributes { get; set; }
        public object seller_sku { get; set; }
        public string warranty { get; set; }
        public string id { get; set; }
        public string title { get; set; }
    }

    public class OrderItem
    {
        public Item item { get; set; }
        public int quantity { get; set; }
        public double sale_fee { get; set; }
        public string listing_type_id { get; set; }
        public int unit_price { get; set; }
        public int full_unit_price { get; set; }
        public string currency_id { get; set; }
        public object manufacturing_days { get; set; }
    }

    public class Coupon
    {
        public int amount { get; set; }
        public object id { get; set; }
    }

    public class BillingInfo
    {
        public string doc_number { get; set; }
        public string doc_type { get; set; }
    }    

    public class Buyer
    {
        public BillingInfo billing_info { get; set; }
        public Phone phone { get; set; }
        public AlternativePhone alternative_phone { get; set; }
        public string nickname { get; set; }
        public string last_name { get; set; }
        public int id { get; set; }
        public string first_name { get; set; }
        public string email { get; set; }
    }

    public class Result
    {
        public Seller seller { get; set; }
        public List<Payment> payments { get; set; }
        public bool ? fulfilled { get; set; }
        public Taxes taxes { get; set; }
        public OrderRequest order_request { get; set; }
        public DateTime expiration_date { get; set; }
        public Feedback feedback { get; set; }
        public Shipping shipping { get; set; }
        public DateTime date_closed { get; set; }
        public long id { get; set; }
        public object manufacturing_ending_date { get; set; }
        public List<OrderItem> order_items { get; set; }
        public DateTime date_last_updated { get; set; }
        public DateTime last_updated { get; set; }
        public object comments { get; set; }
        public object pack_id { get; set; }
        public Coupon coupon { get; set; }
        public DateTime date_created { get; set; }
        public double total_amount_with_shipping { get; set; }
        public object pickup_id { get; set; }
        public object status_detail { get; set; }
        public List<string> tags { get; set; }
        public Buyer buyer { get; set; }
        public int total_amount { get; set; }
        public double paid_amount { get; set; }
        public List<object> mediations { get; set; }
        public string currency_id { get; set; }
        public string status { get; set; }
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

    public class Paging
    {
        public int total { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
    }

    public class AvailableFilter
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Value> values { get; set; }
    }

    public class Search
    {
        public string query { get; set; }
        public List<Result> results { get; set; }
        public Sort sort { get; set; }
        public List<AvailableSort> available_sorts { get; set; }
        public List<AvailableFilter> available_filters { get; set; }
        public List<object> filters { get; set; }
        public Paging paging { get; set; }
        public string display { get; set; }
    }

    public class Order
    {
        public long id { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_closed { get; set; }
        public DateTime last_updated { get; set; }
        public object manufacturing_ending_date { get; set; }
        public Feedback feedback { get; set; }
        public List<object> mediations { get; set; }
        public object comments { get; set; }
        public object pack_id { get; set; }
        public object pickup_id { get; set; }
        public OrderRequest order_request { get; set; }
        public object fulfilled { get; set; }
        public int total_amount { get; set; }
        public double total_amount_with_shipping { get; set; }
        public double paid_amount { get; set; }
        public Coupon coupon { get; set; }
        public DateTime expiration_date { get; set; }
        public List<OrderItem> order_items { get; set; }
        public string currency_id { get; set; }
        public List<Payment> payments { get; set; }
        public Shipping shipping { get; set; }
        public string status { get; set; }
        public object status_detail { get; set; }
        public List<string> tags { get; set; }
        public Buyer buyer { get; set; }
        public Seller seller { get; set; }
        public Taxes taxes { get; set; }
    }

}