using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Models
{
    class AspNetUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string UserRole { get; set; }
        public System.DateTime JoinDate { get; set; }
        public Nullable<System.DateTime> LoginDate { get; set; }

        public  ICollection<Comment> Comments { get; set; }
    }
}
