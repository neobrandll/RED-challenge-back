using API.Enums;

namespace API.Projections
{
    public class SingleOrderProjection
    {
        public SingleOrderProjection(DataModels.Order o)
        {
            OrderId = o.OrderId;
            CustomerName = o.CustomerName;
            OrderType = o.OrderType;     
        }

        public int OrderId { get; set; }
        public OrderType OrderType { get; set; }
        public string CustomerName { get; set; }
           }
}

