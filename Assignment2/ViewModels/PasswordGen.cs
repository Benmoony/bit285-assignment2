using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.ViewModels
{
    public class PasswordGen
    {
        [DisplayName("Last Name:")]
        [Required]
        public string LastName { get; set; }


        [DisplayName("Birth Year:")]
        [Required]
        [DataType(DataType.Date)]
        public string BirthYear { get; set; }

        [DisplayName("Favorite Color:")]
        [Required]
        public string FavColor { get; set; }

        [DisplayName("Passwords: ")]
        public SelectList PasswordOptions { get; set; }

        public string SelectedPassword { get; set; }
    }
}