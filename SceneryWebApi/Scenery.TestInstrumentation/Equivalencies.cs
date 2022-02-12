// <copyright file="Equivalencies.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.TestInstrumentation;

using FluentAssertions;
using FluentAssertions.Equivalency;

/// <summary>
/// Provides double equivalencies.
/// </summary>
public static class Equivalencies
{
    /// <summary>
    /// Gets the precision used for comparing doubles.
    /// </summary>
    public static double DoublePrecision => 0.001D;

    /// <summary>
    /// Defines a double equivalency for comparing <typeparamref name="TEntity"/>s.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    /// <param name="options">The options.</param>
    /// <returns>A double equivalency for comparing <typeparamref name="TEntity"/>s.</returns>
    public static EquivalencyAssertionOptions<TEntity> DoubleEquivalency<TEntity>(EquivalencyAssertionOptions<TEntity> options)
    {
        return options
            .Using<double>(context => context.Subject.Should().BeApproximately(context.Expectation, DoublePrecision))
            .WhenTypeIs<double>();
    }
}
