using PlannyBackend.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlannyBackend.BLL.Dtos.Profile
{
    public class ProfileDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public string SelfIntroduction { get; set; }
       
        public string PictureUrl { get; set; }     
    }
}
