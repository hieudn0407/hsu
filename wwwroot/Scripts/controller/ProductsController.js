"use strict";

var app = angular.module("StoreM", []).run();
var price = 'up';

var deviceType = () => {
    const ua = navigator.userAgent;
    if (/(tablet|ipad|playbook|silk)|(android(?!.*mobi))/i.test(ua)) {
        return "tablet";
    }
    else if (/Mobile|Android|iP(hone|od)|IEMobile|BlackBerry|Kindle|Silk-Accelerated|(hpw|web)OS|Opera M(obi|ini)/.test(ua)) {
        return "mobile";
    }
    return "desktop";
};

var reload_aff = () => {
    //var __atsmarttag = {
    //    pub_id: '5388638995624127439',
    //    new_tab: 1
    //};
    (function () {
        var tags = document.getElementsByTagName("a");
        for (var e = 0; e < tags.length; e++) {
            if (tags[e].id != "scrollUp") {
                var new_element = tags[e].cloneNode(true);
                tags[e].parentNode.replaceChild(new_element, tags[e]);
            }
        }
        var script = document.createElement('script');
        script.src = '//static.accesstrade.vn/js/atsmarttag.min.js?v=1.1.0';
        script.type = 'text/javascript';
        script.async = true;
        (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(script);
    })();
};

$(document).ready(function () {
    $(function () {
        $.scrollUp({
            scrollName: 'scrollUp', // Element ID
            scrollDistance: 300, // Distance from top/bottom before showing element (px)
            scrollFrom: 'top', // 'top' or 'bottom'
            scrollSpeed: 300, // Speed back to top (ms)
            easingType: 'linear', // Scroll to top easing (see http://easings.net/)
            animation: 'fade', // Fade, slide, none
            animationSpeed: 200, // Animation in speed (ms)
            scrollTrigger: false, // Set a custom triggering element. Can be an HTML string or jQuery object
            //scrollTarget: false, // Set a custom target element for scrolling to the top
            scrollText: '<i class="fa fa-angle-up"></i>', // Text for element, can contain HTML
            scrollTitle: false, // Set a custom <a> title if required.
            scrollImg: false, // Set true to use image
            activeOverlay: false, // Set CSS color to display scrollUp active point, e.g '#00FFFF'
            zIndex: 10 // Z-Index for the overlay
        });
    });

    $(".event-title").click(function () {
        $(".event-title").removeClass("event-title-active");
        $(this).addClass("event-title-active");
    });

    $(".filter-sex-item").on('click', function () {
        $(".filter-sex-item").removeClass('filter-sex-item-active');
        $(this).addClass('filter-sex-item-active');
    });

    $(document).on('click', '.filter-brand-img', function () {
        $(this).removeClass('filter-brand-img');
        $(this).addClass('filter-brand-img-active');
    });

    $(document).on('click', '.filter-brand-img-active', function () {
        $(this).removeClass('filter-brand-img-active');
        $(this).addClass('filter-brand-img');
    });

    $(document).on('click', '.nav-tab-items', function () {
        var html = '';
        price = 'down';
        html += '<g stroke="none" stroke-width="1" fill-rule="evenodd">';
        html += '<g id="upfront_filter_specs" transform="translate(-345.000000, -97.000000)">';
        html += '<g id="icon_grey" transform="translate(343.000000, 96.000000)">';
        html += '<g id="Group-12" stroke-width="1" transform="translate(2.500000, 1.000000)">';
        html += '<g id="Group-11" fill-rule="nonzero">';
        html += '<path d="M3.13751228,3.74264069 L0.150147331,0.853553391 C-0.0500491103,0.658291245 -0.0500491103,0.341708755 0.150147331,0.146446609 C0.350343772,-0.0488155365 0.674926335,-0.0488155365 0.875122776,0.146446609 L3.5,2.68497122 L6.12487722,0.146446609 C6.32507367,-0.0488155365 6.64965623,-0.0488155365 6.84985267,0.146446609 C7.05004911,0.341708755 7.05004911,0.658291245 6.84985267,0.853553391 L3.86248772,3.74264069 C3.66229128,3.93790283 3.33770872,3.93790283 3.13751228,3.74264069 Z" transform="translate(3.500000, 1.944544) scale(1, -1) translate(-3.500000, -1.944544) "></path>';
        html += '</g>';
        html += '<g transform="translate(3.500000, 8.000000) scale(1, -1) translate(-3.500000, -8.000000) translate(0.000000, 6.000000)" fill-rule="nonzero">';
        html += '<path d="M3.13751228,3.74264069 L0.150147331,0.853553391 C-0.0500491103,0.658291245 -0.0500491103,0.341708755 0.150147331,0.146446609 C0.350343772,-0.0488155365 0.674926335,-0.0488155365 0.875122776,0.146446609 L3.5,2.68497122 L6.12487722,0.146446609 C6.32507367,-0.0488155365 6.64965623,-0.0488155365 6.84985267,0.146446609 C7.05004911,0.341708755 7.05004911,0.658291245 6.84985267,0.853553391 L3.86248772,3.74264069 C3.66229128,3.93790283 3.33770872,3.93790283 3.13751228,3.74264069 Z" transform="translate(3.500000, 1.944544) scale(1, -1) translate(-3.500000, -1.944544) "></path>';
        html += '</g></g></g></g></g>';
        $('#svg_price').html(html);
        $('#svg_price').removeAttr('class');

        $('.nav-tab-items-active').addClass('nav-tab-items');
        $('.nav-tab-items-active').removeClass('nav-tab-items-active');
        $(this).addClass('nav-tab-items-active');
    });

    $(document).on('click', '.nav-item-price', function () {
        var html = '';
        if (price == 'down') {
            price = 'up';

            html += '<g stroke="none" stroke-width="1" fill-rule="evenodd">';
            html += '<g transform="translate(-344.000000, -97.000000)">';
            html += '<g transform="translate(303.000000, 94.000000)">';
            html += '<g transform="translate(40.000000, 2.000000)">';
            html += '<path d="M6.37298892,10.8430734 C6.27951893,10.9396946 6.14701198,11 6.000064,11 C5.85438692,11 5.72290214,10.9407332 5.62957298,10.8455722 L1.65014733,6.96422851 C1.44995089,6.76896637 1.44995089,6.45238388 1.65014733,6.25712173 C1.85034377,6.06185959 2.17492633,6.06185959 2.37512278,6.25712173 L5.48742894,9.29271806 L5.48742894,1.5 C5.48742894,1.22385763 5.71694348,1 6.000064,1 C6.28318452,1 6.51269905,1.22385763 6.51269905,1.5 L6.51269905,9.29259322 L9.62487722,6.25712173 C9.82507367,6.06185959 10.1496562,6.06185959 10.3498527,6.25712173 C10.5500491,6.45238388 10.5500491,6.76896637 10.3498527,6.96422851 L6.37298892,10.8430734 Z" fill-rule="nonzero"></path>';
            html += '</g></g></g></g>';

            $('#svg_price').removeAttr('class');
        }
        else {
            price = 'down';

            html += '<g stroke="none" stroke-width="1" fill-rule="evenodd">';
            html += '<g transform="translate(-344.000000, -97.000000)">';
            html += '<g transform="translate(303.000000, 94.000000)">';
            html += '<g transform="translate(40.000000, 2.000000)">';
            html += '<path d="M6.37298892,10.8430734 C6.27951893,10.9396946 6.14701198,11 6.000064,11 C5.85438692,11 5.72290214,10.9407332 5.62957298,10.8455722 L1.65014733,6.96422851 C1.44995089,6.76896637 1.44995089,6.45238388 1.65014733,6.25712173 C1.85034377,6.06185959 2.17492633,6.06185959 2.37512278,6.25712173 L5.48742894,9.29271806 L5.48742894,1.5 C5.48742894,1.22385763 5.71694348,1 6.000064,1 C6.28318452,1 6.51269905,1.22385763 6.51269905,1.5 L6.51269905,9.29259322 L9.62487722,6.25712173 C9.82507367,6.06185959 10.1496562,6.06185959 10.3498527,6.25712173 C10.5500491,6.45238388 10.5500491,6.76896637 10.3498527,6.96422851 L6.37298892,10.8430734 Z" fill-rule="nonzero"></path>';
            html += '</g></g></g></g>';

            $('#svg_price').attr('class', 'transform-180');
        }

        $('#svg_price').html(html);
        $('.nav-tab-items-active').addClass('nav-tab-items');
        $('.nav-tab-items-active').removeClass('nav-tab-items-active');
        $(this).addClass('nav-tab-items-active');
        $(this).removeClass('nav-tab-items');
    });

    $(".landing-tags").click(function () {
        $(".landing-tags").removeClass("landing-tags-body-active");
        $(this).addClass("landing-tags-body-active");

        var top = document.getElementById("landing-products-tags").offsetHeight;

        window.scroll({
            behavior: "smooth",
            block: "start",
            top: top,
            left: 0
        });
    });
});

app.controller('HomeController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {
    var url = "/Home/";

    $scope.key = 0; // 1: brand, 2: maingroup, 3: subgroup
    $scope.brand_id = 0;
    $scope.maingroup_id = 0;
    $scope.subgroup_id = 0;
    $scope.filter = 'discount';

    $scope.GetItemsByBrand = (brand_id) => {
        $scope.key = 1;
        $scope.brand_id = brand_id;

        var link = url + "GetItemsByBrand";

        var data_request = {
            brand_id: brand_id,
            filter: $scope.filter
        };

        $http.post(link, data_request).then(function (response) {
            if (response.status == 200) {
                var result = response.data;

                if (result.status) {
                    $scope.Products = result.data;
                }
                else {
                    console.log(result.message);
                }

                CrollToView();
            }
        }, function (error) {
            console.log(error.statusText);
        });
    };

    $scope.GetItemsByMaingroupAndSubgroup = (maingroup_id, subgroup_id) => {
        $scope.key = 3;
        $scope.maingroup_id = maingroup_id;
        $scope.subgroup_id = subgroup_id;

        var link = url + "GetItemsByBrand";

        var data_request = {
            maingroup_id: maingroup_id,
            subgroup_id: subgroup_id,
            filter: $scope.filter
        };

        $http.post(link, data_request).then(function (response) {
            if (response.status == 200) {
                var result = response.data;

                if (result.status) {
                    $scope.Products = result.data;
                }
                else {
                    console.log(result.message);
                }

                CrollToView();
            }
        }, function (error) {
            console.log(error.statusText);
        });
    };

    $scope.Filter = (filter) => {
        $scope.filter = filter;

        var link = url + "GetItemsByBrand";

        var data_request = {
            filter: filter
        };

        if ($scope.key == 1) {
            data_request = {
                brand_id: $scope.brand_id,
                filter: filter
            };
        }
        else if ($scope.key == 2) {
            data_request = {
                maingroup_id: $scope.maingroup_id,
                filter: filter
            };
        }
        else if ($scope.key == 3) {
            data_request = {
                maingroup_id: $scope.maingroup_id,
                subgroup_id: $scope.subgroup_id,
                filter: filter
            };
        }

        $http.post(link, data_request).then(function (response) {
            if (response.status == 200) {
                var result = response.data;

                if (result.status) {
                    $scope.Products = result.data;
                }
                else {
                    console.log(result.message);
                }
            }
        }, function (error) {
            //console.log(error.statusText);
        });

        var __atsmarttag = {
            pub_id: '5388638995624127439',
            utm_source: '',
            utm_medium: '',
            utm_campaign: '',
            utm_content: '',
            new_tab: 0
        };
        (function () {
            var script = document.createElement('script');
            script.src = '//static.accesstrade.vn/js/atsmarttag.min.js?v=1.1.0';
            script.type = 'text/javascript';
            script.async = true;
            (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(script);
        })();
    };

    var GetSaleProduct = () => {
        var link = url + "GetSaleProduct";

        $http.post(link, null).then(function (response) {
            if (response.status == 200) {

                var result = response.data;

                if (result.status) {
                    $scope.StardustProducts = result.data;

                    reload_aff();
                }
                else {
                    console.log(result.message);
                }
            }
        }, function (error) {
            console.log(error.statusText);
        });
    };

    var init = () => {
        let device = deviceType();

        if (device == "desktop") {
            $('.owl-carousel-danh-muc').owlCarousel({
                items: 10,
                margin: 15,
                autoplayHoverPause: true,
                autoplay: true,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });

            $('.owl-carousel-gia-soc').owlCarousel({
                items: 5,
                loop: true,
                nav: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: false,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });

            $('.owl-carousel-voucher').owlCarousel({
                items: 5,
                loop: true,
                nav: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: false,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });

            $('.owl-carousel-brands').owlCarousel({
                items: 5,
                loop: false,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: false,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });

            $('.owl-carousel-banner').owlCarousel({
                items: 1,
                loop: true,
                nav: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: true,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });
        }
        else if (device == "mobile") {
            $('.owl-carousel-danh-muc').owlCarousel({
                items: 3,
                loop: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: true,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });

            $('.owl-carousel-gia-soc').owlCarousel({
                items: 2,
                loop: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: true,
                autoplayTimeout: 4000,
                autoplayHoverPause: true
            });

            $('.owl-carousel-voucher-mobile').owlCarousel({
                items: 2,
                loop: true,
                nav: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: false,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });

            $('.owl-carousel-banner').owlCarousel({
                items: 1,
                loop: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: true,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });
        };
    };

    angular.element(document).ready(function () {
        init();

        GetSaleProduct();
    });
}])

app.controller('CategoryController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {
    var url = "/Category/";

    var page = 1;
    var limit = 16;
    var temp = "all";
    var temp_detail = null;
    var reload = true;

    $scope.get = (filter, filter_detail) => {
        if (filter == 'sale') {
            filter = price;
        }

        temp = filter;
        if (filter_detail == undefined) {
            filter_detail = temp_detail;
        }
        else {
            temp_detail = filter_detail;
        }

        page = 1;

        var data_request = {
            page: page,
            limit: limit,
            filter: temp,
            filter_detail: temp_detail
        };

        $http.post(location.pathname, data_request).then(function (response) {
            if (response.status == 200) {
                let result = response.data;

                if (result.status) {
                    $scope.CateProducts = result.data;

                    //Load lại scripts của aff để tao link cho sản phẩm
                    reload_aff();
                }
            }
        }, function (error) {
            console.log(error.statusText);
        });
    };

    $scope.ShowFiilter = () => {
        $scope.FilterShow = true;
    };

    $scope.CancelFilter = () => {
        $scope.FilterShow = false;
    };

    $scope.ApplyFilter = () => {
        var brands = [];
        var object_name_id = $(".filter-sex-item-active").data('id');
        var price_min = $("#slider-range-value1").text().replaceAll(',', '');
        var price_max = $("#slider-range-value2").text().replaceAll(',', '');
        var lst_brand = $(".filter-brand-img-active");

        for (var i = 0; i < lst_brand.length; i++) {
            brands.push($(lst_brand[i]).data('id'));
        }

        var param = {
            object_name_id: object_name_id,
            price_min: price_min,
            price_max: price_max,
            brands: brands
        };

        $scope.get(temp, param);

        $scope.FilterShow = false;
    };

    var init = () => {
        let device = deviceType();

        if (device == "desktop") {
            $('.owl-carousel-brands').owlCarousel({
                items: 5,
                loop: false,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: false,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });

            $('.owl-carousel-danh-muc-cate-index').owlCarousel({
                items: 10,
                margin: 15,
                nav: true,
                autoplayHoverPause: true,
                autoplay: false,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });
        }
        else if (device == "mobile") {
            $('.owl-carousel-brands').owlCarousel({
                items: 3,
                loop: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: false,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });

            $('.owl-carousel-danh-muc').owlCarousel({
                items: 3,
                loop: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: true,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });
        };
    }

    $(window).scroll(function () {

        if ($(window).scrollTop() > $(document).height() - $(window).height() - 100 && reload) {

            reload = false;
            page++;

            var data_request = {
                page: page,
                limit: limit,
                filter: temp,
                filter_detail: temp_detail
            };

            $http.post(location.pathname, data_request).then(function (response) {
                if (response.status == 200) {
                    let result = response.data;

                    if (result.status) {
                        $scope.CateProducts = $scope.CateProducts.concat(result.data);

                        //Load lại scripts của aff để tao link cho sản phẩm
                        reload_aff();

                        reload = true;
                    }
                }
            }, function (error) {
                console.log(error.statusText);
            });

        }
    });

    angular.element(document).ready(function () {
        init();

        $scope.get(temp, temp_detail);
    });
}])

app.controller('CategoryDetailController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {
    var url = "/Category/";

    var page = 1;
    var limit = 12;
    var temp = "all";
    var temp_detail = null;
    var reload = true;


    $scope.get = (filter, filter_detail) => {
        if (filter == 'sale') {
            filter = price;
        }

        temp = filter;
        if (filter_detail == undefined) {
            filter_detail = temp_detail;
        }
        else {
            temp_detail = filter_detail;
        }
        page = 1;

        var data_request = {
            page: page,
            limit: limit,
            filter: temp,
            filter_detail: temp_detail
        };

        $http.post(location.pathname, data_request).then(function (response) {
            if (response.status == 200) {
                let result = response.data;

                if (result.status) {
                    $scope.CateDetailProducts = result.data;

                    //Load lại scripts của aff để tao link cho sản phẩm
                    reload_aff();
                }
            }
        }, function (error) {
            console.log(error.statusText);
        });
    };

    $scope.ShowFiilter = () => {
        $scope.FilterShow = true;
    };

    $scope.CancelFilter = () => {
        $scope.FilterShow = false;
    };

    $scope.ApplyFilter = () => {
        var brands = [];
        var object_name_id = $(".filter-sex-item-active").data('id');
        var price_min = $("#slider-range-value1").text().replaceAll(',', '');
        var price_max = $("#slider-range-value2").text().replaceAll(',', '');
        var lst_brand = $(".filter-brand-img-active");

        for (var i = 0; i < lst_brand.length; i++) {
            brands.push($(lst_brand[i]).data('id'));
        }

        var param = {
            object_name_id: object_name_id,
            price_min: price_min,
            price_max: price_max,
            brands: brands
        };

        $scope.get(temp, param);

        $scope.FilterShow = false;
    };

    $(window).scroll(function () {
        if ($(window).scrollTop() > $(document).height() - $(window).height() - 100 && reload == true) {
            reload = false;
            page++;

            var data_request = {
                page: page,
                limit: limit,
                filter: temp,
                filter_detail: temp_detail
            };

            $http.post(location.pathname, data_request).then(function (response) {
                if (response.status == 200) {
                    let result = response.data;

                    if (result.status) {
                        $scope.CateDetailProducts = $scope.CateDetailProducts.concat(result.data);

                        //Load lại scripts của aff để tao link cho sản phẩm
                        reload_aff();

                        reload = true;
                    }
                }
            }, function (error) {
                console.log(error.statusText);
            });
        }
    });

    angular.element(document).ready(function () {
        $scope.get("all", null);
    });
}])

app.controller('BrandController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {
    var url = "/Category/";

    var page = 1;
    var limit = 12;
    var temp = "all";
    var temp_detail = null;
    var reload = true;

    $scope.get = (filter, filter_detail) => {
        if (filter == 'sale') {
            filter = price;
        }

        temp = filter;
        if (filter_detail == undefined) {
            filter_detail = temp_detail;
        }
        else {
            temp_detail = filter_detail;
        }
        page = 1;

        var data_request = {
            page: page,
            limit: limit,
            filter: temp,
            filter_detail: temp_detail

        };

        $http.post(location.pathname, data_request).then(function (response) {
            if (response.status == 200) {
                let result = response.data;

                if (result.status) {
                    $scope.BrandProducts = result.data;

                    //Load lại scripts của aff để tao link cho sản phẩm
                    reload_aff();
                }
            }
        }, function (error) {
            console.log(error.statusText);
        });
    };

    $scope.ShowFiilter = () => {
        $scope.FilterShow = true;
    };

    $scope.CancelFilter = () => {
        $scope.FilterShow = false;
    };

    $scope.ApplyFilter = () => {
        var brands = [];
        var object_name_id = $(".filter-sex-item-active").data('id');
        var price_min = $("#slider-range-value1").text().replaceAll(',', '');
        var price_max = $("#slider-range-value2").text().replaceAll(',', '');
        var lst_brand = $(".filter-brand-img-active");

        for (var i = 0; i < lst_brand.length; i++) {
            brands.push($(lst_brand[i]).data('id'));
        }

        var param = {
            object_name_id: object_name_id,
            price_min: price_min,
            price_max: price_max,
            brands: brands
        };

        $scope.get(temp, param);

        $scope.FilterShow = false;
    };

    var init = () => {
        let device = deviceType();

        if (device == "desktop") {
            $('.owl-carousel-brands').owlCarousel({
                items: 5,
                loop: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: false,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });

            $('.owl-carousel-danh-muc').owlCarousel({
                items: 10,
                margin: 15,
                autoplayHoverPause: true,
                autoplay: true,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });
        }
        else if (device == "mobile") {
            $('.owl-carousel-brands').owlCarousel({
                items: 3,
                loop: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: false,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });

            $('.owl-carousel-danh-muc').owlCarousel({
                items: 3,
                loop: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: true,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });
        };
    }

    $(window).scroll(function () {
        if ($(window).scrollTop() > $(document).height() - $(window).height() - 100 && reload == true) {
            reload = false;
            page++;

            var data_request = {
                page: page,
                limit: limit,
                filter: temp,
                filter_detail: temp_detail
            };

            $http.post(location.pathname, data_request).then(function (response) {
                if (response.status == 200) {
                    let result = response.data;

                    if (result.status) {
                        $scope.BrandProducts = $scope.BrandProducts.concat(result.data);

                        //Load lại scripts của aff để tao link cho sản phẩm
                        reload_aff();

                        reload = true;
                    }
                }
            }, function (error) {
                console.log(error.statusText);
            });
        }
    });

    angular.element(document).ready(function () {
        init();

        $scope.get("all", null);
    });
}])

app.controller('BrandSubgroupController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {
    var url = "/Category/";

    var page = 1;
    var limit = 12;
    var temp = "all";
    var temp_detail = null;
    var reload = true;

    $scope.get = (filter, filter_detail) => {
        if (filter == 'sale') {
            filter = price;
        }

        temp = filter;

        if (filter_detail == undefined) {
            filter_detail = temp_detail;
        }
        else {
            temp_detail = filter_detail;
        }
        page = 1;

        var data_request = {
            page: page,
            limit: limit,
            filter: temp,
            filter_detail: temp_detail
        };

        $http.post(location.pathname, data_request).then(function (response) {
            if (response.status == 200) {
                let result = response.data;

                if (result.status) {
                    $scope.BrandSubgroupProducts = result.data;

                    //Load lại scripts của aff để tao link cho sản phẩm
                    reload_aff();
                }
            }
        }, function (error) {
            console.log(error.statusText);
        });
    };

    $scope.ShowFiilter = () => {
        $scope.FilterShow = true;
    };

    $scope.CancelFilter = () => {
        $scope.FilterShow = false;
    };

    $scope.ApplyFilter = () => {
        var brands = [];
        var object_name_id = $(".filter-sex-item-active").data('id');
        var price_min = $("#slider-range-value1").text().replaceAll(',', '');
        var price_max = $("#slider-range-value2").text().replaceAll(',', '');
        var lst_brand = $(".filter-brand-img-active");

        for (var i = 0; i < lst_brand.length; i++) {
            brands.push($(lst_brand[i]).data('id'));
        }

        var param = {
            object_name_id: object_name_id,
            price_min: price_min,
            price_max: price_max,
            brands: brands
        };

        $scope.get(temp, param);

        $scope.FilterShow = false;
    };

    var init = () => {
        let device = deviceType();

        if (device == "desktop") {
            $('.owl-carousel-brands').owlCarousel({
                items: 5,
                loop: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: false,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });

            $('.owl-carousel-danh-muc').owlCarousel({
                items: 10,
                margin: 15,
                autoplayHoverPause: true,
                autoplay: true,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });
        }
        else if (device == "mobile") {
            $('.owl-carousel-brands').owlCarousel({
                items: 3,
                loop: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: false,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });

            $('.owl-carousel-danh-muc').owlCarousel({
                items: 3,
                loop: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: true,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });
        };
    }

    $(window).scroll(function () {
        if ($(window).scrollTop() > $(document).height() - $(window).height() - 100 && reload == true) {
            reload = false;
            page++;

            var data_request = {
                page: page,
                limit: limit,
                filter: temp,
                filter_detail: temp_detail
            };

            $http.post(location.pathname, data_request).then(function (response) {
                if (response.status == 200) {
                    let result = response.data;

                    if (result.status) {
                        $scope.BrandSubgroupProducts = $scope.BrandSubgroupProducts.concat(result.data);

                        //Load lại scripts của aff để tao link cho sản phẩm
                        reload_aff();

                        reload = true;
                    }
                }
            }, function (error) {
                console.log(error.statusText);
            });
        }
    });

    angular.element(document).ready(function () {
        init();

        $scope.get("all", null);
    });
}])

app.controller('BrandSubgroupDetailController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {
    var url = "/Category/";

    var page = 1;
    var limit = 12;
    var temp = "all";
    var filter_detail = null;
    var temp_detail = null;
    var reload = true;

    $scope.get = (filter, temp_detail) => {
        if (filter == 'sale') {
            filter = price;
        }

        temp = filter;

        if (filter_detail == undefined) {
            filter_detail = temp_detail;
        }
        else {
            temp_detail = filter_detail;
        }
        page = 1;

        var data_request = {
            page: page,
            limit: limit,
            filter: temp,
            filter_detail: temp_detail
        };

        $http.post(location.pathname, data_request).then(function (response) {
            if (response.status == 200) {
                let result = response.data;

                if (result.status) {
                    $scope.BrandDetailProducts = result.data;

                    //Load lại scripts của aff để tao link cho sản phẩm
                    reload_aff();
                }
            }
        }, function (error) {
            console.log(error.statusText);
        });
    };

    $scope.ShowFiilter = () => {
        $scope.FilterShow = true;
    };

    $scope.CancelFilter = () => {
        $scope.FilterShow = false;
    };

    $scope.ApplyFilter = () => {
        var brands = [];
        var object_name_id = $(".filter-sex-item-active").data('id');
        var price_min = $("#slider-range-value1").text().replaceAll(',', '');
        var price_max = $("#slider-range-value2").text().replaceAll(',', '');
        var lst_brand = $(".filter-brand-img-active");

        for (var i = 0; i < lst_brand.length; i++) {
            brands.push($(lst_brand[i]).data('id'));
        }

        var param = {
            object_name_id: object_name_id,
            price_min: price_min,
            price_max: price_max,
            brands: brands
        };

        $scope.get(temp, param);

        $scope.FilterShow = false;
    };

    $(window).scroll(function () {
        if ($(window).scrollTop() > $(document).height() - $(window).height() - 100 && reload == true) {

            reload = false;
            page++;

            var data_request = {
                page: page,
                limit: limit,
                filter: temp,
                filter_detail: temp_detail
            };

            $http.post(location.pathname, data_request).then(function (response) {
                if (response.status == 200) {
                    let result = response.data;

                    if (result.status) {
                        $scope.BrandDetailProducts = $scope.BrandDetailProducts.concat(result.data);

                        //Load lại scripts của aff để tao link cho sản phẩm
                        reload_aff();

                        reload = true;
                    }
                }
            }, function (error) {
                console.log(error.statusText);
            });
        }
    });

    angular.element(document).ready(function () {
        $scope.get("all", null);
    });
}])

app.controller('HeaderController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {
    var action;

    $('#search_in').on('focusout', function () {
        setTimeout(function () {
            $scope.SearchData = null;
            $scope.$digest();
        }, 300);
    });

    $("#search_in").focus(function () {
        $scope.Search_By_Keyword();
    });

    $(document).on('keypress', '#search_in', function (e) {
        if (e.which == 13) {
            e.preventDefault();

            if ($scope.keyword == null || $scope.keyword == undefined) {
                return;
            }

            window.location.href = "/tim-kiem?keyword=" + $scope.keyword;
        }
    });

    $scope.Search_By_Keyword = () => {
        if ($scope.keyword == undefined || $scope.keyword.length < 2) {
            $scope.SearchData = null;
            return;
        }

        clearTimeout(action);
        action = setTimeout(() => {

            var data_request = JSON.stringify({
                keyword: $scope.keyword
            });

            if ($scope.keyword.length > 1) {
                $http.post("/Home/Search_by_keyword", data_request).then(function (response) {
                    if (response.status == 200) {
                        let result = response.data;

                        if (result.data == null || result.data == []) {
                            $scope.SearchData = null;
                            $scope.$digest();
                            return;
                        }

                        if (result.status) {
                            $scope.SearchData = result.data;
                        }
                    }
                }, function (error) {
                    console.log(error.statusText);
                });
            }
            else {
                $scope.SearchData = null;
            }

        }, 500);

    };
}])

app.controller('SearchController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {
    var url = "/Category/";
    var page = 1;
    var limit = 12;
    var temp = "all";

    $scope.get = (filter) => {
        if (filter == 'sale') {
            filter = price;
        }

        temp = filter;
        page = 1;

        var data_request = {
            page: page,
            limit: limit,
            filter: temp
        };

        $http.post(location.pathname + location.search, data_request).then(function (response) {
            if (response.status == 200) {
                let result = response.data;

                if (result.status) {
                    $scope.SearchData = result.data;

                    //Load lại scripts của aff để tao link cho sản phẩm
                    reload_aff();
                }
            }
        }, function (error) {
            console.log(error.statusText);
        });
    };

    $(window).scroll(function () {
        if ($(window).scrollTop() > $(document).height() - $(window).height() - 100) {

            page++;

            var data_request = {
                page: page,
                limit: limit,
                filter: temp
            };

            $http.post(location.pathname + location.search, data_request).then(function (response) {
                if (response.status == 200) {
                    let result = response.data;

                    if (result.status) {
                        $scope.SearchData = $scope.SearchData.concat(result.data);

                        //Load lại scripts của aff để tao link cho sản phẩm
                        reload_aff();
                    }
                }
            }, function (error) {
                console.log(error.statusText);
            });
        }
    });

    var init = () => {
        let device = deviceType();

        if (device == "desktop") {
            $('.owl-carousel-danh-muc').owlCarousel({
                items: 10,
                margin: 15,
                autoplayHoverPause: true,
                autoplay: true,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });

            $('.owl-carousel-brands').owlCarousel({
                items: 5,
                loop: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: false,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });
        }
        else if (device == "mobile") {
            $('.owl-carousel-danh-muc').owlCarousel({
                items: 3,
                loop: true,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: true,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });
        };
    };

    angular.element(document).ready(function () {
        init();

        $scope.get("all");
    });
}])

app.controller('LandingPageController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {

    var page = 1;
    var limit = 12;
    var temp = null;
    var temp_detail = null;
    var reload = true;

    $scope.get = (discount) => {
        if (discount == null) {
            temp = null;
        }
        else {
            temp = discount;
        }

        page = 1;

        var data_request = {
            page: page,
            limit: limit,
            discount: temp
        };

        $http.post(location.pathname, data_request).then(function (response) {
            if (response.status == 200) {
                let result = response.data;

                if (result.status) {
                    $scope.LandingPageProducts = result.data;

                    //Load lại scripts của aff để tao link cho sản phẩm
                    reload_aff();
                }
            }
        }, function (error) {
            console.log(error.statusText);
        });
    };

    $scope.ShowFiilter = () => {
        $scope.FilterShow = true;
    };

    $scope.CancelFilter = () => {
        $scope.FilterShow = false;
    };

    $scope.ApplyFilter = () => {
        var brands = [];
        var object_name_id = $(".filter-sex-item-active").data('id');
        var price_min = $("#slider-range-value1").text().replaceAll(',', '');
        var price_max = $("#slider-range-value2").text().replaceAll(',', '');
        var lst_brand = $(".filter-brand-img-active");

        for (var i = 0; i < lst_brand.length; i++) {
            brands.push($(lst_brand[i]).data('id'));
        }

        var param = {
            object_name_id: object_name_id,
            price_min: price_min,
            price_max: price_max,
            brands: brands
        };

        $scope.get(null);

        $scope.FilterShow = false;
    };

    var init = () => {
        let device = deviceType();

        if (device == "desktop") {
            $('.owl-carousel-brands').owlCarousel({
                items: 5,
                loop: false,
                margin: 10,
                autoplayHoverPause: true,
                autoplay: false,
                autoplayTimeout: 3000,
                autoplayHoverPause: true
            });
        }
    }

    $(window).scroll(function () {

        if ($(window).scrollTop() > $(document).height() - $(window).height() - 100 && reload) {

            reload = false;
            page++;

            var data_request = {
                page: page,
                limit: limit,
                discount: temp
            };

            $http.post(location.pathname, data_request).then(function (response) {
                if (response.status == 200) {
                    let result = response.data;

                    if (result.status) {
                        $scope.LandingPageProducts = $scope.LandingPageProducts.concat(result.data);

                        //Load lại scripts của aff để tao link cho sản phẩm
                        reload_aff();
                    }
                }
            }, function (error) {
                console.log(error.statusText);
            });

            reload = true;
        }
    });

    angular.element(document).ready(function () {
        init();

        $scope.get(null);
    });
}])

app.controller('VoucherController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {
    $(document).ready(function () {
        function CopyText(id) {
            var text = document.getElementById(id), range, selection;

            if (document.body.createTextRange) {
                range = document.body.createTextRange();
                range.moveToElementText(text);
                range.select();
            } else if (window.getSelection) {
                selection = window.getSelection();
                range = document.createRange();
                range.selectNodeContents(text);
                selection.removeAllRanges();
                selection.addRange(range);

                document.execCommand("copy");
            }
        }

        $(".saochep").click(function () {
            CopyText($(this).data("id"));
        });
    })
}])
