// <copyright file="EmptyComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests.SceneComponents;

using FluentAssertions;
using Scenery.Components.Implementations.SceneComponents;
using Scenery.Models;
using Xunit;

/// <summary>
/// Provides tests for <see cref="EmptyComponent"/>.
/// </summary>
public class EmptyComponentTests
{
    private readonly EmptyComponent systemUnderTest;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmptyComponentTests"/> class.
    /// </summary>
    public EmptyComponentTests()
    {
        this.systemUnderTest = new EmptyComponent();
    }

    /// <summary>
    /// Tests <see cref="EmptyComponent.Contains(Vector3)"/>.
    /// </summary>
    [Fact]
    public void GivenAPointWhenContainsIsCalledThenFalseIsReturned()
    {
        // Arrange.
        var point = new Vector3();

        // Act.
        var result = this.systemUnderTest.Contains(point);

        // Assert.
        result.Should().BeFalse();
    }

    /// <summary>
    /// Tests <see cref="EmptyComponent.GetAllIntercepts(Line3)"/>.
    /// </summary>
    [Fact]
    public void GivenALineOfSightWhenGetAllInterceptsIsCalledThenNoInterceptIsReturned()
    {
        // Arrange.
        var lineOfSight = new Line3();

        // Act.
        var result = this.systemUnderTest.GetAllIntercepts(lineOfSight);

        // Assert.
        result.Should().BeEmpty();
    }
}
