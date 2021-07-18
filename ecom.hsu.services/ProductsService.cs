using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using ecom.hsu.dtos.View;
using ecom.hsu.database;

namespace ecom.hsu.services
{
    public class ProductsService
    {
        public List<Brands> GetBrands()
        {
            Funtion funtion = new Funtion();
            return funtion.GetAll<Brands>("storemy.get_brands").data;
        }

        public List<Categories> GetCategories()
        {
            Funtion funtion = new Funtion();
            var result = funtion.GetAll<Categories>("storemy.get_category_home");

            if (!result.status)
            {
                WriteToFile($"\r\n{DateTime.Now} : {result.exception.Message}");
            }

            return result.data;
        }

        public Exception test()
        {
            Funtion funtion = new Funtion();
            var result = funtion.GetAll<Categories>("storemy.get_category_home");

            return result.exception;
        }

        public List<Products> GetProducts(int Brand_id, int Maingroup_id, int Subgroup_id, string Filter)
        {
            Funtion funtion = new Funtion();
            var param = new Dictionary<string, object> {
                { "p_brand_id", Brand_id },
                { "p_maingroup_id", Maingroup_id },
                { "p_subgroup_id", Subgroup_id },
                { "p_filter", Filter }
            };

            return funtion.GetAll<Products>("storemy.get_products", param).data;
        }

        public List<ObjectGroups> GetObjectGroupList()
        {
            Funtion funtion = new Funtion();
            return funtion.GetAll<ObjectGroups>("storemy.get_objectgroups").data;
        }

        public List<MainGroup> GetMaingroupByObjectid(int objectid)
        {
            Funtion funtion = new Funtion();
            var param = new Dictionary<string, object> {
                { "p_objectid", objectid }
            };
            return funtion.GetAll<MainGroup>("storemy.get_maingroups_by_objectid", param).data;
        }

        public List<MainGroup> GetSubgroupBymaingroupid(int maingroupid)
        {
            Funtion funtion = new Funtion();
            var param = new Dictionary<string, object> {
                { "p_maingroupid", maingroupid }
            };
            return funtion.GetAll<MainGroup>("storemy.get_subgroups_by_maingroupid", param).data;
        }

        public ResponseStatus UpsertDataProducts(Products product, int brand_id, int objectgroup_id, int maingroup_id, int subgroup_id)
        {
            Funtion funtion = new Funtion();
            var param = new Dictionary<string, object> {
                { "p_brand_id", brand_id },
                { "p_object_id", objectgroup_id },
                { "p_maingroup_id", maingroup_id },
                { "p_subgroup_id", subgroup_id == 0 ? (int?)null : subgroup_id },
                { "p_ecom_brand_id", product.link },
                { "p_name", product.name },
                { "p_retail_price", product.retail_price },
                { "p_sale_price", product.sale_price },
                { "p_image_url", product.image_url },
                { "p_link", null }
            };
            var result = funtion.GetAll<ResponseStatus>("storemy.insert_data_products", param);

            if (result.status)
            {
                return result.data.First();
            }
            else
            {
                return null;
            }
        }

        private void WriteToFile(string Message)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\Log_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
                if (!File.Exists(filepath))
                {
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(Message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
