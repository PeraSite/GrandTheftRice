using System;

namespace PeraCore.Runtime {
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]

	public class SceneSelectorAttribute : Attribute {
		public readonly string Filter;

		public SceneSelectorAttribute(string filter = "HotelLavie") {
			Filter = filter;
		}
	}

}
