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
                case nameof(ConeScene):
                    scene = new ConeScene();
                    break;
                case nameof(CubeScene):
                    scene = new CubeScene();
                    break;
                case nameof(CylinderScene):
                    scene = new CylinderScene();
                    break;
                case nameof(DodecahedronScene):
                    scene = new DodecahedronScene();
                    break;
                case nameof(EmptyScene):
                    scene = new EmptyScene();
                    break;
                case nameof(FullScene):
                    scene = new FullScene();
                    break;
                case nameof(IcosahedronScene):
                    scene = new IcosahedronScene();
                    break;
                case nameof(IntersectedScene):
                    scene = new IntersectedScene
                    {
                        Scenes = ReadSceneList(ref reader, nameof(IntersectedScene.Scenes)),
                    };
                    break;
                case nameof(InvertedScene):
                    scene = new InvertedScene
                    {
                        OriginalScene = ReadScene(ref reader, nameof(InvertedScene.OriginalScene)),
                    };
                    break;
                case nameof(InvisibleScene):
                    scene = new InvisibleScene
                    {
                        OriginalScene = ReadScene(ref reader, nameof(InvisibleScene.OriginalScene)),
                    };
                    break;
                case nameof(OctahedronScene):
                    scene = new OctahedronScene();
                    break;
                case nameof(PlaneScene):
                    scene = new PlaneScene
                    {
                        Normal = ReadVector(ref reader, nameof(PlaneScene.Normal)),
                    };
                    break;
                case nameof(RotatedScene):
                    scene = new RotatedScene
                    {
                        Axis = ReadVector(ref reader, nameof(RotatedScene.Axis)),
                        Angle = ReadDoubleProperty(ref reader, nameof(RotatedScene.Angle)),
                        OriginalScene = ReadScene(ref reader, nameof(RotatedScene.OriginalScene)),
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
                case nameof(TetrahedronScene):
                    scene = new TetrahedronScene();
                    break;
                case nameof(TranslatedScene):
                    scene = new TranslatedScene
                    {
                        Translation = ReadVector(ref reader, nameof(TranslatedScene.Translation)),
                        OriginalScene = ReadScene(ref reader, nameof(TranslatedScene.OriginalScene)),
                    };
                    break;
                case nameof(UnitedScene):
                    scene = new UnitedScene
                    {
                        Scenes = ReadSceneList(ref reader, nameof(UnitedScene.Scenes)),
                    };
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

        private static Vector3 ReadVector(ref Utf8JsonReader reader, string propertyName)
        {
            ReadPropertyName(ref reader, propertyName);
            ReadStartObject(ref reader);
            var vector = new Vector3
            {
                XCoordinate = ReadDoubleProperty(ref reader, nameof(Vector3.XCoordinate)),
                YCoordinate = ReadDoubleProperty(ref reader, nameof(Vector3.YCoordinate)),
                ZCoordinate = ReadDoubleProperty(ref reader, nameof(Vector3.ZCoordinate)),
            };
            ReadEndObject(ref reader);
            return vector;
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
                case ConeScene:
                case CubeScene:
                case CylinderScene:
                case DodecahedronScene:
                case EmptyScene:
                case FullScene:
                case IcosahedronScene:
                case OctahedronScene:
                case SphereScene:
                case TetrahedronScene:
                    break;
                case ColoredScene colored:
                    WriteColor(writer, nameof(ColoredScene.Color), colored.Color);
                    WriteScene(writer, nameof(ColoredScene.OriginalScene), colored.OriginalScene);
                    break;
                case IntersectedScene intersected:
                    WriteSceneArray(writer, nameof(IntersectedScene.Scenes), intersected.Scenes);
                    break;
                case InvertedScene inverted:
                    WriteScene(writer, nameof(InvertedScene.OriginalScene), inverted.OriginalScene);
                    break;
                case InvisibleScene invisible:
                    WriteScene(writer, nameof(InvisibleScene.OriginalScene), invisible.OriginalScene);
                    break;
                case PlaneScene plane:
                    WriteVector(writer, nameof(PlaneScene.Normal), plane.Normal);
                    break;
                case RotatedScene rotated:
                    WriteVector(writer, nameof(RotatedScene.Axis), rotated.Axis);
                    WriteNumber(writer, nameof(RotatedScene.Angle), rotated.Angle);
                    WriteScene(writer, nameof(RotatedScene.OriginalScene), rotated.OriginalScene);
                    break;
                case ScaledScene scaled:
                    WriteNumber(writer, nameof(ScaledScene.Factor), scaled.Factor);
                    WriteScene(writer, nameof(ScaledScene.OriginalScene), scaled.OriginalScene);
                    break;
                case TranslatedScene translated:
                    WriteVector(writer, nameof(TranslatedScene.Translation), translated.Translation);
                    WriteScene(writer, nameof(TranslatedScene.OriginalScene), translated.OriginalScene);
                    break;
                case UnitedScene united:
                    WriteSceneArray(writer, nameof(UnitedScene.Scenes), united.Scenes);
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
        private static void WriteVector(Utf8JsonWriter writer, string propertyName, Vector3 vector)
        {
            WritePropertyName(writer, propertyName);
            WriteStartObject(writer);
            WriteNumber(writer, nameof(Vector3.XCoordinate), vector.XCoordinate);
            WriteNumber(writer, nameof(Vector3.YCoordinate), vector.YCoordinate);
            WriteNumber(writer, nameof(Vector3.ZCoordinate), vector.ZCoordinate);
            WriteEndObject(writer);
        }
    }
}
