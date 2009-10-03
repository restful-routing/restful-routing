using System;

namespace RestfulRouting
{
	public class RouteConfiguration
	{
		static RouteConfiguration()
		{
			Default = () => new RouteConfiguration();
		}

		public string Controller { get; set; }
		public string Singular { get; set; }
		public string Requirements { get; set; }

		public string PathPrefix { get; set; }

		public static Func<RouteConfiguration> Default { get; set; }
	}
}
