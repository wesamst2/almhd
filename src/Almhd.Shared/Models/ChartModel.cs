using System.Collections.Generic;

namespace Almhd.Shared
{
	public class BarChartModel
	{
		public ICollection<string> Labels { get; set; }
		public ICollection<int> Data { get; set; }
	}
}
