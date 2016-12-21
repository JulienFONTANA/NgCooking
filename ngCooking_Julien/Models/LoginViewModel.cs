using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ngCooking_Julien.Models
{
    public class LogViewModel
    {
        [Required]
        public string mail { get; set; }
        [Required]
        public string password { get; set; }
    }
}