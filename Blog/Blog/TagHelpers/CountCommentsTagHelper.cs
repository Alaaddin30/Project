using BlogApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.TagHelpers
{
    [HtmlTargetElement(Attributes = "comment-total")]
    public class CountCommentsTagHelper:TagHelper
    {
        private IBlogRepository repository;
        public CountCommentsTagHelper(IBlogRepository _repository)
        {
            repository = _repository;
        }
        [HtmlAttributeName("comment-blog-id")]
        public int BlogId { get; set; }
        [HtmlAttributeName("comment-cssClass")]
        public string CssClass { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            int totalComment = 0;
            TagBuilder span = new TagBuilder("span");
            span.AddCssClass(CssClass);
            Models.Blog blog = repository.Blog(BlogId);
            if (blog != null) { totalComment = blog.Comments.Count; }
            span.InnerHtml.Append(totalComment.ToString());
            output.Content.AppendHtml(span);
            
        }
    }
}
