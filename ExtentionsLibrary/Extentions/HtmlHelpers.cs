using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
