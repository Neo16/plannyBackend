using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlannyBackend.Models;

namespace PlannyBackend.Dtos
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }  

        public int SettlementId { get; set; }

        public string StreetAddress { get; set; }

        public double Lonlongitude { get; set; }
        public double Latitude { get; set; }

        public LocationDto(Location original)
        {
            if (original != null)
            {
                this.Id = original.Id;
                this.Latitude = original.Lonlongitude;
                this.Name = original.Name;
                this.SettlementId = original.SettlementId;
                this.StreetAddress = original.StreetAddress;
            }
                   
        }

        public Location ToEntity()
        {
            var result = new Location();
            result.Id = this.Id;
            result.Latitude = this.Lonlongitude;
            result.Name = this.Name;
            result.SettlementId = this.SettlementId;
            result.StreetAddress = this.StreetAddress;          

            return result;
        }
    }
}
