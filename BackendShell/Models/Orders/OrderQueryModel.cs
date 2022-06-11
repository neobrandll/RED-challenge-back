using API.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Models.Orders
{
    public class OrderQueryModel
    {
        [BindRequired]
        public string customerName { get; set; }
        [BindRequired]
        public OrderType orderType { get; set; }
    }
}
