using System;
using NUnit.Framework;
using BitsAndPieces.ServiceStack.Funq.Internal;
using FakeItEasy;
using Funq;

namespace servicestacktests.Funq.Internal
{
	[TestFixture]
	public class ContainerWirererTests
	{
		private ContainerWirerer wirerer;

		private Container container;

		/** MOCKS **/

		private InjectedType injectedType;

		[SetUp]
		public void Setup()
		{
			injectedType = A.Fake<InjectedType>();

			container = new Container();

			wirerer = new ContainerWirerer();
		}

		[Test]
		public void Register_typeCanBeResolvedAfterCall()
		{
			A.CallTo(() => injectedType.Type).Returns(typeof(ContainerWirererInTestClass));

			Assert.IsNull(container.TryResolve<ContainerWirererInTestClass>(), "Should not be possible to resolve type before wiring");

			wirerer.Register(container, injectedType);

			Assert.NotNull(container.TryResolve<ContainerWirererInTestClass>(), "Should be possible to resolve after wiring");
		}
	}
}

