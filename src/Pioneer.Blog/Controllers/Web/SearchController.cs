using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Pioneer.Blog.Model;
using Pioneer.Blog.Service;
using Pioneer.Pagination;

namespace Pioneer.Blog.Controllers.Web
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IPaginatedMetaService _paginatedMetaService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly IPostService _postService;

        public SearchController(ISearchService searchService,
            IPaginatedMetaService paginatedMetaService,
            ICategoryService categoryService,
            ITagService tagService,
            IPostService postService)
        {
            _searchService = searchService;
            _paginatedMetaService = paginatedMetaService;
            _categoryService = categoryService;
            _tagService = tagService;
            _postService = postService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Description = "Tìm kiếm";

            ViewBag.Header = "Search";
            ViewBag.Title = "Tìm kiếm";
            ViewBag.Pager = "search";
            ViewBag.Selected = "search";

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Tags = _tagService.GetAll();
            ViewBag.PopularPosts = _postService.GetPopularPosts();
            ViewBag.NewPosts = _postService.GetAll(true, false, false, 4).ToList();

            return View("~/Views/Search/Index.cshtml", new List<Post>());
        }

        [HttpPost]
        public ActionResult Index(SearchRequest request)
        {
            var searchResults = _searchService.SearchPosts(request.Query, 5, request.Page);
            ViewBag.PaginatedMeta = _paginatedMetaService.GetMetaData(searchResults.TotalMatchingPosts, request.Page, 5);

            ViewBag.Description = "Tìm kiếm";

            ViewBag.Header = "Search";
            ViewBag.Title = "Tìm kiếm";
            ViewBag.Selected = "search";
            ViewBag.Query = request.Query;
            ViewBag.TotalQueryMatches = searchResults.TotalMatchingPosts;

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Tags = _tagService.GetAll();
            ViewBag.PopularPosts = _postService.GetPopularPosts();
            ViewBag.NewPosts = _postService.GetAll(true, false, false, 4).ToList();

            return View("~/Views/Search/Index.cshtml", searchResults.Posts);
        }

        [HttpGet("search/{query}/{selectedPage:int?}")]
        public ActionResult GetQuery(string query, int selectedPage = 1)
        {
            var searchResults = _searchService.SearchPosts(query, 5, selectedPage);
            ViewBag.PaginatedMeta = _paginatedMetaService.GetMetaData(searchResults.TotalMatchingPosts, selectedPage, 5);

            ViewBag.Description = "TÌm kiếm";

            ViewBag.Header = "Search";
            ViewBag.Title = "Timf kiếm";
            ViewBag.Selected = "search";
            ViewBag.Query = query;
            ViewBag.TotalQueryMatches = searchResults.TotalMatchingPosts;

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Tags = _tagService.GetAll();
            ViewBag.PopularPosts = _postService.GetPopularPosts();
            ViewBag.NewPosts = _postService.GetAll(true, false, false, 4).ToList();

            return View("~/Views/Search/Index.cshtml", searchResults.Posts);
        }
    }
}
