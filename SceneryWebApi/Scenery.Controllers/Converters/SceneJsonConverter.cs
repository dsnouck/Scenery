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
