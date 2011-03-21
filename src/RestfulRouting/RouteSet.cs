using System.Web.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestfulRouting
{
    public abstract class RouteSet
    {
        public abstract void Map(Mapper map);

        public void RegisterRoutes(RouteCollection routes)
        {
            var mapper = new Mapper();
            Map(mapper);

            mapper.RegisterRoutes(routes);
        }
    }
}
