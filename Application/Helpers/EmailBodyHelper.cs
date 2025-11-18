using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class EmailBodyHelper
    {
        public static string GenerateEmailBody(string template, Dictionary<string, string> placeholders)
        {
            var basePath = AppContext.BaseDirectory;
            var filePath = Path.Combine(basePath, "Templates", $"{template}.html");
            var body = File.ReadAllText(filePath);
            foreach (var placeholder in placeholders)
            {
                body = body.Replace(placeholder.Key, placeholder.Value);
            }
            return body;
        }
    }
}
