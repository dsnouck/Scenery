// <copyright file="FuncDoubleDoubleComponent.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Implementations
{
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using System;
    using System.Collections.Generic;

    /// <inheritdoc/>
    public class FuncDoubleDoubleComponent : IFuncDoubleDoubleComponent
    {
        /// <inheritdoc/>
        public Func<double, double> GetLineThrough(Vector2 point, Vector2 otherPoint)
        {
            if (point == null)
            {
                throw new ArgumentNullException(nameof(point));
            }

            if (otherPoint == null)
            {
                throw new ArgumentNullException(nameof(otherPoint));
            }

            var slope =
                (otherPoint.YCoordinate - point.YCoordinate)
                / (otherPoint.XCoordinate - point.XCoordinate);
            var yIntercept =
                ((point.YCoordinate * otherPoint.XCoordinate) - (point.XCoordinate * otherPoint.YCoordinate))
                / (otherPoint.XCoordinate - point.XCoordinate);

            return xCoordinate => (xCoordinate * slope) + yIntercept;
        }

        /// <inheritdoc/>
        public List<double> GetRealZerosOfQuadraticFunction(double a, double b, double c)
        {
            var discriminant = (b * b) - (4D * a * c);
            if (discriminant < 0D)
            {
                return new List<double>();
            }

            var squareRootOfDiscriminant = Math.Sqrt(discriminant);
            var divisor = 1D / (2D * a);

            return new List<double>
            {
                (-b - squareRootOfDiscriminant) * divisor,
                (-b + squareRootOfDiscriminant) * divisor,
            };
        }
    }
}
