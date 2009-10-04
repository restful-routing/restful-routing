using NUnit.Framework;

namespace RestfulRouting.Tests
{
	public abstract class Spec
	{
		[TestFixtureSetUp]
		public void SetUp()
		{
			given();
			when();
		}

		protected abstract void given();
		protected abstract void when();
	}
}
