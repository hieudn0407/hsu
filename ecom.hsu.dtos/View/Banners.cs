using System;
using System.Collections.Generic;
using System.Text;

namespace ecom.hsu.dtos.View
{
    public class Banners
    {
        public DateTime datefrom { get; set; }
        public DateTime dateto { get; set; }
        public string desktop_url { get; set; }
        public string mobile_url { get; set; }
        public string link { get; set; }
    }
}
