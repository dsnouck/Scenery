// <copyright file="Vector4ComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests;

using FluentAssertions;
using Scenery.Components.Implementations;
using Scenery.Models;
using Scenery.TestInstrumentation;
using Xunit;

/// <summary>
/// Provides tests for <see cref="Vector4Component"/>.
/// </summary>
public class Vector4ComponentTests
{
    private readonly Vector4Component systemUnderTest;

    /// <summary>
    /// Initializes a new instance of the <see cref="Vector4ComponentTests"/> class.
    /// </summary>
    public Vector4ComponentTests()
    {
        this.systemUnderTest = new Vector4Component();
    }

    /// <summary>
    /// Tests <see cref="Vector4Component.DotProduct(Vector4, Vector4)"/>.
    /// </summary>
    [Fact]
    public void GivenTheVectorIsNullWhenDotProductIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        var vector = default(Vector4);

        // Act.
        var action = () => this.systemUnderTest.DotProduct(vector, new Vector4());

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="Vector4Component.DotProduct(Vector4, Vector4)"/>.
    /// </summary>
    [Fact]
    public void GivenTheOtherVectorIsNullWhenDotProductIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        var otherVector = default(Vector4);

        // Act.
        var action = () => this.systemUnderTest.DotProduct(new Vector4(), otherVector);

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="Vector4Component.DotProduct(Vector4, Vector4)"/>.
    /// </summary>
    [Fact]
    public void GivenTwoVectorsWhenDotProductIsCalledThenTheCorrectDotProductIsReturned()
    {
        // Arrange.
        var vector = new Vector4
        {
            X = 1D,
            Y = 2D,
            Z = 3D,
            W = 4D,
        };
        var otherVector = new Vector4
        {
            X = 5D,
            Y = 6D,
            Z = 7D,
            W = 8D,
        };

        // Act.
        var result = this.systemUnderTest.DotProduct(vector, otherVector);

        // Assert.
        result.Should().BeApproximately(70D, Equivalencies.DoublePrecision);
    }
}
