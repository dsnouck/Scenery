using Scenery.Models;
using Scenery.Models.Scenes;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Scenery.Controllers.Converters
{
    public class SceneJsonConverter : CustomJsonConverter<Scene>
    {
        public override Scene Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return ReadScene(ref reader);
        }

        private static Scene ReadScene(ref Utf8JsonReader reader)
        {
            var scene = new Scene();
            ReadStartObject(ref reader);
            var type = ConvertToPascalCase(ReadStringProperty(ref reader, nameof(Type)));

            switch (type)
            {
                case nameof(ColoredScene):
                    scene = new ColoredScene
                    {
                        Color = ReadColor(ref reader, nameof(ColoredScene.Color)),
                        OriginalScene = ReadScene(ref reader, nameof(ColoredScene.OriginalScene)),
                    };
                    break;
                case nameof(CubeScene):
                    scene = new CubeScene();
                    break;
                case nameof(IntersectedScene):
                    scene = new IntersectedScene
                    {
                        Scenes = ReadSceneList(ref reader, nameof(IntersectedScene.Scenes)),
                    };
                    break;
                case nameof(ScaledScene):
                    scene = new ScaledScene
                    {
                        Factor = ReadDoubleProperty(ref reader, nameof(ScaledScene.Factor)),
                        OriginalScene = ReadScene(ref reader, nameof(ScaledScene.OriginalScene)),
                    };
                    break;
                case nameof(SphereScene):
                    scene = new SphereScene();
                    break;
            }

            ReadEndObject(ref reader);

            if (scene.GetType() == typeof(Scene))
            {
                throw new JsonException();
            }

            return scene;
        }

        private static Scene ReadScene(ref Utf8JsonReader reader, string propertyName)
        {
            ReadPropertyName(ref reader, propertyName);
            return ReadScene(ref reader);
        }

        private static List<Scene> ReadSceneList(ref Utf8JsonReader reader, string propertyName)
        {
            var scenes = new List<Scene>();
            ReadPropertyName(ref reader, propertyName);
            ReadStartArray(ref reader);

            while (!ReadEndArray(ref reader))
            {
                scenes.Add(ReadScene(ref reader));
            }

            return scenes;
        }

        private static Color ReadColor(ref Utf8JsonReader reader, string propertyName)
        {
            ReadPropertyName(ref reader, propertyName);
            ReadStartObject(ref reader);
            var color = new Color
            {
                RedComponent = ReadDoubleProperty(ref reader, nameof(Color.RedComponent)),
                GreenComponent = ReadDoubleProperty(ref reader, nameof(Color.GreenComponent)),
                BlueComponent = ReadDoubleProperty(ref reader, nameof(Color.BlueComponent)),
            };
            ReadEndObject(ref reader);
            return color;
        }

        public override void Write(Utf8JsonWriter writer, Scene value, JsonSerializerOptions options)
        {
            WriteScene(writer, value);
        }

        private static void WriteScene(Utf8JsonWriter writer, Scene scene)
        {
            WriteStartObject(writer);
            WriteStringInCamelCase(writer, nameof(Type), scene.GetType().Name);

            switch (scene)
            {
                case ColoredScene colored:
                    WriteColor(writer, nameof(ColoredScene.Color), colored.Color);
                    WriteScene(writer, nameof(ColoredScene.OriginalScene), colored.OriginalScene);
                    break;
                case IntersectedScene intersected:
                    WriteSceneArray(writer, nameof(IntersectedScene.Scenes), intersected.Scenes);
                    break;
                case ScaledScene scaled:
                    WriteNumber(writer, nameof(ScaledScene.Factor), scaled.Factor);
                    WriteScene(writer, nameof(ScaledScene.OriginalScene), scaled.OriginalScene);
                    break;
                case CubeScene:
                case SphereScene:
                    break;
                default:
                    throw new JsonException();
            }

            WriteEndObject(writer);
        }

        private static void WriteScene(Utf8JsonWriter writer, string propertyName, Scene scene)
        {
            WritePropertyName(writer, propertyName);
            WriteScene(writer, scene);
        }

        private static void WriteSceneArray(Utf8JsonWriter writer, string propertyName, List<Scene> scenes)
        {
            WritePropertyName(writer, propertyName);
            WriteStartArray(writer);
            foreach (var scene in scenes)
            {
                WriteScene(writer, scene);
            }
            WriteEndArray(writer);
        }

        private static void WriteColor(Utf8JsonWriter writer, string propertyName, Color color)
        {
            WritePropertyName(writer, propertyName);
            WriteStartObject(writer);
            WriteNumber(writer, nameof(Color.RedComponent), color.RedComponent);
            WriteNumber(writer, nameof(Color.GreenComponent), color.GreenComponent);
            WriteNumber(writer, nameof(Color.BlueComponent), color.BlueComponent);
            WriteEndObject(writer);
        }
    }
}
