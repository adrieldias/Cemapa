using System;
using System.Collections.Generic;

namespace Cemapa.Models.MercadoLivre.Usuario
{
    public class Identification
    {
        public string number { get; set; }
        public string type { get; set; }
    }

    public class Address
    {
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip_code { get; set; }
    }

    public class Phone
    {
        public object area_code { get; set; }
        public string extension { get; set; }
        public string number { get; set; }
        public bool verified { get; set; }
    }

    public class AlternativePhone
    {
        public string area_code { get; set; }
        public string extension { get; set; }
        public string number { get; set; }
    }

    public class BillData
    {
        public object accept_credit_note { get; set; }
    }

    public class Ratings
    {
        public int negative { get; set; }
        public double neutral { get; set; }
        public double positive { get; set; }
    }

    public class Transactions
    {
        public int canceled { get; set; }
        public int completed { get; set; }
        public string period { get; set; }
        public Ratings ratings { get; set; }
        public int total { get; set; }
    }

    public class Sales
    {
        public string period { get; set; }
        public int completed { get; set; }
    }

    public class Claims
    {
        public string period { get; set; }
        public double rate { get; set; }
    }

    public class DelayedHandlingTime
    {
        public string period { get; set; }
        public double rate { get; set; }
    }

    public class Cancellations
    {
        public string period { get; set; }
        public double rate { get; set; }
    }

    public class Metrics
    {
        public Sales sales { get; set; }
        public Claims claims { get; set; }
        public DelayedHandlingTime delayed_handling_time { get; set; }
        public Cancellations cancellations { get; set; }
    }

    public class SellerReputation
    {
        public string level_id { get; set; }
        public object power_seller_status { get; set; }
        public Transactions transactions { get; set; }
        public Metrics metrics { get; set; }
    }

    public class Canceled
    {
        public object paid { get; set; }
        public object total { get; set; }
    }

    public class NotYetRated
    {
        public object paid { get; set; }
        public object total { get; set; }
        public object units { get; set; }
    }

    public class Unrated
    {
        public object paid { get; set; }
        public object total { get; set; }
    }

    public class Transactions2
    {
        public Canceled canceled { get; set; }
        public object completed { get; set; }
        public NotYetRated not_yet_rated { get; set; }
        public string period { get; set; }
        public object total { get; set; }
        public Unrated unrated { get; set; }
    }

    public class BuyerReputation
    {
        public int canceled_transactions { get; set; }
        public List<string> tags { get; set; }
        public Transactions2 transactions { get; set; }
    }

    public class Billing
    {
        public bool allow { get; set; }
        public List<object> codes { get; set; }
    }

    public class ImmediatePayment
    {
        public List<object> reasons { get; set; }
        public bool required { get; set; }
    }

    public class Buy
    {
        public bool allow { get; set; }
        public List<object> codes { get; set; }
        public ImmediatePayment immediate_payment { get; set; }
    }

    public class ShoppingCart
    {
        public string buy { get; set; }
        public string sell { get; set; }
    }

    public class ImmediatePayment2
    {
        public List<object> reasons { get; set; }
        public bool required { get; set; }
    }

    public class List
    {
        public bool allow { get; set; }
        public List<object> codes { get; set; }
        public ImmediatePayment2 immediate_payment { get; set; }
    }

    public class ImmediatePayment3
    {
        public List<object> reasons { get; set; }
        public bool required { get; set; }
    }

    public class Sell
    {
        public bool allow { get; set; }
        public List<object> codes { get; set; }
        public ImmediatePayment3 immediate_payment { get; set; }
    }

    public class Status
    {
        public Billing billing { get; set; }
        public Buy buy { get; set; }
        public bool confirmed_email { get; set; }
        public ShoppingCart shopping_cart { get; set; }
        public bool immediate_payment { get; set; }
        public List list { get; set; }
        public string mercadoenvios { get; set; }
        public string mercadopago_account_type { get; set; }
        public bool mercadopago_tc_accepted { get; set; }
        public object required_action { get; set; }
        public Sell sell { get; set; }
        public string site_status { get; set; }
        public string user_type { get; set; }
    }

    public class Company
    {
        public string brand_name { get; set; }
        public string city_tax_id { get; set; }
        public string corporate_name { get; set; }
        public string identification { get; set; }
        public string state_tax_id { get; set; }
        public object soft_descriptor { get; set; }
    }

    public class Credit
    {
        public double consumed { get; set; }
        public string credit_level_id { get; set; }
        public string rank { get; set; }
    }

    public class Context
    {
    }

    public class Thumbnail
    {
        public string picture_id { get; set; }
        public string picture_url { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public DateTime registration_date { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public string country_id { get; set; }
        public string email { get; set; }
        public Identification identification { get; set; }
        public List<string> internal_tags { get; set; }
        public Address address { get; set; }
        public Phone phone { get; set; }
        public AlternativePhone alternative_phone { get; set; }
        public string user_type { get; set; }
        public List<string> tags { get; set; }
        public object logo { get; set; }
        public int points { get; set; }
        public string site_id { get; set; }
        public string permalink { get; set; }
        public List<string> shipping_modes { get; set; }
        public string seller_experience { get; set; }
        public BillData bill_data { get; set; }
        public SellerReputation seller_reputation { get; set; }
        public BuyerReputation buyer_reputation { get; set; }
        public Status status { get; set; }
        public string secure_email { get; set; }
        public Company company { get; set; }
        public Credit credit { get; set; }
        public Context context { get; set; }
        public Thumbnail thumbnail { get; set; }
    }

    public class UserSimple
    {
        public string identification_type { get; set; }
        public string identification_number { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string zip_dode { get; set; }
        public Phone phone { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public Company company { get; set; }
    
    }

    public class OAuth
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
        public string user_id { get; set; }
        public string refresh_token { get; set; }
    }

    public class Paging
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int total { get; set; }
    }

    public class Order
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class AvailableOrder
    {
        public object id { get; set; }
        public string name { get; set; }
    }

    public class Items
    {
        public string seller_id { get; set; }
        public object query { get; set; }
        public Paging paging { get; set; }
        public List<string> results { get; set; }
        public List<object> filters { get; set; }
        public List<Order> orders { get; set; }
        public List<AvailableOrder> available_orders { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public string name { get; set; }
        public int results { get; set; }
        public object @struct { get; set; }
    }
}