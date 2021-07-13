using System.Collections.Generic;

namespace Almhd.Shared
{
    public class CategoriesModel
    {
        public BlogItem Blog { get; set; }
       public List<CategoryItem> Categories { get; set; }
       public int CategoryId { get; set; }
       public List<ListModel> AllModels { get; set; }
    }
}
