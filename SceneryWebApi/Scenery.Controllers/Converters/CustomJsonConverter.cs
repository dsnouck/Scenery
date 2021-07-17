using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Scenery.Controllers.Converters
{
    public abstract class CustomJsonConverter<T> : JsonConverter<T>
    {
        protected static void ReadStartObject(ref Utf8JsonReader reader)
        {
            if (reader.TokenType != JsonTokenType.StartObject && (!reader.Read() || reader.TokenType != JsonTokenType.StartObject))
            {
                throw new JsonException();
            }
        }

        protected static void ReadPropertyName(ref Utf8JsonReader reader, string propertyName)
        {
            if (!reader.Read() || reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            if (!string.Equals(reader.GetString(), propertyName, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new JsonException();
            }
        }

        protected static double ReadDoubleProperty(ref Utf8JsonReader reader, string propertyName)
        {
            ReadPropertyName(ref reader, propertyName);

            if (!reader.Read() || reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            return reader.GetDouble();
        }

        protected static string ReadStringProperty(ref Utf8JsonReader reader, string propertyName)
        {
            ReadPropertyName(ref reader, propertyName);

            if (!reader.Read() || reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            return reader.GetString();
        }

        protected static void ReadEndObject(ref Utf8JsonReader reader)
        {
            if (!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
            {
                throw new JsonException();
            }
        }

        protected static void WriteStartObject(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
        }

        protected static void WritePropertyName(Utf8JsonWriter writer, string propertyName)
        {
            writer.WritePropertyName(ConvertToCamelCase(propertyName));
        }

        protected static void WriteNumber(Utf8JsonWriter writer, string propertyName, double value)
        {
            writer.WriteNumber(ConvertToCamelCase(propertyName), value);
        }

        protected static void WriteStringInCamelCase(Utf8JsonWriter writer, string propertyName, string value)
        {
            writer.WriteString(ConvertToCamelCase(propertyName), ConvertToCamelCase(value));
        }

        protected static void WriteEndObject(Utf8JsonWriter writer)
        {
            writer.WriteEndObject();

        }

        protected static string ConvertToCamelCase(string value)
        {
            return $"{char.ToLowerInvariant(value[0])}{value[1..]}";
        }

        protected static string ConvertToPascalCase(string value)
        {
            return $"{char.ToUpperInvariant(value[0])}{value[1..]}";
        }
    }
}
