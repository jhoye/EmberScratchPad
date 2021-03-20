using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace ScratchPad.JsonApi
{
    public static class JsonApiHelper
    {
        public static void ConfigureFormatter(
            MediaTypeCollection supportedMediaTypes,
            IList<Encoding> supportedEncodings)
        {
            supportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/vnd.api+json"));
            supportedEncodings.Add(Encoding.UTF8);
            supportedEncodings.Add(Encoding.Unicode);
        }

        public static IEnumerable<MethodInfo> GetActionMethodInfo()
        {
            return Assembly.GetAssembly(typeof(ScratchPad.Program))
                .GetTypes()
                .Where(type=> typeof(Microsoft.AspNetCore.Mvc.ControllerBase).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof( System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any());
        }

        public static void RegisterFormatters(MvcOptions options)
        {
            options.InputFormatters.Insert(0, new JsonApiInputFormatter());
            options.OutputFormatters.Insert(0, new JsonApiOutputFormatter());
        }

        public static void ExceptionHandler(IApplicationBuilder configure)
        {
            configure.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                if (exceptionHandlerPathFeature?.Error is JsonApiException)
                {
                    var exception = (JsonApiException) exceptionHandlerPathFeature.Error;

                    context.Response.ContentType = "application/vnd.api+json";
                    context.Response.StatusCode = (int) exception.StatusCode;

                    await context.Response.WriteAsync(
                        JsonConvert.SerializeObject(new {
                            errors = exception.Errors
                        })
                    );
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = 500;

                    await context.Response.WriteAsync(
                        exceptionHandlerPathFeature?.Error?.Message
                            ?? "An internal server error occured."
                    );
                }
            });
        }
    }
}