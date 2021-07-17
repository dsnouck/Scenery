using Scenery.Models.Scenes;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Scenery.Controllers.Converters
{
    public class SceneJsonConverter : JsonConverter<Scene>
    {
        public override Scene Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return ReadScene(ref reader);
        }

        private static Scene ReadScene(ref Utf8JsonReader reader)
        {
            ReadStartObject(ref reader);
            var type = ConvertToPascalCase(ReadStringProperty(ref reader, nameof(Type)));
            switch (type)
            {
                case nameof(ScaledScene):
                    var factor = ReadDoubleProperty(ref reader, nameof(ScaledScene.Factor));
                    ReadPropertyName(ref reader, nameof(ScaledScene.OriginalScene));
                    var originalScene = ReadScene(ref reader);
                    ReadEndObject(ref reader);

                    return new ScaledScene
                    {
                        Factor = factor,
                        OriginalScene = originalScene,
                    };
                case nameof(SphereScene):
                    ReadEndObject(ref reader);

                    return new SphereScene();
                default:
                    throw new JsonException();

            }
        }

        private static void ReadStartObject(ref Utf8JsonReader reader)
        {
            if (reader.TokenType != JsonTokenType.StartObject && (!reader.Read() || reader.TokenType != JsonTokenType.StartObject))
            {
                throw new JsonException();
            }
        }

        private static void ReadPropertyName(ref Utf8JsonReader reader, string propertyName)
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

        private static double ReadDoubleProperty(ref Utf8JsonReader reader, string propertyName)
        {
            ReadPropertyName(ref reader, propertyName);

            if (!reader.Read() || reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            return reader.GetDouble();
        }

        private static string ReadStringProperty(ref Utf8JsonReader reader, string propertyName)
        {
            ReadPropertyName(ref reader, propertyName);

            if (!reader.Read() || reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            return reader.GetString();
        }

        private static void ReadEndObject(ref Utf8JsonReader reader)
        {
            if (!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
            {
                throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, Scene value, JsonSerializerOptions options)
        {
            WriteScene(writer, value);
        }

        private static void WriteScene(Utf8JsonWriter writer, Scene scene)
        {
            writer.WriteStartObject();
            switch (scene)
            {
                case ScaledScene scaled:
                    WriteStringInCamelCase(writer, nameof(Type), nameof(ScaledScene));
                    WriteNumber(writer, nameof(ScaledScene.Factor), scaled.Factor);
                    WritePropertyName(writer, nameof(ScaledScene.OriginalScene));
                    WriteScene(writer, scaled.OriginalScene);
                    break;
                case SphereScene sphere:
                    WriteStringInCamelCase(writer, nameof(Type), nameof(SphereScene));
                    break;
                default:
                    throw new JsonException();
            }
            writer.WriteEndObject();
        }

        private static void WritePropertyName(Utf8JsonWriter writer, string propertyName)
        {
            writer.WritePropertyName(ConvertToCamelCase(propertyName));
        }

        private static void WriteNumber(Utf8JsonWriter writer, string propertyName, double value)
        {
            writer.WriteNumber(ConvertToCamelCase(propertyName), value);
        }

        private static void WriteStringInCamelCase(Utf8JsonWriter writer, string propertyName, string value)
        {
            writer.WriteString(ConvertToCamelCase(propertyName), ConvertToCamelCase(value));
        }

        private static string ConvertToCamelCase(string value)
        {
            return $"{char.ToLowerInvariant(value[0])}{value[1..]}";
        }

        private static string ConvertToPascalCase(string value)
        {
            return $"{char.ToUpperInvariant(value[0])}{value[1..]}";
        }
    }
}
