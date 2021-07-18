using System;
using System.Collections.Generic;
using System.Text;

namespace ecom.hsu.dtos.View
{
    public class Products
    {
        public int id { get; set; }
        public int brand_id { get; set; }
        public int maingroup_id { get; set; }
        public int subgroup_id { get; set; }
        public string ecom_brand_id { get; set; }
        public string name { get; set; }
        public long retail_price { get; set; } = 0;
        public long sale_price { get; set; } = 0;
        public string image_url { get; set; }
        public string link { get; set; }
        public string logo_url { get; set; }
        public double percent
        {
            get
            {
                return Math.Round(((double)retail_price - (double)sale_price) / retail_price * 100, 0);
            }
        }
    }
}
