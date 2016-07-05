using System;

namespace BitsAndPieces.Annotations.IoC
{
	/// <summary>
	/// Mark a method as one to use for dependency injection
	/// </summary>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor)]
	public class InjectAttribute : Attribute
	{
	}
}
