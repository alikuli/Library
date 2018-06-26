using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;


namespace AliKuli.Extentions
{
    public static class MvcHtmlHelpers
    {
        public static MvcHtmlString EnumSortedDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel = null, IDictionary<string, object> htmlAttributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var selectList = EnumHelper.GetSelectList(metadata.ModelType).OrderBy(i => i.Text).ToList();
            if (!String.IsNullOrEmpty(optionLabel) && selectList.Count != 0 && String.IsNullOrEmpty(selectList[0].Text))
            {
                selectList[0].Text = optionLabel;
                optionLabel = null;
            }

            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, htmlAttributes);
        }

    }
}
