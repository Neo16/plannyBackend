using PlannyBackend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Bll.BllModels
{
    public class ProposalQuery
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
        public String SettlementName { get; set; }

        public bool OnlyAtGivenSettlement { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double RangeInKms { get; set; }

        //Dátum 
        public DateTime FromTime { get; set; }

        public DateTime ToTime { get; set; }

        //Sorrend
        public ProposalOrder Order { get; set; }
    }
}
