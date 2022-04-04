using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminPart.Models.Entity
{
    public class UserModel
    {
        public int userid { get; set; }
        [Required(ErrorMessage ="Please provide username")]
        public string username { get; set; }
        [Required(ErrorMessage = "Please provide password")]
        public string password { get; set; }
        public string role { get; set; }
    }
}