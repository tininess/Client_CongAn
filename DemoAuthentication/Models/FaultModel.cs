using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

namespace DemoAuthentication.Models
{
    public class FaultModel
    {
        [Display(Name = "Detail")]
        public string Detail { get; set; }
          [Display(Name = "Issue")]
        public string Issue { get; set; }
       
    }
}