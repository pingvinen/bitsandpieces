using Funq;
using BitsAndPieces.Annotations.IoC; // needed by MonoDevelop for the class summary reference
using BitsAndPieces.ServiceStack.Funq.Internal;

namespace BitsAndPieces.ServiceStack.Funq
{
	/// <summary>
	/// Automatically register types with one or more methods flagged
	/// with <see cref="InjectAttribute"/> in the container.
	/// 
	/// Note: for now just registers without taking into account the
	/// methods/constructors that are flagged. Currently it simply mimics
	/// container.RegisterAutoWired<T>();
	/// </summary>
	public class AutoRegister
	{
		private readonly ReflectionProvider reflectionProvider;
		private readonly ContainerWirerer wirerer;

		/// <summary>
		/// This constructor is meant to be used when consuming the class
		/// </summary>
		public AutoRegister() : this(new ReflectionProvider(), new ContainerWirerer())
		{
		}

		/// <summary>
		/// This constructor is meant for testing purposes, as it provides an injection
		/// point for Mocks
		/// </summary>
		/// <param name="reflectionProvider">Reflection provider</param>
		/// <param name="wirerer">Wirerer</param>
		public AutoRegister(ReflectionProvider reflectionProvider, ContainerWirerer wirerer)
		{
			this.reflectionProvider = reflectionProvider;
			this.wirerer = wirerer;
		}

		public virtual void RegisterFromNamespace(Container container, string ns)
		{
			// look at all classes in the custom assemblies
			// if a class has one or more methods with the Inject
			// attribute, register it in the container using a
			// custom factory thingy that calls all the flagged
			// methods with their parameters

			foreach (InjectedType t in this.reflectionProvider.GetTypesWithInjectAttribute(ns))
			{
				// Console.WriteLine("--> {0}", t.Type.Name);
				this.wirerer.Register(container, t);
			}
		}
	}
}
