using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MvcBookStore.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MvcBookStore.Utils
{
    public static class JsonExtension
    {
        public static string ToJson(this object source)
        {
            return JsonConvert.SerializeObject(source,new JsonSerializerSettings()
            {
                //ContractResolver = new CamelCasePropertyNamesContractResolver()
                ContractResolver = new UnderlineSplitContractResolver()
            });
        }
    }

    public static class QueryOptionExtension
    {
        public static QueryOption Page(this QueryOption queryOption, int page)
        {
            var option = queryOption.Clone();
            option.CurrentPage = page;
            return option;
        }
    }
    public static class HtmlHelperExtension
    {
        public static HtmlString HtmlConvertToJson(this HtmlHelper htmlHelper, object model)
        {
            return new HtmlString(JsonConvert.SerializeObject(model, new JsonSerializerSettings()
            {
                //ContractResolver = new UnderlineSplitContractResolver(),
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
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

        public static MvcHtmlString BuildKnockoutSortableLink(this HtmlHelper htmlHelper,
            string fieldName, string actionName, string sortField)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return new MvcHtmlString($@"
<a href=""{urlHelper.Action(actionName)}"" 
    data-bind=""click: pagingService.sortEntitiesBy"" 
    data-sort-field=""{sortField}"" >
    {fieldName}
    <span data-bind=""css: pagingService.buildSortIcon('{sortField}')""></span>
</a>
");
        }

        public static MvcHtmlString BuildKnockoutNextPreviousLinks(
        this HtmlHelper htmlHelper, string actionName)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return new MvcHtmlString($@"
<nav>
    <ul class=""pager"">
        <li data-bind=""css: pagingService.buildPreviousClass()"">
            <a href=""{urlHelper.Action(actionName)}"" data-bind=""click: pagingService.previousPage"">Previous</a>
        </li>
        <li data-bind=""css: pagingService.buildNextClass()"">
            <a href=""{urlHelper.Action(actionName)}"" data-bind=""click: pagingService.nextPage"">Next</a>
        </li>
    </ul>
</nav>
");
        }
    }
}