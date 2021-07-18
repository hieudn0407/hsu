using System.Linq;
using DeviceDetectorNET;
using ecom.hsu.dtos.body;
using ecom.hsu.dtos.Filter;
using ecom.hsu.dtos.View;
using ecom.hsu.services;
using Microsoft.AspNetCore.Mvc;

namespace ecom.hsu.vn.Controllers
{
    public class BrandController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("thuong-hieu/{brand_id}")]
        public ActionResult Home(string brand_id)
        {
            var ua = DeviceDetector.GetInfoFromUserAgent(Request.Headers["User-Agent"].ToString());
            ViewBag.DeviceType = ua.Match.DeviceType;

            CommonService commonService = new CommonService();

            var brand = commonService.Get_Brand_Categories(brand_id);

            ViewBag.Brand = brand.First();

            ViewBag.Categories = brand
                .GroupBy(x => x.object_name)
                .Select(y => new Categories_View { name = y.Key, data = y.ToList() })
                .ToList();

            ViewBag.SearchPlaceholder = $"{brand_id}";
            ViewBag.BackLink = "/";

            return View();
        }

        [HttpPost]
        [Route("thuong-hieu/{brand_id}")]
        public ActionResult Home(string brand_id, [FromBody]Search_bd rq)
        {
            CommonService commonService = new CommonService();

            var offset = rq.limit * (rq.page - 1);

            var brand_product = commonService.Get_Product_Brand(brand_id, rq.limit, offset, rq.filter, rq.filter_detail);

            return Json(new { status = true, data = brand_product });
        }

        [Route("thuong-hieu/{brand_id}/{object_id}/{maingroup_id}")]
        public ActionResult Brand_Subgroup(string brand_id, string object_id, string maingroup_id)
        {
            var ua = DeviceDetector.GetInfoFromUserAgent(Request.Headers["User-Agent"].ToString());
            ViewBag.DeviceType = ua.Match.DeviceType;

            CommonService commonService = new CommonService();

            var sub_cate = commonService.Get_Brand_Subgroup(brand_id, object_id, maingroup_id);

            if (sub_cate == null || sub_cate.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var brand = commonService.Get_Brand_Categories(brand_id).First();

            ViewBag.Brand = brand;
            ViewBag.Subgroup = sub_cate;
            ViewBag.SearchPlaceholder = $"{brand_id} - {brand.object_name} - {brand.maingroup_name}";
            ViewBag.BackLink = $"/thuong-hieu/{brand_id}";

            return View();
        }

        [HttpPost]
        [Route("thuong-hieu/{brand_id}/{object_id}/{maingroup_id}")]
        public ActionResult Brand_Subgroup(string brand_id, string object_id, string maingroup_id, [FromBody]Search_bd rq)
        {
            CommonService commonService = new CommonService();

            var offset = rq.limit * (rq.page - 1);

            var data = commonService.Get_Product_Brand_Object_Maingroup(brand_id, object_id, maingroup_id, rq.limit, offset, rq.filter, rq.filter_detail);

            return Json(new { status = true, data });
        }

        [Route("thuong-hieu/{brand_id}/{object_id}/{maingroup_id}/{subgroup_id}")]
        public ActionResult Detail(string brand_id, string object_id, string maingroup_id, string subgroup_id)
        {
            var ua = DeviceDetector.GetInfoFromUserAgent(Request.Headers["User-Agent"].ToString());
            ViewBag.DeviceType = ua.Match.DeviceType;

            CommonService commonService = new CommonService();

            var sub_cate = commonService.Get_Brand_Subgroup(brand_id, object_id, maingroup_id).Where(x => x.subgroup_name_id == subgroup_id).ToList();

            if (sub_cate == null || sub_cate.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var brand = commonService.Get_Brand_Categories(brand_id).First();

            ViewBag.Brand = brand;
            ViewBag.SearchPlaceholder = $"{brand_id} - {sub_cate.First().object_name} - {sub_cate.First().maingroup_name} - {sub_cate.First().subgroup_name}";
            ViewBag.BackLink = $"/thuong-hieu/{brand_id}/{sub_cate.First().object_name_id}/{sub_cate.First().maingroup_name_id}";

            return View();
        }

        [HttpPost]
        [Route("thuong-hieu/{brand_id}/{object_id}/{maingroup_id}/{subgroup_id}")]
        public ActionResult Detail(string brand_id, string object_id, string maingroup_id, string subgroup_id, [FromBody]Search_bd rq)
        {
            CommonService commonService = new CommonService();

            var offset = rq.limit * (rq.page - 1);

            var data = commonService.Get_Product_Brand_Object_Maingroup_Subgroup(brand_id, object_id, maingroup_id, subgroup_id, rq.limit, offset, rq.filter, rq.filter_detail);

            return Json(new { status = true, data });
        }
    }
}