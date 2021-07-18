using ecom.hsu.database;
using ecom.hsu.dtos.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace ecom.hsu.services
{
    public class BannerService
    {
        public List<Banners> GetBanners()
        {
            Funtion funtion = new Funtion();

            return funtion.GetAll<Banners>("storemy.get_banners").data;
        }
    }
}
