using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public class ResourceMapper<TController> : MapperBase<TController> where TController : Controller
	{
		public ResourceMapper(RouteCollection routeCollection, RouteConfiguration configuration) : base(routeCollection, configuration)
		{
			_idSegment = "";
		}

		public override void Map(string resource)
		{
			_controller = resource;

			if (!string.IsNullOrEmpty(_configuration.As))
				resource = _configuration.As;

			_resourcePath = BasePath() + resource;

			MapCreate();

			MapNew();

			MapMemberRoutes();

			MapMembers();

			MapUpdate();

			MapDestroy();

			MapPostOverrideForPutAndDelete();
		}
	}
}