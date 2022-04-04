using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminPart.Models.Entity
{
    public class AdminModel
    {

        public int adminid { get; set; }
        [Required]
        [Display(Name = "User name")]
        public string adminname { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string password { get; set; }
        public int userid { get; set; }
    }
}