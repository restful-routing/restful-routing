using System.Web.Routing;
using RestfulRouting.Mappers;

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
