// <copyright file="ColorComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests;

using System;
using System.Collections.Generic;
using FluentAssertions;
using Scenery.Components.Implementations;
using Scenery.Models;
using Scenery.TestInstrumentation;
using Xunit;

/// <summary>
/// Provides tests for <see cref="ColorComponent"/>.
/// </summary>
public class ColorComponentTests
{
    private readonly ColorComponent systemUnderTest;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorComponentTests"/> class.
    /// </summary>
    public ColorComponentTests()
    {
        this.systemUnderTest = new ColorComponent();
    }

    /// <summary>
    /// Tests <see cref="ColorComponent.Average(List{Color})"/>.
    /// </summary>
    [Fact]
    public void GivenAListOfColorsWhenAverageIsCalledThenTheCorrectAverageIsReturned()
    {
        // Arrange.
        var colors = new List<Color>
            {
                new Color
                {
                    R = 0D,
                    G = 0.5D,
                    B = 1D,
                },
                new Color
                {
                    R = 1D,
                    G = 0.5D,
                    B = 0D,
                },
            };

        // Act.
        var result = this.systemUnderTest.Average(colors);

        // Assert.
        result.Should().BeEquivalentTo(
            new Color
            {
                R = 0.5D,
                G = 0.5D,
                B = 0.5D,
            },
            Equivalencies.DoubleEquivalency);
    }

    /// <summary>
    /// Tests <see cref="ColorComponent.GetSystemDrawingColorFromColor(Color)"/>.
    /// </summary>
    [Fact]
    public void GivenTheColorIsNullWhenGetSystemDrawingColorFromColorIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        Color color = null;

        // Act.
        Action action = () => this.systemUnderTest.GetSystemDrawingColorFromColor(color);

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="ColorComponent.GetSystemDrawingColorFromColor(Color)"/>.
    /// </summary>
    [Fact]
    public void GivenAColorWhenGetSystemDrawingColorFromColorIsCalledThenTheCorrectSystemDrawingColorIsReturned()
    {
        // Arrange.
        var color = new Color
        {
            R = 0D,
            G = 0.5D,
            B = 1D,
        };

        // Act.
        var result = this.systemUnderTest.GetSystemDrawingColorFromColor(color);

        // Assert.
        result.Should().BeEquivalentTo(
            System.Drawing.Color.FromArgb(0, 127, 255));
    }

    /// <summary>
    /// Tests <see cref="ColorComponent.Multiply(Color, double)"/>.
    /// </summary>
    [Fact]
    public void GivenTheColorIsNullWhenMultiplyIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        Color color = null;

        // Act.
        Action action = () => this.systemUnderTest.Multiply(color, 0D);

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="ColorComponent.Multiply(Color, double)"/>.
    /// </summary>
    [Fact]
    public void GivenAColorAndAFactorWhenMultiplyIsCalledThenTheProductIsReturned()
    {
        // Arrange.
        var color = new Color
        {
            R = 0D,
            G = 0.5D,
            B = 1D,
        };
        var factor = 0.5D;

        // Act.
        var result = this.systemUnderTest.Multiply(color, factor);

        // Assert.
        result.Should().BeEquivalentTo(
            new Color
            {
                R = 0D,
                G = 0.25D,
                B = 0.5D,
            },
            Equivalencies.DoubleEquivalency);
    }
}
