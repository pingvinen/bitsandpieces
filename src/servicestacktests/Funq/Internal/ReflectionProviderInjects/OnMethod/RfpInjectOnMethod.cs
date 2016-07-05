using System;

namespace servicestacktests.Funq.Internal.ReflectionProviderInjects.OnMethod
{
	public class RfpInjectOnMethod
	{
		public RfpInjectOnMethod()
		{
		}

		[BitsAndPieces.Annotations.IoC.Inject]
		public void Method()
		{
		}
	}
}

