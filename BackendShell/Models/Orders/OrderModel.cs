using API.Enums;
using System;

namespace API.Models.Orders
{
    public class OrderModel
    {
            public int OrderId { get; set; }
            public OrderType OrderType { get; set; }
            public string CustomerName { get; set; }
            public DateTime CreatedDate { get; set; }
                    
    
}
}
