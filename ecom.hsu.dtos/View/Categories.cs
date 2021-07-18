using System;
using System.Collections.Generic;
using System.Text;

namespace ecom.hsu.dtos.View
{
    public class Categories
    {
        public string object_name { get; set; }
        public string object_name_id { get; set; }
        public string maingroup_name { get; set; }
        public string maingroup_name_id { get; set; }
        public string maingroup_image { get; set; }
        public string subgroup_name { get; set; }
        public string subgroup_name_id { get; set; }
        public string brand_link { get; set; }
        public string brand_logo_url { get; set; }
        public string brand_name_id { get; set; }
        public string brand_logo_icon { get; set; }
        public string subgroup_image_url { get; set; }

    }

    public class Categories_View
    {
        public string name { get; set; }
        public List<Categories> data { get; set; }
    }
}
