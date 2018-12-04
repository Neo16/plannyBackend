﻿using PlannyBackend.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlannyBackend.BLL.Dtos.Profile
{
    public class ProfileDto
    {
    
        public string UserName { get; set; }

     
        public int Age { get; set; }

      
        public Gender Gender { get; set; }


        public string SelfIntroduction { get; set; }
       
        public string PictureUrl { get; set; }     
    }
}
