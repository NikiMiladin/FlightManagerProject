using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class PagerViewModel
    {
        public int Pages { get; set; }
        public int CurrentPage { get; set; } 
        public int ItemsPerPage { get; set; }
    }
}
