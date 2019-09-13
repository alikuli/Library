using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AliKuli.Extentions
{
    public static class HtmlHelpers
    {
        public static IHtmlString ImageBoot4(this HtmlHelper helper, string src, string alt)
        {
            TagBuilder tb = new TagBuilder("img");
            tb.Attributes.Add("src", VirtualPathUtility.ToAbsolute(src));
            tb.Attributes.Add("alt", alt);
            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
        }

        public static IHtmlString FileBrowserBoot4(this HtmlHelper helper, string label = "")
        {
            if (label.IsNullOrWhiteSpace())
                label = "Choose File";

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<div class='custom-file'>"));
            sb.Append(string.Format("<input type='file' class='custom-file-input' id='customFile' alt= 'choose file'>"));
            sb.Append(string.Format("<label class='custom-file-label' for='customFile'>{0}</label>", label));
            sb.Append(string.Format("</div>"));

            return new MvcHtmlString(sb.ToString());
        }

        /// <summary>
        /// https://www.ryadel.com/en/asp-net-mvc-dinamically-set-htmlattributes-razor-html-helpers-readonly/
        /// Gets an object containing a htmlAttributes collection for any Razor HTML helper component,
        /// supporting a static set (anonymous object) and/or a dynamic set (Dictionary)
        /// </summary>
        /// <param name="fixedHtmlAttributes">A fixed set of htmlAttributes (anonymous object)</param>
        /// <param name="dynamicHtmlAttributes">A dynamic set of htmlAttributes (Dictionary)</param>
        /// <returns>A collection of htmlAttributes including a merge of the given set(s)</returns>
        /// 
        ///It can be used in the following way:

        /// var dic = new Dictionary<string,object>();
        /// if (IsReadOnly()) dic.Add("readonly", "readonly");
        /// Html.TextBoxFor(m => m.Name, GetHtmlAttributes(new { @class="someclass" }, dic))
        /// 
        public static IDictionary<string, object> GetHtmlAttributes(
            object fixedHtmlAttributes = null,
            IDictionary<string, object> dynamicHtmlAttributes = null
            )
        {
            var rvd = (fixedHtmlAttributes == null)
                ? new RouteValueDictionary()
                : HtmlHelper.AnonymousObjectToHtmlAttributes(fixedHtmlAttributes);
            if (dynamicHtmlAttributes != null)
            {
                foreach (KeyValuePair<string, object> kvp in dynamicHtmlAttributes)
                    rvd[kvp.Key] = kvp.Value;
            }
            return rvd;
        }

        //public static IHtmlString TextBoxEmailBoot4(this HtmlHelper helper)
        //{
        //    string guid = getGuid();
        //    string id = "emailId" + guid;
        //    string idHelp = "emailHelp" + guid;

        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(string.Format("<div class='form-group'>"));
        //    sb.Append(string.Format("<label for='{0}'>Email address</label>", id));
        //    sb.Append(string.Format("<input type='email' class='form-control' id='{0}' aria-describedby='{1}' placeholder='Enter email'>", id, idHelp));
        //    sb.Append(string.Format("<small id='{0}' class='form-text text-muted'>We'll never share your email with anyone else.</small>", idHelp));
        //    sb.Append(string.Format("</div>"));

        //    return new MvcHtmlString(sb.ToString());

        //}

        //public static IHtmlString EmailTextBoxBoot4<TModel, TProperty>(this HtmlHelper htmlHelper, Expression<Func<TModel, TProperty>> expr) where TModel : class
        //{
        //    string id = expr.Name;
        //    string maxLen = 

        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(string.Format("<div class='form-group'>"));
        //    sb.Append(string.Format("<label for='{0}'>Email address</label>", id));
        //    sb.Append(string.Format("<input type='email' class='form-control' id='{0}' aria-describedby='{1}' placeholder='Enter email'>", id, idHelp));
        //    sb.Append(string.Format("<small id='{0}' class='form-text text-muted'>We'll never share your email with anyone else.</small>", idHelp));
        //    sb.Append(string.Format("</div>"));

        //    return new MvcHtmlString(sb.ToString());
        //}

        //public static MvcHtmlString TextBoxForMaxLength<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        //{
        //    var member = expression.Body as MemberExpression;
        //    var stringLength = member.Member
        //        .GetCustomAttributes(typeof(StringLengthAttribute), false)
        //        .FirstOrDefault() as StringLengthAttribute;

        //    var attributes = (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes);
        //    if (stringLength != null)
        //    {
        //        attributes.Add("maxlength", stringLength.MaximumLength);
        //    }
        //    return htmlHelper.TextBoxFor(expression, attributes);
        //}
        //private static string getGuid()
        //{
        //    return Guid.NewGuid().ToString().Substring(0, 5);
        //}
    }
}
