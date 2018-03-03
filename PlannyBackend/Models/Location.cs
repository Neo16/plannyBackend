using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
      
        public Settlement Settlement { get; set; }

        public int SettlementId { get; set; }

        public string  StreetAddress { get; set; }

        public double Lonlongitude { get; set; }
        public double Latitude { get; set; }

    }
}
