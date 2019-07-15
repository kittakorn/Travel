using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Models
{
    class ChangePassword
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
