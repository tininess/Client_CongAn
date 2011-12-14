using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoAuthentication.Models;

namespace DemoAuthentication.Models
{
    public class AccountWrapper
    {
        public UserModel user { get; set; }
        public UserLoginModel login { get; set; }
    }
}