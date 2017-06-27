using BlogApp.Models.ViewModels;
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
    [HtmlTargetElement("div", Attributes = "pagination-all")]
    public class PagerTagHelper:TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PagerTagHelper(IUrlHelperFactory _urlHelperFactory)
        {
            urlHelperFactory = _urlHelperFactory;
        }
        [HtmlAttributeNotBound]
        [ViewContext] // Current ViewContext when PaginationTagHelper was created
        public ViewContext ViewContext { get; set; }
        [HtmlAttributeName("pagination-model")]
        public PaginationInfo PaginationModel { get; set; }
        [HtmlAttributeName("action-method")]
        public string ActionMethod { get; set; }

        //[HtmlAttributeName("category-id")]
        //public int CurrentCategoryId { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder div = new TagBuilder("div");

            if (PaginationModel.TotalPages > 1 && PaginationModel.CurrentPage != 1)
            {
                TagBuilder previousLink = new TagBuilder("a");
                previousLink.Attributes["href"] = urlHelper.Action(ActionMethod, new { page = PaginationModel.CurrentPage - 1 });
                previousLink.InnerHtml.Append("Prev");
                div.InnerHtml.AppendHtml(previousLink);
            }

            for (int i = 1; i <= PaginationModel.TotalPages; i++)
            {
                TagBuilder a = new TagBuilder("a");
                if (PaginationModel.CurrentPage == i) { a.AddCssClass("active"); }
                a.Attributes["href"] = urlHelper.Action(ActionMethod, new { page = i });
                a.InnerHtml.Append(i.ToString());
                div.InnerHtml.AppendHtml(a);
            }



            if (PaginationModel.TotalPages > 1 && PaginationModel.CurrentPage != PaginationModel.TotalPages)
            {
                TagBuilder nextLink = new TagBuilder("a");
                nextLink.Attributes["href"] = urlHelper.Action(ActionMethod, new { page = PaginationModel.CurrentPage + 1 });
                nextLink.InnerHtml.Append("Next");
                div.InnerHtml.AppendHtml(nextLink);

            }
            output.Content.AppendHtml(div.InnerHtml);
        }
    }
}
