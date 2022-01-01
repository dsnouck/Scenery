// <copyright file="SceneJsonConverter.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using Scenery.Models;
    using Scenery.Models.Scenes;

    /// <inheritdoc/>
    public class SceneJsonConverter : CustomJsonConverter<Scene>
    {
        /// <inheritdoc/>
        public override Scene Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return ReadScene(ref reader);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, Scene value, JsonSerializerOptions options)
        {
            WriteScene(writer, value);
        }

        private static Scene ReadScene(ref Utf8JsonReader reader)
        {
            var scene = new Scene();
            ReadStartObject(ref reader);
            var type = ConvertToPascalCase(ReadStringProperty(ref reader, nameof(Type)));

            switch (type)
            {
                case nameof(Colored):
                    scene = new Colored
                    {
                        Color = ReadColor(ref reader, nameof(Colored.Color)),
                        Scene = ReadScene(ref reader, nameof(Colored.Scene)),
                    };
                    break;
                case nameof(Cone):
                    scene = new Cone();
                    break;
                case nameof(Cube):
                    scene = new Cube();
                    break;
                case nameof(Cylinder):
                    scene = new Cylinder();
                    break;
                case nameof(Dodecahedron):
                    scene = new Dodecahedron();
                    break;
                case nameof(Empty):
                    scene = new Empty();
                    break;
                case nameof(Full):
                    scene = new Full();
                    break;
                case nameof(Icosahedron):
                    scene = new Icosahedron();
                    break;
                case nameof(Intersection):
                    scene = new Intersection
                    {
                        Scenes = ReadSceneList(ref reader, nameof(Intersection.Scenes)),
                    };
                    break;
                case nameof(Inverted):
                    scene = new Inverted
                    {
                        Scene = ReadScene(ref reader, nameof(Inverted.Scene)),
                    };
                    break;
                case nameof(Transparent):
                    scene = new Transparent
                    {
                        Scene = ReadScene(ref reader, nameof(Transparent.Scene)),
                    };
                    break;
                case nameof(Octahedron):
                    scene = new Octahedron();
                    break;
                case nameof(Plane):
                    scene = new Plane
                    {
                        Normal = ReadVector(ref reader, nameof(Plane.Normal)),
                    };
                    break;
                case nameof(Rotated):
                    scene = new Rotated
                    {
                        Axis = ReadVector(ref reader, nameof(Rotated.Axis)),
                        Angle = ReadDoubleProperty(ref reader, nameof(Rotated.Angle)),
                        Scene = ReadScene(ref reader, nameof(Rotated.Scene)),
                    };
                    break;
                case nameof(Scaled):
                    scene = new Scaled
                    {
                        Factor = ReadDoubleProperty(ref reader, nameof(Scaled.Factor)),
                        Scene = ReadScene(ref reader, nameof(Scaled.Scene)),
                    };
                    break;
                case nameof(Sphere):
                    scene = new Sphere();
                    break;
                case nameof(Tetrahedron):
                    scene = new Tetrahedron();
                    break;
                case nameof(Translated):
                    scene = new Translated
                    {
                        Translation = ReadVector(ref reader, nameof(Translated.Translation)),
                        Scene = ReadScene(ref reader, nameof(Translated.Scene)),
                    };
                    break;
                case nameof(Union):
                    scene = new Union
                    {
                        Scenes = ReadSceneList(ref reader, nameof(Union.Scenes)),
                    };
                    break;
            }

            ReadEndObject(ref reader);

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
                R = ReadDoubleProperty(ref reader, nameof(Color.R)),
                G = ReadDoubleProperty(ref reader, nameof(Color.G)),
                B = ReadDoubleProperty(ref reader, nameof(Color.B)),
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
                X = ReadDoubleProperty(ref reader, nameof(Vector3.X)),
                Y = ReadDoubleProperty(ref reader, nameof(Vector3.Y)),
                Z = ReadDoubleProperty(ref reader, nameof(Vector3.Z)),
            };
            ReadEndObject(ref reader);
            return vector;
        }

        private static void WriteScene(Utf8JsonWriter writer, Scene scene)
        {
            WriteStartObject(writer);
            WriteStringInCamelCase(writer, nameof(Type), scene.GetType().Name);

            switch (scene)
            {
                case Cone:
                case Cube:
                case Cylinder:
                case Dodecahedron:
                case Empty:
                case Full:
                case Icosahedron:
                case Octahedron:
                case Sphere:
                case Tetrahedron:
                    break;
                case Colored colored:
                    WriteColor(writer, nameof(Colored.Color), colored.Color);
                    WriteScene(writer, nameof(Colored.Scene), colored.Scene);
                    break;
                case Intersection intersected:
                    WriteSceneArray(writer, nameof(Intersection.Scenes), intersected.Scenes);
                    break;
                case Inverted inverted:
                    WriteScene(writer, nameof(Inverted.Scene), inverted.Scene);
                    break;
                case Transparent transparant:
                    WriteScene(writer, nameof(Transparent.Scene), transparant.Scene);
                    break;
                case Plane plane:
                    WriteVector(writer, nameof(Plane.Normal), plane.Normal);
                    break;
                case Rotated rotated:
                    WriteVector(writer, nameof(Rotated.Axis), rotated.Axis);
                    WriteNumber(writer, nameof(Rotated.Angle), rotated.Angle);
                    WriteScene(writer, nameof(Rotated.Scene), rotated.Scene);
                    break;
                case Scaled scaled:
                    WriteNumber(writer, nameof(Scaled.Factor), scaled.Factor);
                    WriteScene(writer, nameof(Scaled.Scene), scaled.Scene);
                    break;
                case Translated translated:
                    WriteVector(writer, nameof(Translated.Translation), translated.Translation);
                    WriteScene(writer, nameof(Translated.Scene), translated.Scene);
                    break;
                case Union united:
                    WriteSceneArray(writer, nameof(Union.Scenes), united.Scenes);
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
            WriteNumber(writer, nameof(Color.R), color.R);
            WriteNumber(writer, nameof(Color.G), color.G);
            WriteNumber(writer, nameof(Color.B), color.B);
            WriteEndObject(writer);
        }

        private static void WriteVector(Utf8JsonWriter writer, string propertyName, Vector3 vector)
        {
            WritePropertyName(writer, propertyName);
            WriteStartObject(writer);
            WriteNumber(writer, nameof(Vector3.X), vector.X);
            WriteNumber(writer, nameof(Vector3.Y), vector.Y);
            WriteNumber(writer, nameof(Vector3.Z), vector.Z);
            WriteEndObject(writer);
        }
    }
}
