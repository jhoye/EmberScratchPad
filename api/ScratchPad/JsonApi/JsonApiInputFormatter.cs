using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonApiSerializer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

namespace ScratchPad.JsonApi
{
    public class JsonApiInputFormatter : TextInputFormatter
    {
        private readonly IList<Type> ValidModelTypes;

        public JsonApiInputFormatter()
        {
            ValidModelTypes = JsonApiHelper.GetActionMethodInfo()
                .SelectMany(a => a.GetParameters())
                .Select(a => a.ParameterType)
                .ToList();

            JsonApiHelper.ConfigureFormatter(
                SupportedMediaTypes,
                SupportedEncodings
            );
        }

        public override bool CanRead(InputFormatterContext context)
        {
            return ValidModelTypes.Contains(context.ModelType);
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            string json;

            using (var reader = new StreamReader(context.HttpContext.Request.Body))
            using(var task = reader.ReadToEndAsync())
            {
                json = task.Result;
            }

            var obj = JsonConvert.DeserializeObject(json, context.ModelType, new JsonApiSerializerSettings());

            return InputFormatterResult.SuccessAsync(obj);
        }
    }
}