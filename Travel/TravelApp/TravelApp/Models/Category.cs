using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Models
{
    class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Place> Places { get; set; }
    }
}
