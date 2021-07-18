using System;
using System.Collections.Generic;
using System.Text;

namespace ecom.hsu.dtos.View
{
    public class Brands
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string name_short { get; set; }
        public string title { get; set; }
        public string logo_url { get; set; }
        public string logo_icon { get; set; }
        public string aff_link { get; set; }
    }
}
