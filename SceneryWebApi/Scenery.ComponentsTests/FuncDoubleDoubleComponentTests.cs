// <copyright file="FuncDoubleDoubleComponentTests.cs" company="dsnouck">
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
/// Provides tests for <see cref="FuncDoubleDoubleComponent"/>.
/// </summary>
public class FuncDoubleDoubleComponentTests
{
    private readonly FuncDoubleDoubleComponent systemUnderTest;

    /// <summary>
    /// Initializes a new instance of the <see cref="FuncDoubleDoubleComponentTests"/> class.
    /// </summary>
    public FuncDoubleDoubleComponentTests()
    {
        this.systemUnderTest = new FuncDoubleDoubleComponent();
    }

    /// <summary>
    /// Tests <see cref="FuncDoubleDoubleComponent.CreateLineThrough(Vector2, Vector2)"/>.
    /// </summary>
    [Fact]
    public void GivenTheVectorIsNullWhenCreateLineThroughIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        var vector = default(Vector2);

        // Act.
        var action = () => this.systemUnderTest.CreateLineThrough(vector, new Vector2());

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="FuncDoubleDoubleComponent.CreateLineThrough(Vector2, Vector2)"/>.
    /// </summary>
    [Fact]
    public void GivenTheOtherVectorIsNullWhenCreateLineThroughIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        var otherVector = default(Vector2);

        // Act.
        var action = () => this.systemUnderTest.CreateLineThrough(new Vector2(), otherVector);

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="FuncDoubleDoubleComponent.CreateLineThrough(Vector2, Vector2)"/>.
    /// </summary>
    [Fact]
    public void GivenTwoPointsWhenCreateLineThroughIsCalledThenTheCorrectLineIsReturned()
    {
        // Arrange.
        var point = new Vector2
        {
            X = 0D,
            Y = 1D,
        };
        var otherPoint = new Vector2
        {
            X = 2D,
            Y = 3D,
        };

        // Act.
        var result = this.systemUnderTest.CreateLineThrough(point, otherPoint);

        // Assert.
        result(5D).Should().BeApproximately(6D, Equivalencies.DoublePrecision);
    }

    /// <summary>
    /// Tests <see cref="FuncDoubleDoubleComponent.GetRealZerosOfQuadraticFunction(double, double, double)"/>.
    /// </summary>
    [Fact]
    public void GivenAParabolaWithVertexBelowTheXAxisWhenGetRealZerosOfQuadraticFunctionIsCalledThenTheCorrectZerosAreReturned()
    {
        // Arrange.
        var a = 1D;
        var b = 0D;
        var c = -1D;

        // Act.
        var result = this.systemUnderTest.GetRealZerosOfQuadraticFunction(a, b, c);

        // Assert.
        result.Should().BeEquivalentTo(
            new List<double>
            {
                    -1D,
                    1D,
            },
            Equivalencies.DoubleEquivalency);
    }

    /// <summary>
    /// Tests <see cref="FuncDoubleDoubleComponent.GetRealZerosOfQuadraticFunction(double, double, double)"/>.
    /// </summary>
    [Fact]
    public void GivenAParabolaWithVertexOnTheXAxisWhenGetRealZerosOfQuadraticFunctionIsCalledThenTheCorrectZerosAreReturned()
    {
        // Arrange.
        var a = 1D;
        var b = 0D;
        var c = 0D;

        // Act.
        var result = this.systemUnderTest.GetRealZerosOfQuadraticFunction(a, b, c);

        // Assert.
        result.Should().BeEquivalentTo(
            new List<double>
            {
                    0D,
                    0D,
            },
            Equivalencies.DoubleEquivalency);
    }

    /// <summary>
    /// Tests <see cref="FuncDoubleDoubleComponent.GetRealZerosOfQuadraticFunction(double, double, double)"/>.
    /// </summary>
    [Fact]
    public void GivenAParabolaWithVertexAboveTheXAxisWhenGetRealZerosOfQuadraticFunctionIsCalledThenTheCorrectZerosAreReturned()
    {
        // Arrange.
        var a = 1D;
        var b = 0D;
        var c = 1D;

        // Act.
        var result = this.systemUnderTest.GetRealZerosOfQuadraticFunction(a, b, c);

        // Assert.
        result.Should().BeEquivalentTo(
            new List<double>(),
            Equivalencies.DoubleEquivalency);
    }
}
