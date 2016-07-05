using System;
using Funq;
using System.Reflection;

namespace BitsAndPieces.ServiceStack.Funq.Internal
{
	public class ContainerWirerer
	{
		public virtual void Register(Container container, InjectedType type)
		{
			/**
			 * TODO make a factory that calls all the inject methods
			 * and the correct constructor
			 */

			Type containerType = container.GetType();
			MethodInfo registerAutoWired = containerType.GetMethod("RegisterAutoWired");

			var generic = registerAutoWired.MakeGenericMethod(new [] { type.Type });
			generic.Invoke(container, new Object[0]);
		}
	}
}
