using PlannyBackend.Model;

namespace PlannyBackend.BLL.Dtos
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Address { get; set; }        

        public double Lonlongitude { get; set; }
        public double Latitude { get; set; }
    }
}
