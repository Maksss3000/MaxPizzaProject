using System;
using System.Text.Json.Serialization;
using MaxPizzaProject.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MaxPizzaProject.Models
{
    public class SessionCart : Cart
    {
        
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            
            SessionCart cart = session?.GetJson<SessionCart>(CurrentSession.currentSession) ?? new SessionCart();
            cart.Session = session;
            return cart;
        }
        
        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(OrderInformation order)
        {
            base.AddItem(order);
            Session.SetJson(CurrentSession.currentSession, this);
          
        }

        public override void RemoveLine(Guid orderId)
        {
            base.RemoveLine(orderId);
            Session.SetJson(CurrentSession.currentSession, this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove(CurrentSession.currentSession);
        }
    }
}
