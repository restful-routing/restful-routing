using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace RestfulRouting.Mappers
{
    public class RouteMapper : Mapper
    {
        private RouteBase routeBase;

        public RouteMapper(RouteBase routeBase)
        {
            this.routeBase = routeBase;
        }

        public override void RegisterRoutes(RouteCollection routeCollection)
        {
            routeCollection.Add(routeBase);
        }
    }
}
