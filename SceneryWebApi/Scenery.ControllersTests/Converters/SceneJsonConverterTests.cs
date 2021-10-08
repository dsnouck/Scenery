﻿// <copyright file="SceneJsonConverterTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.ControllersTests.Converters
{
    using System;
    using System.Text.Json;
    using FluentAssertions;
    using Scenery.Controllers.Converters;
    using Scenery.Models;
    using Scenery.Models.Scenes;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="SceneJsonConverter"/>.
    /// </summary>
    public class SceneJsonConverterTests
    {
        private readonly JsonSerializerOptions jsonSerializerOptions;
        private readonly IntersectedScene completeScene;
        private readonly string completeJson;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneJsonConverterTests"/> class.
        /// </summary>
        public SceneJsonConverterTests()
        {
            this.jsonSerializerOptions = new JsonSerializerOptions
            {
                Converters =
                {
                    new SceneJsonConverter(),
                },
            };
            this.completeScene = new IntersectedScene
            {
                Scenes =
                    {
                        new UnitedScene
                        {
                            Scenes =
                            {
                                new ConeScene(),
                                new CubeScene(),
                            },
                        },
                        new ColoredScene
                        {
                            Color = new Color
                            {
                                RedComponent = 1D,
                                GreenComponent = 0D,
                                BlueComponent = 0D,
                            },
                            OriginalScene = new CylinderScene(),
                        },
                        new InvertedScene
                        {
                            OriginalScene = new DodecahedronScene(),
                        },
                        new InvisibleScene
                        {
                            OriginalScene = new EmptyScene(),
                        },
                        new RotatedScene
                        {
                            Axis = new Vector3
                            {
                                XCoordinate = 0D,
                                YCoordinate = 0D,
                                ZCoordinate = 1D,
                            },
                            Angle = Math.PI,
                            OriginalScene = new FullScene(),
                        },
                        new ScaledScene
                        {
                            Factor = 0.5D,
                            OriginalScene = new IcosahedronScene(),
                        },
                        new TranslatedScene
                        {
                            Translation = new Vector3
                            {
                                XCoordinate = 1D,
                                YCoordinate = 1D,
                                ZCoordinate = 1D,
                            },
                            OriginalScene = new OctahedronScene(),
                        },
                        new PlaneScene
                        {
                            Normal = new Vector3
                            {
                                XCoordinate = 1D,
                                YCoordinate = 1D,
                                ZCoordinate = 1D,
                            },
                        },
                        new SphereScene(),
                        new TetrahedronScene(),
                    },
            };
            this.completeJson = @"{""type"":""intersectedScene"",""scenes"":[{""type"":""unitedScene"",""scenes"":[{""type"":""coneScene""},{""type"":""cubeScene""}]},{""type"":""coloredScene"",""color"":{""redComponent"":1,""greenComponent"":0,""blueComponent"":0},""originalScene"":{""type"":""cylinderScene""}},{""type"":""invertedScene"",""originalScene"":{""type"":""dodecahedronScene""}},{""type"":""invisibleScene"",""originalScene"":{""type"":""emptyScene""}},{""type"":""rotatedScene"",""axis"":{""xCoordinate"":0,""yCoordinate"":0,""zCoordinate"":1},""angle"":3.141592653589793,""originalScene"":{""type"":""fullScene""}},{""type"":""scaledScene"",""factor"":0.5,""originalScene"":{""type"":""icosahedronScene""}},{""type"":""translatedScene"",""translation"":{""xCoordinate"":1,""yCoordinate"":1,""zCoordinate"":1},""originalScene"":{""type"":""octahedronScene""}},{""type"":""planeScene"",""normal"":{""xCoordinate"":1,""yCoordinate"":1,""zCoordinate"":1}},{""type"":""sphereScene""},{""type"":""tetrahedronScene""}]}";
        }

        /// <summary>
        /// Tests <see cref="SceneJsonConverter.Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>.
        /// </summary>
        [Fact]
        public void GivenACompleteSceneAsJsonWhenDeserializeIsCalledThenItIsCorrectlyDeserialized()
        {
            // Arrange.
            var json = this.completeJson;

            // Act.
            var result = JsonSerializer.Deserialize<Scene>(json, this.jsonSerializerOptions);

            // Assert.
            result.Should().BeEquivalentTo(this.completeScene);
        }

        /// <summary>
        /// Tests <see cref="SceneJsonConverter.Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>.
        /// </summary>
        [Fact]
        public void GivenASceneAsJsonWhenDeserializeIsCalledThenAJsonExceptionIsThrown()
        {
            // Arrange.
            const string json = @"{""type"":""scene""}";

            // Act.
            Action action = () => JsonSerializer.Deserialize<Scene>(json, this.jsonSerializerOptions);

            // Assert.
            action.Should().Throw<JsonException>();
        }

        /// <summary>
        /// Tests <see cref="SceneJsonConverter.Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>.
        /// </summary>
        [Fact]
        public void GivenAMissingStartObjectWhenDeserializeIsCalledThenAJsonExceptionIsThrown()
        {
            // Arrange.
            const string json = @"""string""";

            // Act.
            Action action = () => JsonSerializer.Deserialize<Scene>(json, this.jsonSerializerOptions);

            // Assert.
            action.Should().Throw<JsonException>();
        }

        /// <summary>
        /// Tests <see cref="SceneJsonConverter.Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>.
        /// </summary>
        [Fact]
        public void GivenAMissingEndObjectWhenDeserializeIsCalledThenAJsonExceptionIsThrown()
        {
            // Arrange.
            const string json = @"{""type"":""sphereScene"",""string"":""string""}";

            // Act.
            Action action = () => JsonSerializer.Deserialize<Scene>(json, this.jsonSerializerOptions);

            // Assert.
            action.Should().Throw<JsonException>();
        }

        /// <summary>
        /// Tests <see cref="SceneJsonConverter.Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>.
        /// </summary>
        [Fact]
        public void GivenAMissingStartArrayWhenDeserializeIsCalledThenAJsonExceptionIsThrown()
        {
            // Arrange.
            const string json = @"{""type"":""intersectedScene"",""scenes"":""string""}";

            // Act.
            Action action = () => JsonSerializer.Deserialize<Scene>(json, this.jsonSerializerOptions);

            // Assert.
            action.Should().Throw<JsonException>();
        }

        /// <summary>
        /// Tests <see cref="SceneJsonConverter.Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>.
        /// </summary>
        [Fact]
        public void GivenAMissingPropertyWhenDeserializeIsCalledThenAJsonExceptionIsThrown()
        {
            // Arrange.
            const string json = @"{}";

            // Act.
            Action action = () => JsonSerializer.Deserialize<Scene>(json, this.jsonSerializerOptions);

            // Assert.
            action.Should().Throw<JsonException>();
        }

        /// <summary>
        /// Tests <see cref="SceneJsonConverter.Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>.
        /// </summary>
        [Fact]
        public void GivenAnUnexpectedPropertyWhenDeserializeIsCalledThenAJsonExceptionIsThrown()
        {
            // Arrange.
            const string json = @"{""string"":""string""}";

            // Act.
            Action action = () => JsonSerializer.Deserialize<Scene>(json, this.jsonSerializerOptions);

            // Assert.
            action.Should().Throw<JsonException>();
        }

        /// <summary>
        /// Tests <see cref="SceneJsonConverter.Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>.
        /// </summary>
        [Fact]
        public void GivenAMissingNumberWhenDeserializeIsCalledThenAJsonExceptionIsThrown()
        {
            // Arrange.
            const string json = @"{""type"":""scaledScene"",""factor"":""string""}";

            // Act.
            Action action = () => JsonSerializer.Deserialize<Scene>(json, this.jsonSerializerOptions);

            // Assert.
            action.Should().Throw<JsonException>();
        }

        /// <summary>
        /// Tests <see cref="SceneJsonConverter.Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>.
        /// </summary>
        [Fact]
        public void GivenAMissingStringWhenDeserializeIsCalledThenAJsonExceptionIsThrown()
        {
            // Arrange.
            const string json = @"{""type"":0}";

            // Act.
            Action action = () => JsonSerializer.Deserialize<Scene>(json, this.jsonSerializerOptions);

            // Assert.
            action.Should().Throw<JsonException>();
        }

        /// <summary>
        /// Tests <see cref="SceneJsonConverter.Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>.
        /// </summary>
        [Fact]
        public void GivenANullStringWhenDeserializeIsCalledThenAJsonExceptionIsThrown()
        {
            // Arrange.
            const string json = @"{""type"":null}";

            // Act.
            Action action = () => JsonSerializer.Deserialize<Scene>(json, this.jsonSerializerOptions);

            // Assert.
            action.Should().Throw<JsonException>();
        }

        /// <summary>
        /// Tests <see cref="SceneJsonConverter.Write(Utf8JsonWriter, Scene, JsonSerializerOptions)"/>.
        /// </summary>
        [Fact]
        public void GivenACompleteSceneWhenSerializeIsCalledThenItIsCorrectlySerialized()
        {
            // Arrange.
            var scene = (Scene)this.completeScene;

            // Act.
            var result = JsonSerializer.Serialize(scene, this.jsonSerializerOptions);

            // Assert.
            result.Should().Be(this.completeJson);
        }

        /// <summary>
        /// Tests <see cref="SceneJsonConverter.Write(Utf8JsonWriter, Scene, JsonSerializerOptions)"/>.
        /// </summary>
        [Fact]
        public void GivenASceneWhenSerializeIsCalledThenAJsonExceptionIsThrown()
        {
            // Arrange.
            var scene = new Scene();

            // Act.
            Action action = () => JsonSerializer.Serialize(scene, this.jsonSerializerOptions);

            // Assert.
            action.Should().Throw<JsonException>();
        }
    }
}
