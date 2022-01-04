// <copyright file="Line3ComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests;

using System;
using FluentAssertions;
using Moq;
using Scenery.Components.Implementations;
using Scenery.Components.Interfaces;
using Scenery.Models;
using Xunit;

/// <summary>
/// Provides tests for <see cref="Line3Component"/>.
/// </summary>
public class Line3ComponentTests
{
    private readonly Line3Component systemUnderTest;
    private readonly Mock<IVector3Component> vector3ComponentTestDouble;

    /// <summary>
    /// Initializes a new instance of the <see cref="Line3ComponentTests"/> class.
    /// </summary>
    public Line3ComponentTests()
    {
        this.vector3ComponentTestDouble = new Mock<IVector3Component>();
        this.systemUnderTest = new Line3Component(
            this.vector3ComponentTestDouble.Object);
    }

    /// <summary>
    /// Tests <see cref="Line3Component.GetPointAtDistance(Line3, double)"/>.
    /// </summary>
    [Fact]
    public void GivenTheLineIsNullWhenGetPointAtDistanceIsCalledThenAnArgumentNullExceptionIsThrown()
    {
        // Arrange.
        Line3 line = null;

        // Act.
        Action action = () => this.systemUnderTest.GetPointAtDistance(line, 0D);

        // Assert.
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Tests <see cref="Line3Component.GetPointAtDistance(Line3, double)"/>.
    /// </summary>
    [Fact]
    public void GivenALineAndADistanceWhenGetPointAtDistanceIsCalledThenTheCorrectCalculationsArePerformed()
    {
        // Arrange.
        var line = new Line3();
        var distance = 0D;

        // Act.
        var result = this.systemUnderTest.GetPointAtDistance(line, distance);

        // Assert.
        this.vector3ComponentTestDouble
            .Verify(component => component.Add(It.IsAny<Vector3>(), It.IsAny<Vector3>()), Times.Once);
        this.vector3ComponentTestDouble
            .Verify(component => component.Multiply(It.IsAny<Vector3>(), It.IsAny<double>()), Times.Once);
    }
}
