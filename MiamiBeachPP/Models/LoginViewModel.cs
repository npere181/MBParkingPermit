using System;
using System.Collections.Generic;

namespace MiamiBeachPP.Models
{
    public partial class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? Rememberme { get; set; }
    }
}
