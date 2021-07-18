using System.Linq;
using DeviceDetectorNET;
using ecom.hsu.dtos.body;
using ecom.hsu.dtos.Filter;
using ecom.hsu.services;
using Microsoft.AspNetCore.Mvc;

namespace ecom.hsu.vn.Controllers
{
    public class CategoryController : Controller
    {
        [Route("danh-muc/{objectgroup_id}/{maingroup_id}")]
        public ActionResult Index(string objectgroup_id, string maingroup_id)
        {
            var ua = DeviceDetector.GetInfoFromUserAgent(Request.Headers["User-Agent"].ToString());
            ViewBag.DeviceType = ua.Match.DeviceType;

            CommonService commonService = new CommonService();

            var sub_cate = commonService.Get_Subgroup(objectgroup_id, maingroup_id);


            if (sub_cate == null || sub_cate.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.BrandsRelate = commonService.Get_List_Brand_Relate(objectgroup_id, maingroup_id);
            ViewBag.Subgroup = sub_cate;
            ViewBag.SearchPlaceholder = $"{sub_cate.First().object_name} - {sub_cate.First().maingroup_name}";
            ViewBag.BackLink = "/";

            return View();
        }

        [HttpPost]
        [Route("danh-muc/{objectgroup_id}/{maingroup_id}")]
        public ActionResult Index(string objectgroup_id, string maingroup_id, [FromBody]Search_bd rq)
        {
            CommonService commonService = new CommonService();

            var offset = rq.limit * (rq.page - 1);

            var cate_product = commonService.Get_Product_Cate(objectgroup_id, maingroup_id, rq.limit, offset, rq.filter, rq.filter_detail);

            return Json(new { status = true, data = cate_product });
        }

        [Route("danh-muc/{objectgroup_id}/{maingroup_id}/{subgroup_id}")]
        public ActionResult Detail(string objectgroup_id, string maingroup_id, string subgroup_id)
        {
            var ua = DeviceDetector.GetInfoFromUserAgent(Request.Headers["User-Agent"].ToString());
            ViewBag.DeviceType = ua.Match.DeviceType;

            CommonService commonService = new CommonService();

            var sub_cate = commonService.Get_Subgroup(objectgroup_id, maingroup_id).Where(x => x.subgroup_name_id == subgroup_id).ToList();

            if (sub_cate == null || sub_cate.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.SearchPlaceholder = $"{sub_cate.First().object_name} - {sub_cate.First().maingroup_name} - {sub_cate.First().subgroup_name}";
            ViewBag.BackLink = $"/danh-muc/{sub_cate.First().object_name_id}/{sub_cate.First().maingroup_name_id}";

            return View();
        }

        [HttpPost]
        [Route("danh-muc/{objectgroup_id}/{maingroup_id}/{subgroup_id}")]
        public ActionResult Detail(string objectgroup_id, string maingroup_id, string subgroup_id, [FromBody]Search_bd rq)
        {
            CommonService commonService = new CommonService();

            var offset = rq.limit * (rq.page - 1);

            var cate_product_detail = commonService.Get_Product_Cate_Detail(objectgroup_id, maingroup_id, subgroup_id, rq.limit, offset, rq.filter, rq.filter_detail);

            return Json(new { status = true, data = cate_product_detail });
        }
    }
}