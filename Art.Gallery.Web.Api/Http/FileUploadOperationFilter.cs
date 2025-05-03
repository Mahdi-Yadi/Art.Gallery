using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Art.Gallery.Web.Api.Http;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // بررسی کردن که آیا متد HTTP نیاز به فایل داره یا نه
        var hasFormFile = context.MethodInfo.GetParameters()
            .Any(p => p.ParameterType == typeof(IFormFile));

        if (hasFormFile)
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    { "multipart/form-data", new OpenApiMediaType { Schema = new OpenApiSchema { Type = "object" } } }
                }
            };
        }
    }
}