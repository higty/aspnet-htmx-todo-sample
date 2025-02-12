using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspnetHtmxTodoSample.TagHelpers;

[HtmlTargetElement("input-field-panel")]
public class InputFieldPanelTagHelper : TagHelper
{
    public string Text { get; set; } = "";
    public string Name { get; set; } = "";
    public string Value { get; set; } = "";
    public bool DatePicker { get; set; } = false;

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        {
            var div = new TagBuilder("div");
            div.InnerHtml.AppendHtml(this.Text);
            output.Content.AppendHtml(div);
        }
        {
            var tx = new TagBuilder("input");
            tx.Attributes.Add("type", "text");
            tx.Attributes.Add("name", this.Name);
            tx.Attributes.Add("value", this.Value);
            if (this.DatePicker)
            {
                tx.Attributes.Add("date-picker", "true");
            }
            output.Content.AppendHtml(tx);
        }
        {
            var div = new TagBuilder("div");
            div.Attributes.Add("validate-result-panel", this.Name);
            output.Content.AppendHtml(div);
        }
        return base.ProcessAsync(context, output);
    }
}
