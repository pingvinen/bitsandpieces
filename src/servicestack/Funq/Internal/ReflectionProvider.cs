using System;
using System.Collections.Generic;
using System.Reflection;
using BitsAndPieces.Annotations.IoC;
using System.Linq;

namespace BitsAndPieces.ServiceStack.Funq.Internal
{
	public class ReflectionProvider
	{
		#region GetTypesInNamespace
		public virtual IEnumerable<Type> GetTypesInNamespace(string ns)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();

			Type[] types;

			foreach (Assembly a in assemblies)
			{
				try
				{
					types = a.GetTypes();
				}
				catch (ReflectionTypeLoadException)
				{
					// oh well...
					continue;
				}

				foreach (Type t in types)
				{
					if (!String.IsNullOrEmpty(t.Namespace) && t.Namespace.StartsWith(ns))
					{
						yield return t;
					}
				}
			}
		}
		#endregion

		#region GetTypesWithInjectAttribute
		public virtual IEnumerable<InjectedType> GetTypesWithInjectAttribute(string ns)
		{
			foreach (Type t in this.GetTypesInNamespace(ns))
			{
				var injectMethods = from xx in t.GetMethods()
						where xx.GetCustomAttributes(typeof(InjectAttribute), true).Length > 0
					select xx;

				var methods = injectMethods.ToList();

				var injectConstructors = from xx in t.GetConstructors()
						where xx.GetCustomAttributes(typeof(InjectAttribute), false).Length > 0
					select xx;

				var constructors = injectConstructors.ToList();

				if (constructors.Count > 1)
				{
					throw new InvalidOperationException(
						String.Format(
							  "Only 1 constructor can have the {0} attribute, but {1} has {2}"
							, typeof(InjectAttribute).Name
							, t.Name
							, constructors.Count
						));
				}

				if (methods.Count > 0 || constructors.Count > 0)
				{
					yield return new InjectedType() {
						Type = t
							, Methods = methods
							, Constructor = constructors.FirstOrDefault()
					};
				}
			}
		}
		#endregion
	}
}
