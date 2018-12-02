using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }       

        //Todo: nem lehetne referenciával? 
        public int? ParentCategoryId { get; set; }

        public ICollection<PlannyCategory> PlannyCategories { get; set; }
    }
}
