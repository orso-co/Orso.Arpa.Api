using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Orso.Arpa.Api.ModelBinding
{
    public class TrimmedStringConverter : JsonConverter<string>
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(string);

        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString() is string value ? value.Trim() : null;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
