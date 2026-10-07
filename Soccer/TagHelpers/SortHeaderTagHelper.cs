using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Soccer.Models;

namespace Soccer.TagHelpers;

// Застосування первинного конструктора
public class SortHeaderTagHelper(IUrlHelperFactory urlHelperFactory) : TagHelper
{
    public SortState Property { get; set; } // значення поточного властивості
    public SortState Current { get; set; }  // значення активної властивості для сортування
    public string? Action { get; set; }     // дія контролера
    public bool Up { get; set; }            // сортування за зростанням/спаданням

    [ViewContext] // Атрибут вказує механізму ASP.NET Core автоматично впровадити поточний контекст виконання представлення у цю властивість під час створення екземпляра Tag Helper-а.
    [HtmlAttributeNotBound] // Атрибут забороняє рушію Razor шукати та прив'язувати цю властивість до однойменного HTML-атрибута в розмітці.
    public ViewContext ViewContext { get; set; } = null!;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
        output.TagName = "a";
        string? url = urlHelper.Action(Action, new { sortOrder = Property });
        output.Attributes.SetAttribute("href", url);
        output.Attributes.SetAttribute("class", "text-decoration-none text-dark fw-bold"); // Сучасна стилізація посилання

        // Якщо поточна властивість відповідає обраній для сортування
        if (Current == Property)
        {
            TagBuilder tag = new("i");
            tag.AddCssClass("bi"); // Заміна застарілого glyphicon на Bootstrap Icons
            tag.AddCssClass(Up ? "bi-chevron-up" : "bi-chevron-down");
            tag.Attributes.Add("style", "margin-left: 0.25rem; font-size: 0.8em;");

            output.PostContent.AppendHtml(tag);
        }
    }
}