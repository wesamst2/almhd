using System.Collections.Generic;

namespace Almhd.Shared
{
	public class PageListModel
   {
      public IEnumerable<PostItem> Posts { get; set; }
      public Pager Pager { get; set; }
   }
}
