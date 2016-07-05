using System;
using NUnit.Framework;
using BitsAndPieces.ServiceStack.Funq;
using BitsAndPieces.ServiceStack.Funq.Internal;
using Funq;
using FakeItEasy;
using System.Collections.Generic;

namespace servicestacktests.Funq
{
	[TestFixture]
	public class AutoRegisterTests
	{
		private AutoRegister autoRegister;

		private Container container;

		private ReflectionProvider reflectionProvider;

		private ContainerWirerer wirerer;

		[SetUp]
		public void Setup()
		{
			reflectionProvider = A.Fake<ReflectionProvider>();
			wirerer = A.Fake<ContainerWirerer>();
			container = new Container();

			autoRegister = new AutoRegister(reflectionProvider, wirerer);
		}

		[Test]
		public void RegisterFromNamespace_callsReflectionProvider_WithCorrectParams()
		{
			autoRegister.RegisterFromNamespace(container, "Awesome.Namespace");

			A.CallTo(() => reflectionProvider.GetTypesWithInjectAttribute("Awesome.Namespace")).MustHaveHappened();
		}

		[Test]
		public void RegisterFromNamespace_callsWirerer_WithCorrectParams()
		{
			var injectedType = A.Fake<InjectedType>();
			A.CallTo(() => reflectionProvider.GetTypesWithInjectAttribute(A<string>.Ignored)).Returns(new List<InjectedType>() {{ injectedType }});

			autoRegister.RegisterFromNamespace(container, "Awesome.Namespace");

			A.CallTo(() => wirerer.Register(container, injectedType)).MustHaveHappened();
		}
	}
}
