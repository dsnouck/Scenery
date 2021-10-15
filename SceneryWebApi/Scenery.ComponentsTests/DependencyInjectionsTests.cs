// <copyright file="DependencyInjectionsTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests
{
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using Scenery.Components.Implementations;
    using Scenery.Components.Interfaces;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="DependencyInjections"/>.
    /// </summary>
    public class DependencyInjectionsTests
    {
        /// <summary>
        /// Tests <see cref="DependencyInjections.AddComponents(IServiceCollection)"/>.
        /// </summary>
        [Fact]
        public void WhenAddComponentsIsCalledThenImplementationsAreCorrectlyInjected()
        {
            // Act.
            var result = new ServiceCollection()
                .AddComponents()
                .BuildServiceProvider();

            // Assert.
            result.GetService<IColorComponent>().Should().BeOfType<ColorComponent>();
        }
    }
}
