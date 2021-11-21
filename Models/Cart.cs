using System;
using System.Collections.Generic;


namespace MaxPizzaProject.Models
{
    public class Cart
    {

        public List<OrderInformation> Orders { get; set; } = new List<OrderInformation>();

        
        public virtual void AddItem(OrderInformation order)
        {
            Orders.Add(order);
        }

        public virtual void RemoveLine(Guid orderId) =>
            Orders.RemoveAll(o => o.OrderId == orderId);
        public virtual void Clear() => Orders.Clear();

    }

    public class OrderInformation
    {
        public OrderInformation()
        {
            Guid g = Guid.NewGuid();
            OrderId = g;
        }

        public Guid OrderId { get; set; }
        public long ProdId { get; set; }
        public Dictionary<string, string> Topp { get; set; }

        public string ProductName { get; set; }

        public decimal TotalPrice { get; set; }

        public string Size { get; set; }
    }


}


