// <copyright file="SamplerComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Moq;
    using Scenery.Components.Implementations;
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="SamplerComponent"/>.
    /// </summary>
    public class SamplerComponentTests
    {
        private readonly SamplerComponent systemUnderTest;
        private readonly Mock<IColorComponent> colorComponentTestDouble;
        private readonly Mock<IFuncDoubleDoubleComponent> funcDoubleDoubleComponentTestDouble;
        private readonly DoubleEquivalencyTestComponent doubleEquivalencyTestComponent;

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
            this.doubleEquivalencyTestComponent = new DoubleEquivalencyTestComponent();
        }

        /// <summary>
        /// Tests <see cref="SamplerComponent.SampleImageToBitmap(Func{Vector2, Color}, SamplerSettings)"/>.
        /// </summary>
        [Fact]
        public void GivenTheSamplerSettingsIsNullWhenSampleImageToBitmapIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action SampleImageToBitmap(SamplerSettings samplerSettings)
            {
                return () => this.systemUnderTest.SampleImageToBitmap(vector => new Color(), samplerSettings);
            }

            this.funcDoubleDoubleComponentTestDouble
                .Setup(component => component.GetLineThrough(It.IsAny<Vector2>(), It.IsAny<Vector2>()))
                .Returns(xCoordinate => 0D);

            var samplerSettings = new SamplerSettings
            {
                ColumnCount = 1,
                RowCount = 1,
                SubsampleCount = 1,
            };

            // Act.
            var action = SampleImageToBitmap(samplerSettings);

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = SampleImageToBitmap(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

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
                ColumnCount = 3,
                RowCount = 2,
                SubsampleCount = 1,
            };
            this.colorComponentTestDouble
                .Setup(component => component.Average(It.IsAny<List<Color>>()))
                .Returns(new Color());
            this.funcDoubleDoubleComponentTestDouble
                .Setup(component => component.GetLineThrough(It.IsAny<Vector2>(), It.IsAny<Vector2>()))
                .Returns(xCoordinate => 0D);

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
                this.doubleEquivalencyTestComponent.DoubleEquivalency);
            this.colorComponentTestDouble
                .Verify(component => component.Average(It.IsAny<List<Color>>()), Times.Exactly(6));
            this.funcDoubleDoubleComponentTestDouble
                .Verify(component => component.GetLineThrough(It.IsAny<Vector2>(), It.IsAny<Vector2>()), Times.Exactly(2));
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
                ColumnCount = 3,
                RowCount = 2,
                SubsampleCount = 2,
            };
            this.colorComponentTestDouble
                .Setup(component => component.Average(It.IsAny<List<Color>>()))
                .Returns(new Color());
            this.funcDoubleDoubleComponentTestDouble
                .Setup(component => component.GetLineThrough(It.IsAny<Vector2>(), It.IsAny<Vector2>()))
                .Returns(xCoordinate => 0D);

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
                this.doubleEquivalencyTestComponent.DoubleEquivalency);
            this.colorComponentTestDouble
                .Verify(component => component.Average(It.IsAny<List<Color>>()), Times.Exactly(6));
            this.funcDoubleDoubleComponentTestDouble
                .Verify(component => component.GetLineThrough(It.IsAny<Vector2>(), It.IsAny<Vector2>()), Times.Exactly(2));
        }
    }
}
