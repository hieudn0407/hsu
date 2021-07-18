using System;
using System.Collections.Generic;
using System.Text;

namespace ecom.hsu.dtos.Filter
{
    public class Filter
    {
        public string object_name_id { get; set; }
        public string price_min { get; set; }
        public string price_max { get; set; }
        public List<string> brands { get; set; }
    }
}
