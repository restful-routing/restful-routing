using System;
using System.Linq.Expressions;

namespace RestfulRouting.Mappers
{
    public class RootMapper<TController> : StandardMapper
    {
        Expression<Func<TController, object>> _action;

        public RootMapper(Expression<Func<TController, object>> action)
        {
            _action = action;
        }

        public override void RegisterRoutes(System.Web.Routing.RouteCollection routeCollection)
        {
            Map("").To(_action);
            base.RegisterRoutes(routeCollection);
        }
    }
}