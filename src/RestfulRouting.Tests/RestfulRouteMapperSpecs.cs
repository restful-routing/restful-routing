using System.Web.Routing;
using NUnit.Framework;
using RestfulRouting;
using RestfulRouting.Tests;
using NUnit.Should;

namespace RestfulRouteMapperSpecs
{
	[TestFixture]
	public class when_changing_the_configuration_for_a_mapping : Spec
	{
		private RestfulRouteMapper _map;

		protected override void given()
		{
			_map = new RestfulRouteMapper(new RouteCollection());
		}

		protected override void when()
		{
			_map.Resources<BlogsController>(config => config.As = "test");
		}

		[Test]
		public void should_not_retain_configuration_for_other_mappings()
		{
			_map.Resources<PostsController>(config =>
			                                	{
			                                		config.As.ShouldBeNull();
			                                	});
		}

		[Test]
		public void should_not_retain_configuration_for_other_mappings_with_nested_resources()
		{
			_map.Resources<PostsController>(config =>
			{
				config.As.ShouldBeNull();
			}, map => map.Resources<PostsController>());
		}

		[Test]
		public void should_not_retain_configuration_for_other_mappings_with_a_nested_resource()
		{
			_map.Resources<PostsController>(config =>
			{
				config.As.ShouldBeNull();
			}, map => map.Resource<PostsController>());
		}

		[Test]
		public void should_not_retain_configuration_for_other_resources_mappings()
		{
			_map.Resources<PostsController>();
			_map.RouteConfiguration.As.ShouldBeNull();
		}
	}
}
