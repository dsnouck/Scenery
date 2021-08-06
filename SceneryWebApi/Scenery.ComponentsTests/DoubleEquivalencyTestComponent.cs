// <copyright file="DoubleEquivalencyTestComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Tests
{
    using System;
    using FluentAssertions;
    using FluentAssertions.Equivalency;

    /// <summary>
    /// Provides double equivalencies.
    /// </summary>
    public class DoubleEquivalencyTestComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleEquivalencyTestComponent"/> class.
        /// </summary>
        public DoubleEquivalencyTestComponent()
        {
            this.Precision = 0.001D;
        }

        /// <summary>
        /// Gets the precision.
        /// </summary>
        public double Precision { get; }

        /// <summary>
        /// Defines a double equivalency for comparing <typeparamref name="TEntity"/>s.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity.</typeparam>
        /// <param name="options">The options.</param>
        /// <returns>A double equivalency for comparing <typeparamref name="TEntity"/>s.</returns>
        public EquivalencyAssertionOptions<TEntity> DoubleEquivalency<TEntity>(EquivalencyAssertionOptions<TEntity> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return options
                .Using<double>(context => context.Subject.Should().BeApproximately(context.Expectation, this.Precision))
                .WhenTypeIs<double>();
        }
    }
}
