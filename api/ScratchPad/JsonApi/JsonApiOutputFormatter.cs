using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonApiSerializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

namespace ScratchPad.JsonApi
{
    public class JsonApiOutputFormatter : TextOutputFormatter
    {
        private readonly IList<Type> ValidModelTypes;

        public JsonApiOutputFormatter()
        {
            var returnTypes = new List<Type>();

            ValidModelTypes = JsonApiHelper.GetActionMethodInfo()
                .Select(a =>
                    a.ReturnType.IsGenericType &&
                    (
                        a.ReturnType.FullName.StartsWith("Microsoft.AspNetCore.Mvc.ActionResult") ||
                        a.ReturnType.FullName.StartsWith("System.Threading.Tasks.Task")
                    )
                        ? a.ReturnType.GenericTypeArguments.First()
                        : a.ReturnType
                )
                .ToList();

            JsonApiHelper.ConfigureFormatter(
                SupportedMediaTypes,
                SupportedEncodings
            );
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;

            var json = JsonConvert.SerializeObject(
                context.Object,
                new JsonApiSerializerSettings());

            return response.WriteAsync(json);
        }

        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return ValidModelTypes.Contains(context.ObjectType);
        }
    }
}