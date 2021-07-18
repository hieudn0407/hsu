using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using ecom.hsu.database;
using ecom.hsu.dtos.View;
using System.Linq;
using ecom.hsu.dtos.Brands;

namespace ecom.hsu.services
{
    public class CrawlService
    {

    }

    public class Crawl_Vascara
    {
        List<Products> lst = new List<Products>();

        private readonly string cate_giay = "4779";
        private readonly int cate_page_giay = 70;

        private readonly string cate_tui = "4780";
        private readonly int cate_page_tui = 50;

        private readonly string cate_vi = "4925";
        private readonly int cate_page_vi = 15;

        public List<string> ReadItemsVascaraForTuiXach(string cate, int page)
        {
            List<string> lst = new List<string>();
            for (int j = 1; j < page; j++)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.vascara.com/product/filterproduct?page={j}&cate={cate}&viewmore=1&viewcol=3");
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "GET";
                request.ContentType = "application/json";

                using (var response = request.GetResponse())
                {
                    var result = response.GetResponseStream();
                    var streamreader = new StreamReader(result);
                    var data = JsonConvert.DeserializeObject<VascaraTuiXach>(streamreader.ReadToEnd());

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(data.html);

                    if (doc.DocumentNode.SelectNodes("//div") != null)
                    {
                        var items = doc.DocumentNode.SelectNodes("//div")
                        .Select(p => p.InnerHtml)
                        .ToList();


                        int temp = 1;
                        string html = string.Empty;
                        foreach (var item in items)
                        {
                            if (temp == 4) temp = 1;
                            if (temp == 1) html = item;
                            if (temp == 3) lst.Add(html);
                            temp++;
                        }
                    }
                }
            }

            return lst;
        }

        public List<Products> ProcessHtmlsForTuiXach(List<string> htmls)
        {
            foreach (var html in htmls)
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                var href = doc.DocumentNode.SelectNodes("//div//div//a[@href]").First().Attributes["href"].Value;
                var img = doc.DocumentNode.SelectNodes("//div//div//a//img[@src]").First().Attributes["src"].Value;
                var sale_price = long.Parse(doc.DocumentNode.SelectNodes("//div//span//ins//span").First().InnerText.Replace(".", ""));
                var retail_price = doc.DocumentNode.SelectNodes("//div//span//del//span") == null
                    ? sale_price
                    : long.Parse(doc.DocumentNode.SelectNodes("//div//span//del//span").First().InnerText.Replace(".", ""));
                var name = doc.DocumentNode.SelectNodes("//div//h5//a").First().InnerText.TrimStart().TrimEnd();

                lst.Add(new Products
                {
                    link = href,
                    name = name,
                    sale_price = sale_price,
                    retail_price = retail_price,
                    image_url = img
                });
            }

            return lst;
        }

        public void Process()
        {
            ProductsService productsService = new ProductsService();
            CommonService commonService = new CommonService();

            try
            {
                #region thêm hoặc cập nhật sản phẩm giày của vascara

                var items_giay = ReadItemsVascaraForTuiXach(cate_giay, cate_page_giay);
                var data_giay = ProcessHtmlsForTuiXach(items_giay);
                foreach (var item in data_giay)
                {
                    productsService.UpsertDataProducts(item, 2, 1, 5, 0);
                }

                commonService.Update_Table_Product("update storemy.products set subgroup_id = 9 where ecom_brand_id ilike '%/giay-sneaker/%' and maingroup_id = 5 and objectid = 1 and brand_id = 2");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 10 where ecom_brand_id ilike '%/giay-bup-be/%' and maingroup_id = 5 and objectid = 1 and brand_id = 2");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 11 where ecom_brand_id ilike '%/dep-guoc/%' and maingroup_id = 5 and objectid = 1 and brand_id = 2");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 12 where ecom_brand_id ilike '%/giay-bit/%' and maingroup_id = 5 and objectid = 1 and brand_id = 2");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 13 where ecom_brand_id ilike '%/giay-cao-got/%' and maingroup_id = 5 and objectid = 1 and brand_id = 2");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 14 where ecom_brand_id ilike '%/giay-sandals/%' and maingroup_id = 5 and objectid = 1 and brand_id = 2");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 15 where ecom_brand_id ilike '%/giay-luoi/%' and maingroup_id = 5 and objectid = 1 and brand_id = 2");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 16 where ecom_brand_id ilike '%/giay-boot/%' and maingroup_id = 5 and objectid = 1 and brand_id = 2");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 17 where ecom_brand_id ilike '%/giay-da-that/%' and maingroup_id = 5 and objectid = 1 and brand_id = 2");

                #endregion

                #region thêm hoặc cập nhật sản phẩm túi của vascara

                var items_tui = ReadItemsVascaraForTuiXach(cate_tui, cate_page_tui);
                var data_tui = ProcessHtmlsForTuiXach(items_tui);
                foreach (var item in data_tui)
                {
                    productsService.UpsertDataProducts(item, 2, 1, 3, 0);
                }

                commonService.Update_Table_Product("update storemy.products set subgroup_id = 2 where ecom_brand_id ilike '%/tui-xach-tay/%' and maingroup_id = 3 and objectid = 1 and brand_id = 2");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 3 where ecom_brand_id ilike '%/tui-deo-cheo/%' and maingroup_id = 3 and objectid = 1 and brand_id = 2");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 4 where ecom_brand_id ilike '%/tui-xach-da-that/%' and maingroup_id = 3 and objectid = 1 and brand_id = 2");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 5 where ecom_brand_id ilike '%/balo/%' and maingroup_id = 3 and objectid = 1 and brand_id = 2");

                #endregion

                #region thêm hoặc cập nhật sản phẩm túi của vascara

                var items_vi = ReadItemsVascaraForTuiXach(cate_vi, cate_page_vi);
                var data_vi = ProcessHtmlsForTuiXach(items_vi);
                foreach (var item in data_vi)
                {
                    productsService.UpsertDataProducts(item, 2, 1, 4, 0);
                }

                commonService.Update_Table_Product("update storemy.products set subgroup_id = 6 where ecom_brand_id ilike '%/vi-cam-tay/%' and maingroup_id = 4 and objectid = 1 and brand_id = 2");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 7 where ecom_brand_id ilike '%/vi-du-tiec/%' and maingroup_id = 4 and objectid = 1 and brand_id = 2");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 8 where ecom_brand_id ilike '%/vi-da-that/%' and maingroup_id = 4 and objectid = 1 and brand_id = 2");

                #endregion
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class Crawl_SevenAM
    {
        List<Products> lst = new List<Products>();

        private readonly string cate_damnu = "dam-nu";
        private readonly int cate_page_damnu = 15;

        private readonly string cate_ao = "ao-nu";
        private readonly int cate_page_ao = 4;

        private readonly string cate_quanaonu = "quan-ao-nu";
        private readonly int cate_page_quanaonu = 2;

        private readonly string cate_chanvay = "juyp-a";
        private readonly int cate_page_chanvay = 1;

        private readonly string cate_bolien = "bo-lien";
        private readonly int cate_page_bolien = 1;

        public List<Products> ProcessHtmlsForAll(string cate, int page)
        {
            try
            {
                for (int i = 1; i <= page; i++)
                {
                    HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                    HtmlAgilityPack.HtmlDocument doc = web.Load($"https://sevenam.vn/collections/{cate}?page={i}");
                    var items = doc.DocumentNode.SelectNodes("//*[@class=\"product-item\"]");

                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            HtmlAgilityPack.HtmlDocument doc1 = new HtmlAgilityPack.HtmlDocument();
                            doc1.LoadHtml(item.InnerHtml);

                            var href = $"https://sevenam.vn{doc1.DocumentNode.SelectNodes("//div[1]//div[1]//div[1]//a[@href]").First().Attributes["href"].Value}";
                            var img = doc1.DocumentNode.SelectNodes("//div[1]//div[1]//div[1]//a//img[@src]").First().Attributes["src"].Value.Replace("//", "https://");
                            var sale_price = long.Parse(doc1.DocumentNode.SelectNodes("//div[1]//div[3]//span").First().InnerText.TrimStart().TrimEnd().Replace(",", "").Replace("₫", ""));
                            var retail_price = doc1.DocumentNode.SelectNodes("//div[1]//div[3]//span[2]//s") == null
                                ? sale_price
                                : long.Parse(doc1.DocumentNode.SelectNodes("//div[1]//div[3]//span[2]//s").First().InnerText.Replace(",", "").Replace("₫", ""));
                            var name = doc1.DocumentNode.SelectNodes("//div//div[2]//h3//a").First().InnerText.TrimStart().TrimEnd();

                            lst.Add(new Products
                            {
                                link = href,
                                name = name,
                                sale_price = sale_price,
                                retail_price = retail_price,
                                image_url = img
                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return lst;

        }

        public void Process()
        {
            ProductsService productsService = new ProductsService();
            CommonService commonService = new CommonService();

            try
            {
                #region thêm hoặc cập nhật sản phẩm đầm của Seven.AM

                var items_giay = ProcessHtmlsForAll(cate_damnu, cate_page_damnu);
                foreach (var item in items_giay)
                {
                    productsService.UpsertDataProducts(item, 3, 1, 1, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm áo của Seven.AM

                var items_ao = ProcessHtmlsForAll(cate_ao, cate_page_ao);
                foreach (var item in items_ao)
                {
                    productsService.UpsertDataProducts(item, 3, 1, 1, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm quần của Seven.AM

                var items_quan = ProcessHtmlsForAll(cate_quanaonu, cate_page_quanaonu);
                foreach (var item in items_quan)
                {
                    productsService.UpsertDataProducts(item, 3, 1, 1, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm chân váy của Seven.AM

                var items_chanvay = ProcessHtmlsForAll(cate_chanvay, cate_page_chanvay);
                foreach (var item in items_chanvay)
                {
                    productsService.UpsertDataProducts(item, 3, 1, 1, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm bộ liền của Seven.AM

                var items_bolien = ProcessHtmlsForAll(cate_bolien, cate_page_bolien);
                foreach (var item in items_bolien)
                {
                    productsService.UpsertDataProducts(item, 3, 1, 1, 0);
                }

                #endregion

                commonService.Update_Table_Product("update storemy.products set subgroup_id = 18 where ecom_brand_id ilike '%/ao-nu/%' and maingroup_id = 1 and objectid = 1 and brand_id = 3");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 19 where ecom_brand_id ilike '%/quan-ao-nu/%' and maingroup_id = 1 and objectid = 1 and brand_id = 3");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 20 where ecom_brand_id ilike '%/dam-nu/%' and maingroup_id = 1 and objectid = 1 and brand_id = 3");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 21 where ecom_brand_id ilike '%/juyp-a/%' and maingroup_id = 1 and objectid = 1 and brand_id = 3");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 22 where ecom_brand_id ilike '%/bo-lien/%' and maingroup_id = 1 and objectid = 1 and brand_id = 3");
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class Crawl_ShopCeleb
    {
        List<Products> lst = new List<Products>();

        private readonly string cate_damnu = "https://shop.celeb.vn/collections/dam-nu";

        private readonly string cate_aothunnam = "1001994061";
        private readonly int cate_page_aothunnam = 3;

        private readonly string cate_aosominam = "1001994060";
        private readonly int cate_page_aosominam = 2;

        private readonly string cate_aokhoacnam = "https://shop.celeb.vn/collections/ao-khoac-nam";

        private readonly string cate_quantaynam = "https://shop.celeb.vn/collections/quan-tay-nam";

        private readonly string cate_quanjeannam = "https://shop.celeb.vn/collections/quan-jean-nam";

        private readonly string cate_quankakinam = "https://shop.celeb.vn/collections/quan-kaki-nam";

        private readonly string cate_aovestnam = "https://shop.celeb.vn/collections/ao-vest-nam";

        private readonly string cate_quanlotnam = "https://shop.celeb.vn/collections/quan-lot-nam";

        public List<Products> ProcessHtmlsForAll(string cate, int page)
        {
            lst = new List<Products>();
            try
            {
                for (int i = 1; i <= page; i++)
                {
                    HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                    HtmlAgilityPack.HtmlDocument doc = web.Load($"https://shop.celeb.vn/search?q=filter%3D(collectionid%3Aproduct%3D{cate})%26%26((price%3Aproduct%3E0)%26%26(price%3Aproduct%3C%3D2000000))&sortby=(updated_at%3Aproduct%3Ddesc)&view=filter&page={i}");
                    var items = doc.DocumentNode.SelectNodes("//*[@class=\"product-block product-resize\"]");

                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            HtmlAgilityPack.HtmlDocument doc1 = new HtmlAgilityPack.HtmlDocument();
                            doc1.LoadHtml(item.InnerHtml);

                            var href = $"https://shop.celeb.vn{doc1.DocumentNode.SelectNodes("//div[1]//a[@href]").First().Attributes["href"].Value}";
                            var img = doc1.DocumentNode.SelectNodes("//div[1]//a//picture//img[@src]").First().Attributes["src"].Value.Replace("//", "https://");
                            var sale_price = long.Parse(doc1.DocumentNode.SelectNodes("//div[2]//div//div[1]//p").First().InnerText.TrimStart().TrimEnd().Replace(",", "").Replace("₫", ""));
                            //var retail_price = doc1.DocumentNode.SelectNodes("//div[1]//div[3]//span[2]//s") == null
                            //    ? sale_price
                            //    : long.Parse(doc1.DocumentNode.SelectNodes("//div[1]//div[3]//span[2]//s").First().InnerText.Replace(",", "").Replace("₫", ""));
                            var name = doc1.DocumentNode.SelectNodes("//div[2]//div//h3//a").First().InnerText.TrimStart().TrimEnd();

                            lst.Add(new Products
                            {
                                link = href,
                                name = name,
                                sale_price = sale_price,
                                retail_price = sale_price,
                                image_url = img
                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return lst;

        }

        public List<Products> ProcessHtmlsForAll(string cate)
        {
            lst = new List<Products>();

            try
            {
                HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load($"{cate}");
                var items = doc.DocumentNode.SelectNodes("//*[@class=\"product-block product-resize\"]");

                if (items != null)
                {
                    foreach (var item in items)
                    {
                        HtmlAgilityPack.HtmlDocument doc1 = new HtmlAgilityPack.HtmlDocument();
                        doc1.LoadHtml(item.InnerHtml);

                        var href = $"https://shop.celeb.vn{doc1.DocumentNode.SelectNodes("//div[1]//a[@href]").First().Attributes["href"].Value}";
                        var img = doc1.DocumentNode.SelectNodes("//div[1]//a//picture//img[@src]").First().Attributes["src"].Value.Replace("//", "https://");
                        var sale_price = long.Parse(doc1.DocumentNode.SelectNodes("//div[2]//div//div[1]//p").First().InnerText.TrimStart().TrimEnd().Replace(",", "").Replace("₫", ""));
                        //var retail_price = doc1.DocumentNode.SelectNodes("//div[1]//div[3]//span[2]//s") == null
                        //    ? sale_price
                        //    : long.Parse(doc1.DocumentNode.SelectNodes("//div[1]//div[3]//span[2]//s").First().InnerText.Replace(",", "").Replace("₫", ""));
                        var name = doc1.DocumentNode.SelectNodes("//div[2]//div//h3//a").First().InnerText.TrimStart().TrimEnd();

                        lst.Add(new Products
                        {
                            link = href,
                            name = name,
                            sale_price = sale_price,
                            retail_price = sale_price,
                            image_url = img
                        });
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return lst;

        }

        public void Process()
        {
            ProductsService productsService = new ProductsService();
            CommonService commonService = new CommonService();

            try
            {
                #region thêm hoặc cập nhật sản phẩm đầm của Shop.Celeb

                var items_damnu = ProcessHtmlsForAll(cate_damnu);
                foreach (var item in items_damnu)
                {
                    productsService.UpsertDataProducts(item, 5, 1, 1, 25);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm áo thun nam của Shop.Celeb

                var items_aothunnam = ProcessHtmlsForAll(cate_aothunnam, cate_page_aothunnam);
                foreach (var item in items_aothunnam)
                {
                    productsService.UpsertDataProducts(item, 5, 2, 9, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm áo sơ mi nam của Shop.Celeb

                var items_aosominam = ProcessHtmlsForAll(cate_aosominam, cate_page_aosominam);
                foreach (var item in items_aosominam)
                {
                    productsService.UpsertDataProducts(item, 5, 2, 9, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm áo khoác nam của Shop.Celeb

                var items_aokhoacnam = ProcessHtmlsForAll(cate_aokhoacnam);
                foreach (var item in items_aokhoacnam)
                {
                    productsService.UpsertDataProducts(item, 5, 2, 9, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm áo vest nam của Shop.Celeb

                var items_aovestnam = ProcessHtmlsForAll(cate_aovestnam);
                foreach (var item in items_aokhoacnam)
                {
                    productsService.UpsertDataProducts(item, 5, 2, 9, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm quần tây nam của Shop.Celeb

                var items_quantaynam = ProcessHtmlsForAll(cate_quantaynam);
                foreach (var item in items_quantaynam)
                {
                    productsService.UpsertDataProducts(item, 5, 2, 9, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm quần jean nam của Shop.Celeb

                var items_quanjeannam = ProcessHtmlsForAll(cate_quanjeannam);
                foreach (var item in items_quanjeannam)
                {
                    productsService.UpsertDataProducts(item, 5, 2, 9, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm quần kaki nam của Shop.Celeb

                var items_quankakinam = ProcessHtmlsForAll(cate_quankakinam);
                foreach (var item in items_quankakinam)
                {
                    productsService.UpsertDataProducts(item, 5, 2, 9, 0);
                }

                #endregion

                commonService.Update_Table_Product("update storemy.products set subgroup_id = 23 where ecom_brand_id ilike '%ao-thun%' and maingroup_id = 9 and objectid = 2 and brand_id = 5");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 24 where ecom_brand_id ilike '%ao-khoac%' and maingroup_id = 9 and objectid = 2 and brand_id = 5");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 25 where ecom_brand_id ilike '%so-mi%' and maingroup_id = 9 and objectid = 2 and brand_id = 5");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 26 where ecom_brand_id ilike '%polo%' and maingroup_id = 9 and objectid = 2 and brand_id = 5");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 29 where ecom_brand_id ilike '%quan-tay%' and maingroup_id = 9 and objectid = 2 and brand_id = 5");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 30 where ecom_brand_id ilike '%quan-jean%' and maingroup_id = 9 and objectid = 2 and brand_id = 5");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 31 where ecom_brand_id ilike '%quan-kaki%' and maingroup_id = 9 and objectid = 2 and brand_id = 5");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 32 where ecom_brand_id ilike '%quan-short%' and maingroup_id = 9 and objectid = 2 and brand_id = 5");
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class Crawl_MaysHouse
    {
        List<Products> lst = new List<Products>();

        private readonly string cate_aosomi = "ao-so-mi";
        private readonly int cate_page_aosomi = 11;

        private readonly string cate_aokhoackieu = "ao-khoac-kieu";
        private readonly int cate_page_aokhoackieu = 2;

        private readonly string cate_chanvay = "chan-vay";
        private readonly int cate_page_chanvay = 5;

        private readonly string cate_aothun = "ao-thun";
        private readonly int cate_page_aothun = 2;

        private readonly string cate_aolen = "ao-len";
        private readonly int cate_page_aolen = 1;

        private readonly string cate_quan = "quan";
        private readonly int cate_page_quan = 5;

        private readonly string cate_setbo = "set-trang-phuc-jumpsuit";
        private readonly int cate_page_setbo = 8;

        private readonly string cate_aovest = "ao-khoac-ao-vest";
        private readonly int cate_page_aovest = 1;

        private readonly string cate_vaydam = "vay-dam";
        private readonly int cate_page_vaydam = 1;

        private readonly string cate_aodai = "ao-dai";
        private readonly int cate_page_aodai = 7;

        private List<Products> ProcessHtmlsForAll(string cate, int page)
        {
            lst = new List<Products>();
            try
            {
                for (int i = 1; i <= page; i++)
                {
                    HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                    HtmlAgilityPack.HtmlDocument doc = web.Load($"https://mayshouse.vn/collections/{cate}?page={i}");
                    var items = doc.DocumentNode.SelectNodes("//*[@class=\"product-item\"]");

                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            try
                            {
                                HtmlAgilityPack.HtmlDocument doc1 = new HtmlAgilityPack.HtmlDocument();
                                doc1.LoadHtml(item.InnerHtml);

                                var href = $"https://mayshouse.vn{doc1.DocumentNode.SelectNodes("//div[1]//a[@href]").First().Attributes["href"].Value}";
                                var img = doc1.DocumentNode.SelectNodes("//div[1]//a//img[@src]").First().Attributes["src"].Value.Replace("//", "https://");
                                var sale_price = long.Parse(doc1.DocumentNode.SelectNodes("//div[2]//div[2]//span").First().InnerText.TrimStart().TrimEnd().Replace(",", "").Replace("₫", ""));
                                var retail_price = doc1.DocumentNode.SelectNodes("//div[2]//div[2]//span[2]") == null
                                    ? sale_price
                                    : long.Parse(doc1.DocumentNode.SelectNodes("//div[2]//div[2]//span[2]").First().InnerText.Replace(",", "").Replace("₫", ""));
                                var name = doc1.DocumentNode.SelectNodes("//div[2]//div[1]//div[1]//a").First().InnerText.TrimStart().TrimEnd();

                                lst.Add(new Products
                                {
                                    link = href,
                                    name = name,
                                    sale_price = sale_price,
                                    retail_price = retail_price,
                                    image_url = img
                                });
                            }
                            catch
                            {

                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return lst;

        }

        private List<Products> ProcessHtmlsForAll(string cate)
        {
            lst = new List<Products>();

            try
            {
                HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load($"{cate}");
                var items = doc.DocumentNode.SelectNodes("//*[@class=\"product-item\"]");

                if (items != null)
                {
                    foreach (var item in items)
                    {
                        try
                        {
                            HtmlAgilityPack.HtmlDocument doc1 = new HtmlAgilityPack.HtmlDocument();
                            doc1.LoadHtml(item.InnerHtml);

                            var href = $"https://shop.celeb.vn{doc1.DocumentNode.SelectNodes("//div[1]//a[@href]").First().Attributes["href"].Value}";
                            var img = doc1.DocumentNode.SelectNodes("//div[1]//a//img[@src]").First().Attributes["src"].Value.Replace("//", "https://");
                            var sale_price = long.Parse(doc1.DocumentNode.SelectNodes("//div[2]//div[2]//span").First().InnerText.TrimStart().TrimEnd().Replace(",", "").Replace("₫", ""));
                            var retail_price = doc1.DocumentNode.SelectNodes("//div[2]//div[2]//span[2]") == null
                                ? sale_price
                                : long.Parse(doc1.DocumentNode.SelectNodes("//div[2]//div[2]//span[2]").First().InnerText.Replace(",", "").Replace("₫", ""));
                            var name = doc1.DocumentNode.SelectNodes("//div[2]//div[1]//div[1]//a").First().InnerText.TrimStart().TrimEnd();

                            lst.Add(new Products
                            {
                                link = href,
                                name = name,
                                sale_price = sale_price,
                                retail_price = retail_price,
                                image_url = img
                            });
                        }
                        catch
                        {

                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return lst;

        }

        public void Process()
        {
            ProductsService productsService = new ProductsService();
            CommonService commonService = new CommonService();

            try
            {
                #region thêm hoặc cập nhật sản phẩm áo sơ mi của MaysHouse

                var items_aosomi = ProcessHtmlsForAll(cate_aosomi, cate_page_aosomi);
                foreach (var item in items_aosomi)
                {
                    productsService.UpsertDataProducts(item, 6, 1, 1, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm áo khoác của MaysHouse

                var items_aokhoac = ProcessHtmlsForAll(cate_aokhoackieu, cate_page_aokhoackieu);
                foreach (var item in items_aokhoac)
                {
                    productsService.UpsertDataProducts(item, 6, 1, 1, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm chân váy của MaysHouse

                var items_chanvay = ProcessHtmlsForAll(cate_chanvay, cate_page_chanvay);
                foreach (var item in items_chanvay)
                {
                    productsService.UpsertDataProducts(item, 6, 1, 1, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm áo thun của MaysHouse

                var items_aothun = ProcessHtmlsForAll(cate_aothun, cate_page_aothun);
                foreach (var item in items_aothun)
                {
                    productsService.UpsertDataProducts(item, 6, 1, 1, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm áo len của MaysHouse

                var items_aolen = ProcessHtmlsForAll(cate_aolen, cate_page_aolen);
                foreach (var item in items_aolen)
                {
                    productsService.UpsertDataProducts(item, 6, 1, 1, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm quần của MaysHouse

                var items_quan = ProcessHtmlsForAll(cate_quan, cate_page_quan);
                foreach (var item in items_quan)
                {
                    productsService.UpsertDataProducts(item, 6, 1, 1, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm đồ bộ của MaysHouse

                var items_setbo = ProcessHtmlsForAll(cate_setbo, cate_page_setbo);
                foreach (var item in items_setbo)
                {
                    productsService.UpsertDataProducts(item, 6, 1, 1, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm vest của MaysHouse

                var items_vest = ProcessHtmlsForAll(cate_aovest, cate_page_aovest);
                foreach (var item in items_vest)
                {
                    productsService.UpsertDataProducts(item, 6, 1, 1, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm váy đầm của MaysHouse

                var items_vaydam = ProcessHtmlsForAll(cate_vaydam, cate_page_vaydam);
                foreach (var item in items_vaydam)
                {
                    productsService.UpsertDataProducts(item, 6, 1, 1, 0);
                }

                #endregion

                #region thêm hoặc cập nhật sản phẩm áo dài của MaysHouse

                var items_aodai = ProcessHtmlsForAll(cate_aodai, cate_page_aodai);
                foreach (var item in items_aodai)
                {
                    productsService.UpsertDataProducts(item, 6, 1, 1, 0);
                }

                #endregion

                commonService.Update_Table_Product("update storemy.products set subgroup_id = 34 where ecom_brand_id ilike '%/ao-so-mi/%' and maingroup_id = 1 and objectid = 1 and brand_id = 6");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 33 where ecom_brand_id ilike '%/ao-khoac-kieu/%' and maingroup_id = 1 and objectid = 1 and brand_id = 6;");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 33 where ecom_brand_id ilike '%/ao-khoac-ao-vest/%' and maingroup_id = 1 and objectid = 1 and brand_id = 6");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 21 where ecom_brand_id ilike '%/chan-vay/%' and maingroup_id = 1 and objectid = 1 and brand_id = 6");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 18 where ecom_brand_id ilike '%/ao-thun/%' and maingroup_id = 1 and objectid = 1 and brand_id = 6");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 35 where ecom_brand_id ilike '%/ao-len/%' and maingroup_id = 1 and objectid = 1 and brand_id = 6");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 19 where ecom_brand_id ilike '%/quan/%' and maingroup_id = 1 and objectid = 1 and brand_id = 6");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 22 where ecom_brand_id ilike '%/set-trang-phuc-jumpsuit/%' and maingroup_id = 1 and objectid = 1 and brand_id = 6");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 20 where ecom_brand_id ilike '%/vay-dam/%' and maingroup_id = 1 and objectid = 1 and brand_id = 6");
                commonService.Update_Table_Product("update storemy.products set subgroup_id = 36 where ecom_brand_id ilike '%/ao-dai/%' and maingroup_id = 1 and objectid = 1 and brand_id = 6");
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class Crawl_Kwin
    {

    }
}
