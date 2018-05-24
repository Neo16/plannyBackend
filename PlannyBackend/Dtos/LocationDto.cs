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
        public string Address { get; set; }        

        public double Lonlongitude { get; set; }
        public double Latitude { get; set; }

        public LocationDto(Location original)
        {
            if (original != null)
            {
                this.Id = original.Id;
                this.Latitude = original.Longitude;
                this.Address = original.Address;              
            }
                   
        }

        public Location ToEntity()
        {
            var result = new Location();
            result.Id = this.Id;
            result.Latitude = this.Lonlongitude;
            result.Address = this.Address;             

            return result;
        }
    }
}
