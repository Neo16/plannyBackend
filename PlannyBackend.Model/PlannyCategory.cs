using PlannyBackend.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlannyBackend.Model
{
    public class PlannyCategory
    {
        public Planny Planny { get; set; }
        public int PlannyId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
