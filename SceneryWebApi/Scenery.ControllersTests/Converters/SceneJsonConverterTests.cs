// <copyright file="SceneJsonConverterTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.ControllersTests.Converters;

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
    }

    private static string CompleteJson => @"{""type"":""intersection"",""scenes"":[{""type"":""union"",""scenes"":[{""type"":""cone""},{""type"":""cube""}]},{""type"":""colored"",""color"":{""r"":1,""g"":0,""b"":0},""scene"":{""type"":""cylinder""}},{""type"":""inverted"",""scene"":{""type"":""dodecahedron""}},{""type"":""transparent"",""scene"":{""type"":""empty""}},{""type"":""rotated"",""axis"":{""x"":0,""y"":0,""z"":1},""angle"":3.141592653589793,""scene"":{""type"":""full""}},{""type"":""scaled"",""factor"":0.5,""scene"":{""type"":""icosahedron""}},{""type"":""translated"",""translation"":{""x"":1,""y"":1,""z"":1},""scene"":{""type"":""octahedron""}},{""type"":""plane"",""normal"":{""x"":1,""y"":1,""z"":1}},{""type"":""sphere""},{""type"":""tetrahedron""}]}";

    private static Intersection CompleteScene => new ()
    {
        Scenes =
                {
                    new Union
                    {
                        Scenes =
                        {
                            new Cone(),
                            new Cube(),
                        },
                    },
                    new Colored
                    {
                        Color = new Color
                        {
                            R = 1D,
                            G = 0D,
                            B = 0D,
                        },
                        Scene = new Cylinder(),
                    },
                    new Inverted
                    {
                        Scene = new Dodecahedron(),
                    },
                    new Transparent
                    {
                        Scene = new Empty(),
                    },
                    new Rotated
                    {
                        Axis = new Vector3
                        {
                            X = 0D,
                            Y = 0D,
                            Z = 1D,
                        },
                        Angle = Math.PI,
                        Scene = new Full(),
                    },
                    new Scaled
                    {
                        Factor = 0.5D,
                        Scene = new Icosahedron(),
                    },
                    new Translated
                    {
                        Translation = new Vector3
                        {
                            X = 1D,
                            Y = 1D,
                            Z = 1D,
                        },
                        Scene = new Octahedron(),
                    },
                    new Plane
                    {
                        Normal = new Vector3
                        {
                            X = 1D,
                            Y = 1D,
                            Z = 1D,
                        },
                    },
                    new Sphere(),
                    new Tetrahedron(),
                },
    };

    /// <summary>
    /// Tests <see cref="SceneJsonConverter.Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>.
    /// </summary>
    [Fact]
    public void GivenACompleteSceneAsJsonWhenDeserializeIsCalledThenItIsCorrectlyDeserialized()
    {
        // Arrange.
        var json = CompleteJson;

        // Act.
        var result = JsonSerializer.Deserialize<Scene>(json, this.jsonSerializerOptions);

        // Assert.
        result.Should().BeEquivalentTo(CompleteScene);
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
        const string json = @"{""type"":""sphere"",""string"":""string""}";

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
        const string json = @"{""type"":""intersection"",""scenes"":""string""}";

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
        const string json = @"{""type"":""scaled"",""factor"":""string""}";

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
        Scene scene = CompleteScene;

        // Act.
        var result = JsonSerializer.Serialize(scene, this.jsonSerializerOptions);

        // Assert.
        result.Should().Be(CompleteJson);
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
