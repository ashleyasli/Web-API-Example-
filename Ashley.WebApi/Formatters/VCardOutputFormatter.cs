using Ashley.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ashley.WebApi.Formatters
{
    public class VCardOutputFormatter : TextOutputFormatter
    {   
        public VCardOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;

            var stringBuilder = new StringBuilder();

            if (context.Object is List<ContactModel>)
            {
                foreach (ContactModel model in context.Object as List<ContactModel>)
                {
                    FormatVCard(stringBuilder, model);
                }
            }
            else { 
            var model = context.Object as ContactModel;
            FormatVCard(stringBuilder, model);
            }
        return response.WriteAsync(stringBuilder.ToString());
        }

    public static void FormatVCard(StringBuilder stringBuilder, ContactModel model)
        {
            stringBuilder.AppendLine("BEGIN:VCARD");
            stringBuilder.AppendLine("VERSION:2.1");
            stringBuilder.AppendLine("BEGIN:VCARD");
            stringBuilder.AppendLine($"N: {model.LastName}; {model.FirstName}");
            stringBuilder.AppendLine($"FN: {model.FirstName}; {model.LastName}");
            stringBuilder.AppendLine($"UID: {model.Id}\r\n");
            stringBuilder.AppendLine("END:VCARD");
        }
        protected override bool CanWriteType(Type type)
        {
            if (typeof(ContactModel).IsAssignableFrom(type) || typeof(List<ContactModel>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
    }
}
