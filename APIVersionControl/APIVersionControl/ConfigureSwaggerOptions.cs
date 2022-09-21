using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace APIVersionControl
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "My .Net API restful",
                Version = description.ApiVersion.ToString(),
                Description = "This is my first API version control",
                Contact = new OpenApiContact()
                {
                    Email = "apiVersion@control.cl",
                    Name = "ApiVersion"
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += ". This API version has been deprecated";
            }

            return info;
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            //Add Swagger documentation for each API version
            foreach(var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        public void Configure(SwaggerGenOptions options)
        {
            Configure(options);
        }
    }
}
