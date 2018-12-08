using System;
using System.Collections.Generic;
using System.Text;

namespace PlannyBackend.Model
{
    public class MainCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
