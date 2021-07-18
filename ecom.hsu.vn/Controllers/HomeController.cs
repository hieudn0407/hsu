using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ecom.hsu.vn.Models;
using ecom.hsu.services;
using DeviceDetectorNET;
using ecom.hsu.dtos.View;
using ecom.hsu.dtos.body;

namespace ecom.hsu.vn.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var ua = DeviceDetector.GetInfoFromUserAgent(Request.Headers["User-Agent"].ToString());
            ViewBag.DeviceType = ua.Match.DeviceType;

            BannerService bannerService = new BannerService();
            ProductsService productsService = new ProductsService();
            CommonService commonService = new CommonService();

            var brands = commonService.Get_Landing_Brands("thoi-trang-nu");
            var categories = productsService.GetCategories();
            var banners = bannerService.GetBanners();

            ViewBag.SuggestForYouProducts = commonService.Get_Sale_Product();

            ViewBag.LandingPageHome = commonService.LandingPageForHome();
            ViewBag.Banners = banners;
            ViewBag.Brands = brands;
            ViewBag.Categories = categories
                .GroupBy(x => x.object_name)
                .Select(y => new Categories_View { name = y.Key, data = y.ToList() })
                .ToList();

            ViewBag.SearchPlaceholder = "Tìm kiếm trên XXX";
            ViewBag.BackLink = null;

            return View();
        }

        [HttpPost]
        public ActionResult GetSaleProduct()
        {
            CommonService commonService = new CommonService();
            var saleProduct = commonService.Get_Landing_Products("thoi-trang-nu");

            return Json(new { status = true, data = saleProduct });
        }

        [Route("tim-kiem")]
        public ActionResult Search(string keyword)
        {
            var ua = DeviceDetector.GetInfoFromUserAgent(Request.Headers["User-Agent"].ToString());
            ViewBag.DeviceType = ua.Match.DeviceType;

            ProductsService productsService = new ProductsService();
            ViewBag.Categories = productsService.GetCategories()
                .GroupBy(x => x.object_name)
                .Select(y => new Categories_View { name = y.Key, data = y.ToList() })
                .ToList();

            ViewBag.SearchPlaceholder = keyword;
            ViewBag.BackLink = "/";
            return View();
        }

        [HttpPost]
        [Route("tim-kiem")]
        public ActionResult Search([FromBody]Search_bd rq, string keyword)
        {
            CommonService commonService = new CommonService();

            var offset = rq.limit * (rq.page - 1);

            var data = commonService.Seach(keyword, rq.limit, offset);

            return Json(new { status = true, data });
        }

        [HttpPost]
        public ActionResult Search_by_keyword([FromBody]Search_bd rq)
        {
            CommonService commonService = new CommonService();

            var data = commonService.Seach(rq.keyword, 30, 0).GroupBy(x => x.name).Select(y => new { name = y.Key, data = y.ToList() }).Take(10).ToList();

            return Json(new { status = true, data });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
