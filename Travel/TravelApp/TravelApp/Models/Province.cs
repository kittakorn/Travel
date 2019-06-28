using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Models
{
    class Province
    {

        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public virtual ICollection<Place> Places { get; set; }
    }
}
