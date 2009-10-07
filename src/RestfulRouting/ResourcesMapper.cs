using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public class ResourcesMapper<TController> : MapperBase<TController> where TController : Controller
	{
		public ResourcesMapper(RouteCollection routeCollection, RouteConfiguration configuration) : base(routeCollection, configuration)
		{
			_idSegment = "/{id}";
		}

		public override void Map(string resource)
		{
			_controller = resource;

			if (!string.IsNullOrEmpty(_configuration.As))
				resource = _configuration.As;

			_resourcePath = BasePath() + resource;

			MapIndex();

			if (_configuration.Shallow)
			{
				_resourcePath = resource;

				MapIndex();
			}

			MapCreate();

			MapNew();

			MapMemberRoutes();

			MapCollectionRoutes();

			MapMembers();

			MapUpdate();

			MapDestroy();

			MapPostOverrideForPutAndDelete();
		}
		
	}
}