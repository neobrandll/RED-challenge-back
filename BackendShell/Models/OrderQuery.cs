using API.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Models
{
    public class OrderQuery
    {
        [BindRequired]
        public string customerName { get; set; }
        [BindRequired]
        public OrderType orderType { get; set; }
    }
}
