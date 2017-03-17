using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment2.ViewModels
{
    public class Login
    {
        [Required]
        public string UserName { get; set;}
        [Required]
        public string Password { get; set;}
        public string Error { get; set; }
    }
}