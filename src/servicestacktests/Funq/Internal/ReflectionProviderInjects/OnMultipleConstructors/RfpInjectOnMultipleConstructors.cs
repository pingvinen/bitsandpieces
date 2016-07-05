using System;
using BitsAndPieces.Annotations.IoC;

namespace servicestacktests.Funq.Internal.ReflectionProviderInjects.OnMultipleConstructors
{
	public class RfpInjectOnMultipleConstructors
	{
		[Inject]
		public RfpInjectOnMultipleConstructors()
		{
		}

		[Inject]
		public RfpInjectOnMultipleConstructors(string str)
		{
		}
	}
}

