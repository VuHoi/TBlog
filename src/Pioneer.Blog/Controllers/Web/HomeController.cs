﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pioneer.Blog.Model;
using Pioneer.Blog.Model.Views;
using Pioneer.Blog.Service;


namespace Pioneer.Blog.Controllers.Web
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ISiteMapService _siteMapService;
        private readonly ICommunicationService _communicationService;
        private readonly IRssService _rssService;

        public HomeController(IPostService postService,
            ISiteMapService siteMapService,
            ICommunicationService communicationService, 
            IRssService rssService)
        {
            _postService = postService;
            _siteMapService = siteMapService;
            _communicationService = communicationService;
            _rssService = rssService;
        }

        public IActionResult Index()
        {
            ViewBag.Description =
                "Trang chủ";

            ViewBag.PopularPosts = _postService.GetPopularPosts().ToList();
            ViewBag.LatestPosts = _postService.GetAll(true, false, false, 8).ToList();

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //TODO: Figure out how to combine logic with Index
        public ActionResult SignUp(SignUpViewModel model)
        {
            ViewBag.Description =
                "Đăng kí";

            ViewBag.PopularPosts = _postService.GetPopularPosts().ToList();
            ViewBag.LatestPosts = _postService.GetAll(true, false, false, 8).ToList();

            if (!ModelState.IsValid)
            {
                ViewBag.IsValid = false;
                return View("Index", model);
            }

            //var response = _communicationService.SignUpToMailingList(model);

            //if (response.Status != OperationStatus.Created)
            //{
            //    ViewBag.IsValid = false;
            //    ViewBag.IsValidMessage = response.Message;
            //    return View("Index", model);
            //}

            ViewBag.IsValid = true;

            return View("Index", model);
        }

        public IActionResult SiteMap()
        {
            return Content(_siteMapService.GetSiteMap(), "text/xml", Encoding.UTF8);
        }

        public IActionResult RssFeed()
        {
            return Content(_rssService.GetFeed(), "text/xml", Encoding.UTF8);
        }
    }
}