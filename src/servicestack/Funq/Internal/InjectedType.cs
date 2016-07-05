using System;
using System.Collections.Generic;
using System.Reflection;

namespace BitsAndPieces.ServiceStack.Funq.Internal
{
	public class InjectedType
	{
		private IList<MethodInfo> methods = new List<MethodInfo>();

		public virtual Type Type { get; set; }
		public virtual ConstructorInfo Constructor { get; set; }

		public virtual IList<MethodInfo> Methods
		{
			get
			{
				return this.methods;
			}
			set
			{
				methods = value;
			}
		}

		public virtual bool HasInjectMethods
		{
			get { return methods.Count > 0; }
		}

		public virtual bool HasInjectConstructor
		{
			get { return Constructor != default(ConstructorInfo); }
		}
	}
}

