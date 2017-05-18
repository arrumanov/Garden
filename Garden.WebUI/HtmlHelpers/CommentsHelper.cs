using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Garden.WebUI.Models;
using Garden.Domain.Abstract;
using Garden.Domain.Entities;
using System.Data.Entity;

namespace Garden.WebUI.HtmlHelpers
{
    public static class CommentsHelper
    {
        public static MvcHtmlString CommentsList(this HtmlHelper html,
                                              IEnumerable<Message> lm)//Topic topic)
        {
            StringBuilder result = new StringBuilder();
            //IOrderedEnumerable k = topic.Messages.OrderByDescending();

            foreach (var item in lm /*topic.Messages*/)
            {
                TagBuilder div = new TagBuilder("div");
                TagBuilder span = new TagBuilder("span ");

                span.SetInnerText(item.Date + " | " + item.TestMessage);
                div.InnerHtml += span.ToString();

                span.AddCssClass("input-xlarge uneditable-input");
                div.AddCssClass("comment");

                result.Append(div.ToString());
            }


            return MvcHtmlString.Create(result.ToString());
        }
    }
}