using BlogApp.Infrastructure;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "navigation")]
    public class CategoryTagHelper : TagHelper
    {
        private ICategoryRepository repository;
        private IUrlHelperFactory urlHelperFactory;
        public CategoryTagHelper(IUrlHelperFactory _urlHelperFactory, ICategoryRepository _repository)
        {
            urlHelperFactory = _urlHelperFactory;
            repository = _repository;
        }
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName("category-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("category-action")]
        public string ActionMethod { get; set; }

        [HtmlAttributeName("current-category-id")]
        public int CategoryId { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
             int categoryId =Convert.ToInt32(CategoryId);

            Dictionary<int, Category> categories = repository.Categories.ToDictionary(c => c.CategoryId);
            TagBuilder div = new TagBuilder("div");
            TagBuilder home = new TagBuilder("a");
            home.AddCssClass("home");
            home.Attributes["href"] = urlHelper.Action(ActionMethod, Controller, new { categoryId= 0, page = 1 });
            home.InnerHtml.Append("Home");
            ////if (CategoryId == 0) { home.AddCssClass("active"); }
            div.InnerHtml.AppendHtml(home);
            foreach (var kvp in categories)
            {
                TagBuilder a = new TagBuilder("a");
                a.Attributes["href"]= urlHelper.Action(ActionMethod, Controller, new { categoryId = kvp.Key , page = 1 });
                a.InnerHtml.Append(categories[kvp.Key].CategoryName.ToString());
                if (kvp.Key == CategoryId) { a.AddCssClass("active disabled"); }
                div.InnerHtml.AppendHtml(a);
            }
            output.Content.AppendHtml(div.InnerHtml);
        }
    }
}


/*  <a href="controller/action/CategoryId/PageId">CategoryName</a>
 *        <div>
 *             <a href="#">Home</a>
               <a active href="#">Cat-1</a>
               <a href="">Cat-2</a>
               <a href="#">Cat-3</a>
               <a href="#">Cat-4</a>
               <a href="#">Cat-5</a>
           </div>

    */