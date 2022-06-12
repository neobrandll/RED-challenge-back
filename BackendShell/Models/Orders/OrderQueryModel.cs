using API.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Models.Orders
{
    public class OrderQueryModel
    {
        #nullable enable
        public string? customerName { get; set; }

        #nullable enable
        public int?  orderId { get; set; }
        public OrderType? orderType { get; set; }
    }
}
