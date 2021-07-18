using System.Collections.Generic;
using System.Linq;
using ecom.hsu.dtos;
using ecom.hsu.dtos.View;
using ecom.hsu.database;
using ecom.hsu.dtos.Filter;

namespace ecom.hsu.services
{
    public class CommonService
    {
        public List<Products> Get_Sale_Product()
        {
            string sql_query = "select p.*, b.logo_url from storemy.products p left join storemy.brands b on p.brand_id = b.id  order by random() limit 20";

            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            return funtion.GetAll<Products>("storemy.excute_sql", param).data;
        }

        public List<Products> Get_Super_Sale()
        {
            string sql_query = "select p.*, b.logo_url from storemy.products p left join storemy.brands b on p.brand_id = b.id  order by p.sale_price limit 8";

            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            return funtion.GetAll<Products>("storemy.excute_sql", param).data;
        }

        public List<Products> Get_Product_Cate(string objectgroup_id, string maingroup_id, int limit, int offset, string filter, Filter filter_detail)
        {
            string sql_query = string.Format("select p.*, o.name_id object_name_id, m.name_id maingroup_name_id, b.logo_url" +
                " from storemy.products p " +
                " left join storemy.objectgroup o on p.objectid = o.id" +
                " left join storemy.maingroup m on p.maingroup_id = m.id" +
                " left join storemy.brands b on p.brand_id = b.id" +
                " where o.name_id = '{0}' and m.name_id = '{1}' {2}" +
                $" order by p.{Filter(filter)}" +
                " limit {3} offset {4}", objectgroup_id, maingroup_id, FilterDetail("p", "b", filter_detail), limit, offset);

            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            return funtion.GetAll<Products>("storemy.excute_sql", param).data;
        }

        public List<Products> Get_Product_Cate_Detail(string objectgroup_id, string maingroup_id, string subgroup_id, int limit, int offset, string filter, Filter filter_detail)
        {
            string sql_query = string.Format("select p.*, o.name_id object_name_id, m.name_id maingroup_name_id , s.name_id subgroup_name_id, b.logo_url" +
                " from storemy.products p " +
                " left join storemy.objectgroup o on p.objectid = o.id" +
                " left join storemy.maingroup m on p.maingroup_id = m.id" +
                " left join storemy.subgroup s on p.subgroup_id = s.id" +
                " left join storemy.brands b on p.brand_id = b.id" +
                " where o.name_id = '{0}' and m.name_id = '{1}' and s.name_id = '{2}' {3}" +
                $" order by p.{Filter(filter)}" +
                " limit {4} offset {5}", objectgroup_id, maingroup_id, subgroup_id, FilterDetail("p", "b", filter_detail), limit, offset);

            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            return funtion.GetAll<Products>("storemy.excute_sql", param).data;
        }

        public List<Products> Get_Product_Brand(string brand_id, int limit, int offset, string filter, Filter filter_detail)
        {
            string sql_query = string.Format("select p.*, b.logo_url" +
                " from storemy.products p" +
                " left join storemy.brands b on p.brand_id = b.id" +
                " where b.name_short  = '{0}' {1}" +
                $" order by p.{Filter(filter)}" +
                " limit {2} offset {3}", brand_id, FilterDetail("p", "b", filter_detail), limit, offset);

            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            return funtion.GetAll<Products>("storemy.excute_sql", param).data;
        }

        public List<Products> Get_Product_Brand_Object_Maingroup(string brand_name_id, string object_name_id, string maingroup_name_id, int limit, int offset, string filter, Filter filter_detail)
        {
            string sql_query = string.Format("select p.*, b.logo_url" +
                " from storemy.products p" +
                " left join storemy.brands b on p.brand_id = b.id" +
                " left join storemy.objectgroup o on p.objectid = o.id " +
                " left join storemy.maingroup m on p.maingroup_id = m.id " +
                " where b.name_short  = '{0}' and o.name_id = '{1}' and m.name_id = '{2}' {3}" +
                $" order by p.{Filter(filter)}" +
                " limit {4} offset {5}", brand_name_id, object_name_id, maingroup_name_id, FilterDetail("p", "b", filter_detail), limit, offset);

            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            return funtion.GetAll<Products>("storemy.excute_sql", param).data;
        }

        public List<Products> Get_Product_Brand_Object_Maingroup_Subgroup(string brand_name_id, string object_name_id, string maingroup_name_id, string subgroup_name_id, int limit, int offset, string filter, Filter filter_detail)
        {
            string sql_query = string.Format("select p.*, b.logo_url" +
                " from storemy.products p" +
                " left join storemy.brands b on p.brand_id = b.id" +
                " left join storemy.objectgroup o on p.objectid = o.id" +
                " left join storemy.maingroup m on p.maingroup_id = m.id" +
                " left join storemy.subgroup s on p.subgroup_id = s.id " +
                " where b.name_short  = '{0}' and o.name_id = '{1}' and m.name_id = '{2}' and s.name_id = '{3}' {4}" +
                $" order by p.{Filter(filter)}" +
                " limit {5} offset {6}", brand_name_id, object_name_id, maingroup_name_id, subgroup_name_id, FilterDetail("p", "b", filter_detail), limit, offset);

            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            return funtion.GetAll<Products>("storemy.excute_sql", param).data;
        }

        private string Filter(string filter)
        {
            switch (filter)
            {
                case "all":
                    {
                        return "updateddate";
                    }
                case "new":
                    {
                        return "sale_price desc";
                    }
                case "sale":
                    {
                        return "sale_price";
                    }
                default: return "updateddate";
            }
        }

        private string FilterDetail(string key_object, string key_product, string key_brand, Filter filter)
        {
            if (filter == null)
                return "";

            string sql = $"and {key_object}.name_id = '{filter.object_name_id}' and {key_product}.sale_price between {filter.price_min} and {filter.price_max}";
            if (filter.brands != null)
            {
                var temp = string.Join(" or ", filter.brands);
                sql += $" and ({temp})";
            }

            return sql;
        }

        private string FilterDetail(string key_product, string key_brand, Filter filter)
        {
            if (filter == null)
                return "";

            string sql = $"and {key_product}.sale_price between {filter.price_min} and {filter.price_max}";
            if (filter.brands != null && filter.brands.Count > 0)
            {
                var temp = string.Join("','", filter.brands);
                sql += $" and {key_brand}.name_short in ('{temp}')";
            }

            return sql;
        }

        public List<Categories> Get_Subgroup(string objectgroup_id, string maingroup_id)
        {
            string sql_query = string.Format("select s.name subgroup_name" +
                " ,s.name_id subgroup_name_id " +
                " ,s.image subgroup_image_url " +
                " ,m.name maingroup_name" +
                " ,m.name_id maingroup_name_id" +
                " ,o.name object_name" +
                " ,o.name_id object_name_id" +
                " from storemy.subgroup s" +
                " left join storemy.maingroup m on s.maingroup_id = m.id" +
                " left join storemy.objectgroup o on m.objectid = o.id" +
                " where o.name_id = '{0}' and m.name_id = '{1}'", objectgroup_id, maingroup_id);

            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            return funtion.GetAll<Categories>("storemy.excute_sql", param).data;
        }

        public List<Categories> Get_Brand_Categories(string brand_name_id)
        {
            string sql_query = string.Format("select o.name object_name" +
                " ,o.name_id object_name_id" +
                " ,m.name maingroup_name" +
                " ,m.name_id maingroup_name_id" +
                " ,m.image maingroup_image" +
                " ,x.aff_link brand_link" +
                " ,x.logo_url brand_logo_url" +
                " ,x.name_short brand_name_id" +
                " ,x.logo_icon brand_logo_icon" +
                " from (select p.objectid, p.maingroup_id, b.aff_link, b.logo_url, b.name_short, b.logo_icon" +
                " from storemy.brands b " +
                " left join storemy.products p on b.id = p.brand_id" +
                " where b.name_short = '{0}'" +
                " group by b.name ,p.objectid, p.maingroup_id, b.aff_link, b.logo_url, b.name_short, b.logo_icon) x" +
                " left join storemy.objectgroup o on x.objectid = o.id" +
                " left join storemy.maingroup m on x.maingroup_id = m.id", brand_name_id);

            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            return funtion.GetAll<Categories>("storemy.excute_sql", param).data;
        }

        public List<Categories> Get_Brand_Subgroup(string brand_name_id, string object_name_id, string maingroup_name_id)
        {
            string sql_query = string.Format("select o.name object_name" +
                " ,o.name_id object_name_id" +
                " ,m.name maingroup_name" +
                " ,m.name_id maingroup_name_id" +
                " ,m.image maingroup_image" +
                " ,x.aff_link brand_link" +
                " ,x.logo_url brand_logo_url" +
                " ,x.name_short brand_name_id" +
                " ,s.name subgroup_name" +
                " ,s.name_id subgroup_name_id" +
                " from (select p.objectid, p.maingroup_id, b.aff_link, b.logo_url, b.name_short, p.subgroup_id" +
                " from storemy.brands b " +
                " left join storemy.products p on b.id = p.brand_id" +
                " where p.subgroup_id is not null and b.name_short = '{0}'" +
                " group by  b.name ,p.objectid, p.maingroup_id, b.aff_link, b.logo_url, b.name_short, p.subgroup_id) x" +
                " left join storemy.objectgroup o on x.objectid = o.id" +
                " left join storemy.maingroup m on x.maingroup_id = m.id" +
                " left join storemy.subgroup s on x.subgroup_id = s.id" +
                " where o.name_id = '{1}' and m.name_id = '{2}'", brand_name_id, object_name_id, maingroup_name_id);

            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            return funtion.GetAll<Categories>("storemy.excute_sql", param).data;
        }

        public List<Brands> Get_List_Brand_Relate(string object_name_id, string maingroup_name_id)
        {
            string sql_query = string.Format("select b.name_short, b.logo_icon, b.aff_link from storemy.products p " +
                " left join storemy.brands b on p.brand_id = b.id" +
                " left join storemy.maingroup m on p.maingroup_id = m.id" +
                " left join storemy.objectgroup o on p.objectid = o.id " +
                " where o.name_id = '{0}' and m.name_id ='{1}'" +
                " group by b.name_short, b.logo_icon, b.aff_link", object_name_id, maingroup_name_id);

            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            return funtion.GetAll<Brands>("storemy.excute_sql", param).data;
        }

        public void Update_Table_Product(string sql_query)
        {
            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            funtion.GetAll<Products>("storemy.update_sql", param);
        }

        public List<Products> Seach(string keyword, int limit, int offset)
        {
            string sql_query = $"select p.*, b.logo_url from storemy.products p left join storemy.brands b on p.brand_id = b.id where p.keyword ilike '%{keyword.NonUnicode()}%' limit {limit} offset {offset}";
            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            var data = funtion.GetAll<Products>("storemy.excute_sql", param).data;

            return data;
        }

        public List<Brands> Get_Landing_Brands(string page_id)
        {
            string sql_query = string.Empty;
            switch (page_id)
            {
                case "thoi-trang-nu":
                    {
                        sql_query = string.Format("select b.*" +
                            " from storemy.products p" +
                            " left join storemy.brands b on p.brand_id = b.id " +
                            " where p.maingroup_id in (1)" +
                            " group by b.id ");

                        break;
                    }
                case "thoi-trang-nam":
                    {
                        sql_query = string.Format("select b.*" +
                            " from storemy.products p" +
                            " left join storemy.brands b on p.brand_id = b.id " +
                            " where p.maingroup_id in (9)" +
                            " group by b.id ");

                        break;
                    }
                case "giay-tui-vi-nu":
                    {
                        sql_query = string.Format("select b.*" +
                            " from storemy.products p" +
                            " left join storemy.brands b on p.brand_id = b.id " +
                            " where p.maingroup_id in (3,4,5)" +
                            " group by b.id ");

                        break;
                    }
            }

            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            var data = funtion.GetAll<Brands>("storemy.excute_sql", param).data;

            return data;
        }

        public List<Products> Get_Landing_Products(string page_id)
        {
            string sql_query = string.Empty;
            switch (page_id)
            {
                case "thoi-trang-nu":
                    {
                        sql_query = string.Format("select p.*, b.logo_url" +
                            " from storemy.products p" +
                            " left join storemy.brands b on p.brand_id = b.id " +
                            " where p.maingroup_id in (1)" +
                            " order by random() limit 12");

                        break;
                    }
                case "thoi-trang-nam":
                    {
                        sql_query = string.Format("select p.*, b.logo_url" +
                            " from storemy.products p" +
                            " left join storemy.brands b on p.brand_id = b.id " +
                            " where p.maingroup_id in (9)" +
                            " order by random() limit 8");

                        break;
                    }
                case "giay-tui-vi-nu":
                    {
                        sql_query = string.Format("select p.*, b.logo_url" +
                            " from storemy.products p" +
                            " left join storemy.brands b on p.brand_id = b.id " +
                            " where p.maingroup_id in (3,4,5)" +
                            " order by random() limit 8");

                        break;
                    }
            }

            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            var data = funtion.GetAll<Products>("storemy.excute_sql", param).data;

            return data;
        }

        public List<LandingPageTags> Get_Landing_tags(string page_id)
        {
            string sql_query = string.Empty;
            switch (page_id)
            {
                case "thoi-trang-nu":
                    {
                        sql_query = string.Format("select round(cast (p.retail_price - p.sale_price as float) / p.retail_price * 100) as discount, count(*) as quantity" +
                            " from storemy.products p" +
                            " left join storemy.maingroup m on p.maingroup_id = m.id " +
                            " where m.id in (1)" +
                            " group by discount" +
                            " order by discount desc");

                        break;
                    }
                case "thoi-trang-nam":
                    {
                        sql_query = string.Format("select round(cast (p.retail_price - p.sale_price as float) / p.retail_price * 100) as discount, count(*) as quantity" +
                            " from storemy.products p" +
                            " left join storemy.maingroup m on p.maingroup_id = m.id " +
                            " where m.id in (9)" +
                            " group by discount" +
                            " order by discount desc");

                        break;
                    }
                case "giay-tui-vi-nu":
                    {
                        sql_query = string.Format("select round(cast (p.retail_price - p.sale_price as float) / p.retail_price * 100) as discount, count(*) as quantity" +
                            " from storemy.products p" +
                            " left join storemy.maingroup m on p.maingroup_id = m.id " +
                            " where m.id in (3,4,5)" +
                            " group by discount" +
                            " order by discount desc");

                        break;
                    }
            }

            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            var result = funtion.GetAll<LandingPageTags>("storemy.excute_sql", param);

            return result.data;
        }

        public List<Products> Get_Landing_Products(string page_id, int? discount, int limit, int offset)
        {
            string discount_query = discount != null ? $"and round(cast (p.retail_price - p.sale_price as float) / p.retail_price * 100) = {discount}" : "";
            string sql_query = string.Empty;
            switch (page_id)
            {
                case "thoi-trang-nu":
                    {
                        sql_query = string.Format("select p.*, b.logo_url " +
                            " from storemy.products p" +
                            " left join storemy.maingroup m on p.maingroup_id = m.id" +
                            " left join storemy.brands b on p.brand_id = b.id" +
                            " where m.id in (1) {0}" +
                            " limit {1} offset {2}", discount_query, limit, offset);

                        break;
                    }
                case "thoi-trang-nam":
                    {
                        sql_query = string.Format("select p.*, b.logo_url " +
                            " from storemy.products p" +
                            " left join storemy.maingroup m on p.maingroup_id = m.id" +
                            " left join storemy.brands b on p.brand_id = b.id" +
                            " where m.id in (9) {0}" +
                            " limit {1} offset {2}", discount_query, limit, offset);

                        break;
                    }
                case "giay-tui-vi-nu":
                    {
                        sql_query = string.Format("select p.*, b.logo_url " +
                            " from storemy.products p" +
                            " left join storemy.maingroup m on p.maingroup_id = m.id" +
                            " left join storemy.brands b on p.brand_id = b.id" +
                            " where m.id in (3,4,5) {0}" +
                            " limit {1} offset {2}", discount_query, limit, offset);

                        break;
                    }
            }

            Funtion funtion = new Funtion();

            var param = new Dictionary<string, object> {
                { "p_sql", sql_query }
            };

            var result = funtion.GetAll<Products>("storemy.excute_sql", param);

            return result.data;
        }

        public List<LadingPageView> LandingPageForHome()
        {
            List<LadingPageView> ladingPageViews = new List<LadingPageView> {
                new LadingPageView {
                    page_id = "thoi-trang-nu",
                    name = "THỜI TRANG NỮ",
                    brands = Get_Landing_Brands("thoi-trang-nu"),
                    products = Get_Landing_Products("thoi-trang-nu"),
                    link = "/collections/thoi-trang-nu"
                },
                new LadingPageView
                {
                    page_id = "thoi-trang-nam",
                    name = "THỜI TRANG NAM",
                    brands = Get_Landing_Brands("thoi-trang-nam"),
                    products = Get_Landing_Products("thoi-trang-nam"),
                    link = "/collections/thoi-trang-nam"
                },
                new LadingPageView
                {
                    page_id = "giay-tui-vi-nu",
                    name = "GIÀY & TÚI & VÍ NỮ",
                    brands = Get_Landing_Brands("giay-tui-vi-nu"),
                    products = Get_Landing_Products("giay-tui-vi-nu"),
                    link = "/collections/giay-tui-vi-nu"
                }
            };

            return ladingPageViews;
        }
    }
}
