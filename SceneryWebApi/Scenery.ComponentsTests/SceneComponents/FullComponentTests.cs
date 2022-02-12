// <copyright file="FullComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests.SceneComponents;

using FluentAssertions;
using Scenery.Components.Implementations.SceneComponents;
using Scenery.Models;
using Xunit;

/// <summary>
/// Provides tests for <see cref="FullComponent"/>.
/// </summary>
public class FullComponentTests
{
    private readonly FullComponent systemUnderTest;

    /// <summary>
    /// Initializes a new instance of the <see cref="FullComponentTests"/> class.
    /// </summary>
    public FullComponentTests()
    {
        this.systemUnderTest = new FullComponent();
    }

    /// <summary>
    /// Tests <see cref="EmptyComponent.Contains(Vector3)"/>.
    /// </summary>
    [Fact]
    public void GivenAPointWhenContainsIsCalledThenTrueIsReturned()
    {
        // Arrange.
        var point = new Vector3();

        // Act.
        var result = this.systemUnderTest.Contains(point);

        // Assert.
        result.Should().BeTrue();
    }

    /// <summary>
    /// Tests <see cref="EmptyComponent.GetAllSurfaceIntersections(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenALineOfSightWhenGetAllSurfaceIntersectionsIsCalledThenNoSurfaceIntersectionIsReturned()
    {
        // Arrange.
        var lineOfSight = new Line3();

        // Act.
        var result = this.systemUnderTest.GetAllSurfaceIntersections(lineOfSight);

        // Assert.
        result.Should().BeEmpty();
    }
}
