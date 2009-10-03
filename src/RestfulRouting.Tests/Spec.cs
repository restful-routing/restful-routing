using System;
using NUnit.Framework;

namespace RestfulRouting.Tests
{
	public abstract class Spec
	{
		[SetUp]
		public void SetUp()
		{
			given();
			when();
		}

		protected abstract void given();
		protected abstract void when();
	}
}
