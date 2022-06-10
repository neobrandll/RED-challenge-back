using System;
using API.Enums;

namespace API.DataModels
{
    public class Order
    {
        public int OrderId { get; set; }
        public OrderType OrderType { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }
    }
}