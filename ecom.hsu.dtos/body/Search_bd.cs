namespace ecom.hsu.dtos.body
{
    public class Search_bd
    {
        public string keyword { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public string filter { get; set; }
        public int? discount { get; set; }
        public Filter.Filter filter_detail { get; set; }
    }
}
