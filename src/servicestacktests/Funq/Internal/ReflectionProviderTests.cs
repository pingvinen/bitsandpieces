using System;
using NUnit.Framework;
using BitsAndPieces.ServiceStack.Funq.Internal;
using servicestacktests.Funq.Internal.ReflectionProviderTestStuff;
using System.Linq;
using servicestacktests.Funq.Internal.ReflectionProviderTestStuff.SubNs;
using servicestacktests.Funq.Internal.ReflectionProviderInjects.OnConstructor;
using servicestacktests.Funq.Internal.ReflectionProviderInjects.OnMethod;
using servicestacktests.Funq.Internal.ReflectionProviderInjects.OnMultipleConstructors;

namespace servicestacktests.Funq.Internal
{
	[TestFixture]
	public class ReflectionProviderTests
	{
		private ReflectionProvider reflectionProvider;

		[SetUp]
		public void Setup()
		{
			reflectionProvider = new ReflectionProvider();
		}

		[Test]
		public void GetTypesInNamespace_works()
		{
			var actual = reflectionProvider.GetTypesInNamespace(typeof(RfpClassWeWant).Namespace);

			var actualList = actual.ToList();

			Assert.AreEqual(2, actualList.Count);
			Assert.Contains(typeof(RfpClassWeWant), actualList);
			Assert.Contains(typeof(RfpClassInSubNamespace), actualList);
		}





		[Test]
		public void GetTypesWithInjectAttribute_onConstructor()
		{
			var actual = reflectionProvider.GetTypesWithInjectAttribute(typeof(RfpInjectOnConstructor).Namespace);

			var actualList = actual.ToList();

			Assert.AreEqual(1, actualList.Count);
			Assert.AreEqual(typeof(RfpInjectOnConstructor), actualList[0].Type);
		}

		[Test]
		public void GetTypesWithInjectAttribute_onMethod()
		{
			var actual = reflectionProvider.GetTypesWithInjectAttribute(typeof(RfpInjectOnMethod).Namespace);

			var actualList = actual.ToList();

			Assert.AreEqual(1, actualList.Count);
			Assert.AreEqual(typeof(RfpInjectOnMethod), actualList[0].Type);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void GetTypesWithInjectAttribute_onMultipleConstructors_throwsException()
		{
			var actual = reflectionProvider.GetTypesWithInjectAttribute(typeof(RfpInjectOnMultipleConstructors).Namespace);

			// because the method is yielding an IEnumerable, we are not hitting the code
			// until we do something with that enumerable... so let's call ToList
			actual.ToList();
		}
	}
}

