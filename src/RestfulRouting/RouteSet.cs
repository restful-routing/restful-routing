using System.Web.Routing;
using RestfulRouting.Mappers;

namespace RestfulRouting
{
    public abstract class RouteSet
    {
        public static bool RouteToLowercase = true;
        public static bool MapToLowercase = false;

        public abstract void Map(IMapper map);

        public void RegisterRoutes(RouteCollection routes)
        {
            var mapper = new Mapper();
            Map(mapper);

            mapper.RegisterRoutes(routes);
        }
    }
}
