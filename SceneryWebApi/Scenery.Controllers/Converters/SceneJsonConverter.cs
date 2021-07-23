using Scenery.Models.Scenes;
using System;
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
                case nameof(CubeScene):
                    scene = new CubeScene();
                    break;
                case nameof(IntersectedScene):
                    ReadPropertyName(ref reader, nameof(IntersectedScene.Scenes));
                    ReadStartArray(ref reader);
                    var intersected = new IntersectedScene();
                    while (!ReadEndArray(ref reader))
                    {
                        intersected.Scenes.Add(ReadScene(ref reader));
                    }
                    scene = intersected;
                    break;
                case nameof(ScaledScene):
                    var factor = ReadDoubleProperty(ref reader, nameof(ScaledScene.Factor));
                    ReadPropertyName(ref reader, nameof(ScaledScene.OriginalScene));
                    var originalScene = ReadScene(ref reader);
                    scene = new ScaledScene
                    {
                        Factor = factor,
                        OriginalScene = originalScene,
                    };
                    break;
                case nameof(SphereScene):
                    scene = new SphereScene();
                    break;
            }
            ReadEndObject(ref reader);
            return scene;
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
                case IntersectedScene intersected:
                    WritePropertyName(writer, nameof(IntersectedScene.Scenes));
                    WriteStartArray(writer);
                    foreach (var childScene in intersected.Scenes)
                    {
                        WriteScene(writer, childScene);
                    }
                    WriteEndArray(writer);
                    break;
                case ScaledScene scaled:
                    WriteNumber(writer, nameof(ScaledScene.Factor), scaled.Factor);
                    WritePropertyName(writer, nameof(ScaledScene.OriginalScene));
                    WriteScene(writer, scaled.OriginalScene);
                    break;
            }
            WriteEndObject(writer);
        }
    }
}
