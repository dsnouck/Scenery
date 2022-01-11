// <copyright file="Matrix4ComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests;

using FluentAssertions;
using Moq;
using Scenery.Components.Implementations;
using Scenery.Components.Interfaces;
using Scenery.Models;
using Scenery.TestInstrumentation;
using Xunit;

/// <summary>
/// Provides tests for <see cref="Matrix4Component"/>.
/// </summary>
public class Matrix4ComponentTests
{
    private readonly Matrix4Component systemUnderTest;
    private readonly Mock<IVector3Component> vector3ComponentTestDouble;
    private readonly Mock<IVector4Component> vector4ComponentTestDouble;

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix4ComponentTests"/> class.
    /// </summary>
    public Matrix4ComponentTests()
    {
        this.vector3ComponentTestDouble = new Mock<IVector3Component>();
        this.vector4ComponentTestDouble = new Mock<IVector4Component>();
        this.systemUnderTest = new Matrix4Component(
            this.vector3ComponentTestDouble.Object,
            this.vector4ComponentTestDouble.Object);
    }

    /// <summary>
    /// Tests <see cref="Matrix4Component.GetRotationMatrix(Vector3, double)"/>.
    /// </summary>
    [Fact]
    public void GivenAnAxisAndAnAngleWhenGGetRotationMatrixIsCalledThenTheCorrectRotationMatrixIsReturned()
    {
        // Arrange.
        var axis = new Vector3
        {
            X = 0D,
            Y = 0D,
            Z = 1D,
        };
        var angle = Math.PI / 4D;
        this.vector3ComponentTestDouble
            .Setup(component => component.Normalize(It.IsAny<Vector3>()))
            .Returns(axis);

        // Act.
        var result = this.systemUnderTest.GetRotationMatrix(axis, angle);

        // Assert.
        result.Should().BeEquivalentTo(
            new Matrix4
            {
                FirstRow = new Vector4
                {
                    X = Math.Sqrt(2D) / 2D,
                    Y = -Math.Sqrt(2D) / 2D,
                    Z = 0D,
                    W = 0D,
                },
                SecondRow = new Vector4
                {
                    X = Math.Sqrt(2D) / 2D,
                    Y = Math.Sqrt(2D) / 2D,
                    Z = 0D,
                    W = 0D,
                },
                ThirdRow = new Vector4
                {
                    X = 0D,
                    Y = 0D,
                    Z = 1D,
                    W = 0D,
                },
                FourthRow = new Vector4
                {
                    X = 0D,
                    Y = 0D,
                    Z = 0D,
                    W = 1D,
                },
            },
            Equivalencies.DoubleEquivalency);
        this.vector3ComponentTestDouble
            .Verify(component => component.Normalize(It.IsAny<Vector3>()), Times.Once);
    }

    /// <summary>
    /// Tests <see cref="Matrix4Component.GetScalingMatrix(double)"/>.
    /// </summary>
    [Fact]
    public void GivenAFactorWhenGetScalingMatrixIsCalledThenTheCorrectScalingMatrixIsReturned()
    {
        // Arrange.
        var factor = 2D;

        // Act.
        var result = this.systemUnderTest.GetScalingMatrix(factor);

        // Assert.
        result.Should().BeEquivalentTo(
            new Matrix4
            {
                FirstRow = new Vector4
                {
                    X = 2D,
                    Y = 0D,
                    Z = 0D,
                    W = 0D,
                },
                SecondRow = new Vector4
                {
                    X = 0D,
                    Y = 2D,
                    Z = 0D,
                    W = 0D,
                },
                ThirdRow = new Vector4
                {
                    X = 0D,
                    Y = 0D,
                    Z = 2D,
                    W = 0D,
                },
                FourthRow = new Vector4
                {
                    X = 0D,
                    Y = 0D,
                    Z = 0D,
                    W = 1D,
                },
            },
            Equivalencies.DoubleEquivalency);
    }

    /// <summary>
    /// Tests <see cref="Matrix4Component.GetTranslationMatrix(Vector3)"/>.
    /// </summary>
    [Fact]
    public void GivenTheTranslationIsNullWhenGetTranslationMatrixIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        var translation = default(Vector3);

        // Act.
        var action = () => this.systemUnderTest.GetTranslationMatrix(translation);

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="Matrix4Component.GetTranslationMatrix(Vector3)"/>.
    /// </summary>
    [Fact]
    public void GivenATranslationWhenGetTranslationMatrixIsCalledThenTheCorrectTranslationMatrixIsReturned()
    {
        // Arrange.
        var translation = new Vector3
        {
            X = 0D,
            Y = 0D,
            Z = 1D,
        };

        // Act.
        var result = this.systemUnderTest.GetTranslationMatrix(translation);

        // Assert.
        result.Should().BeEquivalentTo(
            new Matrix4
            {
                FirstRow = new Vector4
                {
                    X = 1D,
                    Y = 0D,
                    Z = 0D,
                    W = 0D,
                },
                SecondRow = new Vector4
                {
                    X = 0D,
                    Y = 1D,
                    Z = 0D,
                    W = 0D,
                },
                ThirdRow = new Vector4
                {
                    X = 0D,
                    Y = 0D,
                    Z = 1D,
                    W = 1D,
                },
                FourthRow = new Vector4
                {
                    X = 0D,
                    Y = 0D,
                    Z = 0D,
                    W = 1D,
                },
            },
            Equivalencies.DoubleEquivalency);
    }

    /// <summary>
    /// Tests <see cref="Matrix4Component.Multiply(Matrix4, Vector4)"/>.
    /// </summary>
    [Fact]
    public void GivenTheMatrixIsNullWhenMultiplyIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        var matrix = default(Matrix4);

        // Act.
        var action = () => this.systemUnderTest.Multiply(matrix, new Vector4());

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="Matrix4Component.Multiply(Matrix4, Vector4)"/>.
    /// </summary>
    [Fact]
    public void GivenAMatrixAndAVectorWhenMultiplyIsCalledThenVector4ComponentDotProductIsCalledFourTimes()
    {
        // Arrange.
        var matrix = new Matrix4();
        var vector = new Vector4();

        // Act.
        var result = this.systemUnderTest.Multiply(matrix, vector);

        // Assert.
        this.vector4ComponentTestDouble
            .Verify(component => component.DotProduct(It.IsAny<Vector4>(), It.IsAny<Vector4>()), Times.Exactly(4));
    }
}
