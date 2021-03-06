using System;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Orso.Arpa.Api.ModelBinding
{
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(DateTime);

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            if(reader.TryGetDateTime(out DateTime result))
            {
                return result;
            }
            return DateTime.MinValue;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            if (value == DateTime.MinValue)
            {
                writer.WriteStringValue((string)null);
            } else
            {
                writer.WriteStringValue(value.ToString("s", System.Globalization.CultureInfo.InvariantCulture) + "Z");
            }
        }
    }
}
