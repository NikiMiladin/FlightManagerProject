using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FlightManager.Models.Base
{
    public abstract class BaseIndexViewModel
    {
        public PagerViewModel Pager { get; set; }
        public BaseIndexViewModel()
        {
            this.Pager = new PagerViewModel();
        }

    }
}
