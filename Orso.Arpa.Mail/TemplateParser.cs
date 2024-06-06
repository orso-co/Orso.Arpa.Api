using System.IO;
using System.Reflection;
using System.Text;
using Orso.Arpa.Mail.Interfaces;

namespace Orso.Arpa.Mail
{
    public class TemplateParser : ITemplateParser
    {
        public string Parse(ITemplate templateData)
        {
            var builder = new StringBuilder();
            var templatePath = $"{Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Templates", "Html", $"{templateData.Name}.html")}";

            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"No email template file found for path ${templatePath}");
            }

            using (StreamReader reader = File.OpenText(templatePath))
            {
                builder.Append(reader.ReadToEnd());
            }

            foreach (PropertyInfo propertyInfo in templateData.GetType().GetProperties())
            {
                if (propertyInfo.PropertyType != typeof(string) || !propertyInfo.CanRead)
                {
                    continue;
                }
                var value = (string)propertyInfo.GetValue(templateData);
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = "- ohne -";
                }
                builder.Replace("{{" + propertyInfo.Name + "}}", value);
            }

            return builder.ToString();
        }
    }
}
