using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.ViewModels
{
    public class CreateAccount
    {
        [DisplayName("First Name :")]
        [Required]
        public string FirstName { get; set; }

        [DisplayName("Last Name :")]
        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [DisplayName("Program:")]
        public int ProgramID { get; set; }

        public bool emailCheck { get; set; }
    }
}