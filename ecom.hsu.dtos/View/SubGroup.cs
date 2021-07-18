using System;
using System.Collections.Generic;
using System.Text;

namespace ecom.hsu.dtos.View
{
    public class SubGroup
    {
        public int id { get; set; }
        public int maingroup_id { get; set; }
        public string name { get; set; }
        public string name_id { get; set; }
        public bool isactived { get; set; }
    }
}
