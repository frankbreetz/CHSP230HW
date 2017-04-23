using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HW1BirthdayCard.Models
{
    public class BirthdayWish
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string From { get; set; }
        [Required(ErrorMessage = "Please enter the recipients name")]
        public string To { get; set; }
        [Required(ErrorMessage = "Please enter a special message for you friend")]
        public string Message { get; set; }
    }
}