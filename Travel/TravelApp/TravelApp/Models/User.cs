using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public System.DateTime JoinDate { get; set; }
        public Nullable<System.DateTime> LoginDate { get; set; }
    }
}
