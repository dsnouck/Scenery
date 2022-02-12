// <copyright file="SamplerComponentTests.cs" company="dsnouck">
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
/// Provides tests for <see cref="SamplerComponent"/>.
/// </summary>
public class SamplerComponentTests
{
    private readonly SamplerComponent systemUnderTest;
    private readonly Mock<IColorComponent> colorComponentTestDouble;
    private readonly Mock<IFuncDoubleDoubleComponent> funcDoubleDoubleComponentTestDouble;

    /// <summary>
    /// Initializes a new instance of the <see cref="SamplerComponentTests"/> class.
    /// </summary>
    public SamplerComponentTests()
    {
        this.colorComponentTestDouble = new Mock<IColorComponent>();
        this.funcDoubleDoubleComponentTestDouble = new Mock<IFuncDoubleDoubleComponent>();
        this.systemUnderTest = new SamplerComponent(
            this.colorComponentTestDouble.Object,
            this.funcDoubleDoubleComponentTestDouble.Object);
    }

    /// <summary>
    /// Tests <see cref="SamplerComponent.SampleImageToBitmap(Func{Vector2, Color}, SamplerSettings)"/>.
    /// </summary>
    [Fact]
    public void GivenTheSamplerSettingsIsNullWhenSampleImageToBitmapIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        var samplerSettings = default(SamplerSettings);

        // Act.
        var action = () => this.systemUnderTest.SampleImageToBitmap(vector => new Color(), samplerSettings);

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="SamplerComponent.SampleImageToBitmap(Func{Vector2, Color}, SamplerSettings)"/>.
    /// </summary>
    [Fact]
    public void GivenAnImageAndSamplerSettingsWhenSampleImageToBitmapIsCalledThenTheCorrectBitmapIsReturned()
    {
        // Arrange.
        static Color Image(Vector2 point)
        {
            return new Color();
        }

        var samplerSettings = new SamplerSettings
        {
            Columns = 3,
            Rows = 2,
            Subsamples = 1,
        };
        this.colorComponentTestDouble
            .Setup(component => component.Average(It.IsAny<List<Color>>()))
            .Returns(new Color());
        this.funcDoubleDoubleComponentTestDouble
            .Setup(component => component.CreateLineThrough(It.IsAny<Vector2>(), It.IsAny<Vector2>()))
            .Returns(x => 0D);

        // Act.
        var result = this.systemUnderTest.SampleImageToBitmap(Image, samplerSettings);

        // Assert.
        result.Should().BeEquivalentTo(
            new List<List<Color>>
            {
                    new List<Color>
                    {
                        new Color(),
                        new Color(),
                        new Color(),
                    },
                    new List<Color>
                    {
                        new Color(),
                        new Color(),
                        new Color(),
                    },
            },
            Equivalencies.DoubleEquivalency);
        this.colorComponentTestDouble
            .Verify(component => component.Average(It.IsAny<List<Color>>()), Times.Exactly(6));
        this.funcDoubleDoubleComponentTestDouble
            .Verify(component => component.CreateLineThrough(It.IsAny<Vector2>(), It.IsAny<Vector2>()), Times.Exactly(2));
    }

    /// <summary>
    /// Tests <see cref="SamplerComponent.SampleImageToBitmap(Func{Vector2, Color}, SamplerSettings)"/>.
    /// </summary>
    [Fact]
    public void GivenAnImageAndSamplerSettingsWithSubsamplingWhenSampleImageToBitmapIsCalledThenTheCorrectBitmapIsReturned()
    {
        // Arrange.
        static Color Image(Vector2 point)
        {
            return new Color();
        }

        var samplerSettings = new SamplerSettings
        {
            Columns = 3,
            Rows = 2,
            Subsamples = 2,
        };
        this.colorComponentTestDouble
            .Setup(component => component.Average(It.IsAny<List<Color>>()))
            .Returns(new Color());
        this.funcDoubleDoubleComponentTestDouble
            .Setup(component => component.CreateLineThrough(It.IsAny<Vector2>(), It.IsAny<Vector2>()))
            .Returns(x => 0D);

        // Act.
        var result = this.systemUnderTest.SampleImageToBitmap(Image, samplerSettings);

        // Assert.
        result.Should().BeEquivalentTo(
            new List<List<Color>>
            {
                    new List<Color>
                    {
                        new Color(),
                        new Color(),
                        new Color(),
                    },
                    new List<Color>
                    {
                        new Color(),
                        new Color(),
                        new Color(),
                    },
            },
            Equivalencies.DoubleEquivalency);
        this.colorComponentTestDouble
            .Verify(component => component.Average(It.IsAny<List<Color>>()), Times.Exactly(6));
        this.funcDoubleDoubleComponentTestDouble
            .Verify(component => component.CreateLineThrough(It.IsAny<Vector2>(), It.IsAny<Vector2>()), Times.Exactly(2));
    }
}
