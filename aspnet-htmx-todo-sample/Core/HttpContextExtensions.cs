using static AspnetHtmxTodoSample.Pages.ApiEndpoint;
using System.Text.Json;
using HigLabo.Web;

namespace AspnetHtmxTodoSample;

public static class HttpContextExtensions
{
    public static async ValueTask<T> CreateFromBody<T>(this HttpContext context)
    {
        var bodyText = await context.Request.GetRequestBodyTextAsync();
        var options = new JsonSerializerOptions();
        options.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString;
        options.Converters.Add(new HigLabo.Core.NullableDateOnlyJsonConverter());
        var o = JsonSerializer.Deserialize<T>(bodyText, options);
        if (o == null) throw new InvalidOperationException();
        return o;
    }
}
