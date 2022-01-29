// <copyright file="BitmapComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests;

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
    /// Tests <see cref="BitmapComponent.CreateSkiaBitmap(List{List{Color}})"/>.
    /// </summary>
    [Fact]
    public void GivenTheBitmapIsNullWhenCreateSkiaBitmapIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        var bitmap = default(List<List<Color>>);

        // Act.
        var action = () => this.systemUnderTest.CreateSkiaBitmap(bitmap);

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="BitmapComponent.CreateSkiaBitmap(List{List{Color}})"/>.
    /// </summary>
    [Fact]
    public void GivenABitmapWhenCreateSkiaBitmapIsCalledThenColorComponentCreateSkiaColorFromColorIsCalled()
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
        using var result = this.systemUnderTest.CreateSkiaBitmap(bitmap);

        // Assert.
        this.colorComponentTestDouble
            .Verify(component => component.CreateSkiaColorFromColor(It.IsAny<Color>()), Times.Once);
    }
}
