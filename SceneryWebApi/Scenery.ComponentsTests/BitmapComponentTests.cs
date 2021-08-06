// <copyright file="BitmapComponentTests.cs" company="dsnouck">
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
    /// Provides tests for <see cref="BitmapComponent"/>.
    /// </summary>
    public class BitmapComponentTests
    {
        private readonly BitmapComponent systemUnderTest;
        private readonly Mock<IColorComponent> colorComponentTestDouble;

        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapComponentTests"/> class.
        /// </summary>
        public BitmapComponentTests()
        {
            this.colorComponentTestDouble = new Mock<IColorComponent>();
            this.systemUnderTest = new BitmapComponent(
                this.colorComponentTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="BitmapComponent.CreateSystemDrawingBitmap(List{List{Color}})"/>.
        /// </summary>
        [Fact]
        public void GivenTheBitmapIsNullWhenCreateSystemDrawingBitmapIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            Action CreateSystemDrawingBitmap(List<List<Color>> bitmap)
            {
                return () => this.systemUnderTest.CreateSystemDrawingBitmap(bitmap);
            }

            // Act.
            var action = CreateSystemDrawingBitmap(new List<List<Color>>
            {
                new List<Color>
                {
                    new Color(),
                },
            });

            // Assert.
            action.Should().NotThrow();

            // Act.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            action = CreateSystemDrawingBitmap(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="BitmapComponent.CreateSystemDrawingBitmap(List{List{Color}})"/>.
        /// </summary>
        [Fact]
        public void GivenABitmapWhenCreateSystemDrawingBitmapIsCalledThenColorComponentGetSystemDrawingColorFromColorIsCalled()
        {
            // Arrange.
            var bitmap = new List<List<Color>>
            {
                new List<Color>
                {
                    new Color(),
                },
            };

            // Act.
            using var result = this.systemUnderTest.CreateSystemDrawingBitmap(bitmap);

            // Assert.
            this.colorComponentTestDouble
                .Verify(component => component.GetSystemDrawingColorFromColor(It.IsAny<Color>()), Times.Once);
        }
    }
}
