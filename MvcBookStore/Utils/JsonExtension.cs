using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MvcBookStore.Models;
using Newtonsoft.Json;

namespace MvcBookStore.Utils
{
    public static class JsonExtension
    {
        public static string ToJson(this object source)
        {
            return JsonConvert.SerializeObject(source);
        }
    }

    public static class HtmlHelperExtension
    {
        public static HtmlString HtmlConvertToJson(this HtmlHelper htmlHelper, object model)
        {
            return new HtmlString(JsonConvert.SerializeObject(model, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            }));
        }

        public static MvcHtmlString BuildSortableLink(this HtmlHelper htmlHelper, string fieldName, string actionName,
            string sortField, QueryOption queryOption)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            var isCurrentSortField = queryOption.SortField == sortField;

            var url = urlHelper.Action(actionName, new
            {
                SortField = sortField,
                SortOrder = (isCurrentSortField && queryOption.SortOrder == SortOrder.ASC)
                    ? SortOrder.DESC
                    : SortOrder.ASC
            });
            var sortIcon = BuildSortIcon(isCurrentSortField, queryOption);

            return new MvcHtmlString($"<a href=\"{url}\">{sortIcon} {fieldName}</a>");
        }

        private static string BuildSortIcon(bool isCurrentSortField, QueryOption queryOption)
        {
            var classBuilder = new StringBuilder("glyphicon glyphicon-sort");
            if (isCurrentSortField)
            {
                classBuilder.Append("-by-alphabet");
                if (queryOption.SortOrder == SortOrder.DESC)
                {
                    classBuilder.Append("-alt");
                }
            }
            return $"<span class=\"{classBuilder}\"></span>";
        }
    }
}