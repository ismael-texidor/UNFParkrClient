using ParkrUNF;
using System;
using NUnit.Framework;

namespace ParkrUnitTest
{
	[TestFixture]
	public class UnitTest
	{
		[Test]
		public void Pass ()
		{
			//var viewModel = new LoginViewController (p);
			Assert.True (true);
		}

		[Test]
		public void Fail ()
		{
			Assert.False (true);
		}

		[Test]
		[Ignore ("another time")]
		public void Ignore ()
		{
			Assert.True (false);
		}
	}
}
