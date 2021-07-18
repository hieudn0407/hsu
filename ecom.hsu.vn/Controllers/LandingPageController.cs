using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceDetectorNET;
using ecom.hsu.dtos.body;
using ecom.hsu.services;
using Microsoft.AspNetCore.Mvc;

namespace ecom.hsu.vn.Controllers
{
    public class LandingPageController : Controller
    {
        [Route("collections/{page_id}")]
        public IActionResult Index(string page_id)
        {
            var ua = DeviceDetector.GetInfoFromUserAgent(Request.Headers["User-Agent"].ToString());
            ViewBag.DeviceType = ua.Match.DeviceType;

            CommonService commonService = new CommonService();

            ViewBag.Brands = commonService.Get_Landing_Brands(page_id);
            ViewBag.Tags = commonService.Get_Landing_tags(page_id);

            ViewBag.SearchPlaceholder = "Tìm kiếm trên XXX";
            ViewBag.BackLink = "/";

            return View();
        }

        [HttpPost]
        [Route("collections/{page_id}")]
        public IActionResult Index(string page_id, [FromBody]Search_bd rq)
        {
            CommonService commonService = new CommonService();

            var offset = rq.limit * (rq.page - 1);

            var data = commonService.Get_Landing_Products(page_id, rq.discount, rq.limit, offset);

            return Json(new { status = true, data });
        }
    }
}