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

            using (StreamReader reader = File.OpenText($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Templates\\Html\\{templateData.Name}.html"))
            {
                builder.Append(reader.ReadToEnd());
            }

            foreach (PropertyInfo propertyInfo in templateData.GetType().GetProperties())
            {
                if (propertyInfo.PropertyType != typeof(string) || !propertyInfo.CanRead)
                {
                    continue;
                }
                builder.Replace("{{" + propertyInfo.Name + "}}", (string)propertyInfo.GetValue(templateData));
            }

            return builder.ToString();
        }
    }
}
