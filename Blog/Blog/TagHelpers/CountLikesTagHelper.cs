using BlogApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace BlogApp.TagHelpers
{
    [HtmlTargetElement(Attributes ="like-total")]
    public class CountLikesTagHelper:TagHelper
    {
        private ILikeRepository repository;

        [HtmlAttributeName("like-blog-id")]
        public int BlogId { get; set; }

        [HtmlAttributeName("like-total")]
        public LikeEnum Total { get; set; }

        [HtmlAttributeName("like-cssClass")]
        public string CssClass { get; set; }
        public CountLikesTagHelper(ILikeRepository _repository)
        {
            repository = _repository;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder span = new TagBuilder("span");
            span.AddCssClass(CssClass);
            int totalLikes = repository.Likes.Where(l => l.BlogId == BlogId && l.Liked == true).Count();
            int totalDislikes = repository.Likes.Where(l => l.BlogId == BlogId && l.Liked == false).Count();
            if (Total == LikeEnum.LIKE)
            {
                span.InnerHtml.Append(totalLikes.ToString());
            }
            else
            {
               span.InnerHtml.Append(totalDislikes.ToString());
            }

            output.Content.AppendHtml(span.InnerHtml);
        }
    }
}
