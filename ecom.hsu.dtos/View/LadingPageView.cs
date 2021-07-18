using System;
using System.Collections.Generic;
using System.Text;

namespace ecom.hsu.dtos.View
{
    public class LadingPageView
    {
        public string page_id { get; set; }
        public string name { get; set; }
        public List<Products> products { get; set; }
        public List<Brands> brands { get; set; }
        public string link { get; set; }
    }
}
