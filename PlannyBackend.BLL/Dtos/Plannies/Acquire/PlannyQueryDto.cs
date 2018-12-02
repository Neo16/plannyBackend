using PlannyBackend.Model.Enums;
using System;
using System.Collections.Generic;

namespace PlannyBackend.Bll.Dtos
{
    public class PlannyQueryDto
    {
        //kategoria
        public List<int> CategoryIds { get; set; }

        //szűrők kiíróra és résztvevőkre
        public Gender ParticipantsGender { get; set; }

        public int ParticipantsAgeMin { get; set; }

        public int ParticipantsAgeMax { get; set; }

        public int ParticipantsNumberMin { get; set; }

        public int ParticipantsNumberMax { get; set; }

        public bool OnlyPlanniesByFriends { get; set; }       

        public bool OnlySimilarInterestParticipants { get; set; }

        //Szűrők Helyszínre 
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double RangeInKms { get; set; }

        //Dátum 
        public DateTime FromTime { get; set; }

        public DateTime ToTime { get; set; }

        //Sorrend
        public Order Order { get; set; }

    }
}
