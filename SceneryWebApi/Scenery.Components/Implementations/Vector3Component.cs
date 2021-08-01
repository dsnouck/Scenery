// <copyright file="Vector3Component.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Implementations
{
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using System;

    /// <inheritdoc/>
    public class Vector3Component : IVector3Component
    {
        /// <inheritdoc/>
        public Vector3 Add(Vector3 vector, Vector3 otherVector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (otherVector == null)
            {
                throw new ArgumentNullException(nameof(otherVector));
            }

            return new Vector3
            {
                XCoordinate = vector.XCoordinate + otherVector.XCoordinate,
                YCoordinate = vector.YCoordinate + otherVector.YCoordinate,
                ZCoordinate = vector.ZCoordinate + otherVector.ZCoordinate,
            };
        }

        /// <inheritdoc/>
        public Vector3 CreateVector3FromSphericalCoordinates(double radius, double inclination, double azimuth)
        {
            var sineOfInclination = Math.Sin(inclination);
            var cosineOfInclination = Math.Cos(inclination);
            var sineOfAzimuth = Math.Sin(azimuth);
            var cosineOfAzimuth = Math.Cos(azimuth);

            return new Vector3
            {
                XCoordinate = radius * sineOfInclination * cosineOfAzimuth,
                YCoordinate = radius * sineOfInclination * sineOfAzimuth,
                ZCoordinate = radius * cosineOfInclination,
            };
        }

        /// <inheritdoc/>
        public Vector3 CrossProduct(Vector3 vector, Vector3 otherVector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (otherVector == null)
            {
                throw new ArgumentNullException(nameof(otherVector));
            }

            return new Vector3
            {
                XCoordinate = (vector.YCoordinate * otherVector.ZCoordinate) - (vector.ZCoordinate * otherVector.YCoordinate),
                YCoordinate = (vector.ZCoordinate * otherVector.XCoordinate) - (vector.XCoordinate * otherVector.ZCoordinate),
                ZCoordinate = (vector.XCoordinate * otherVector.YCoordinate) - (vector.YCoordinate * otherVector.XCoordinate),
            };
        }

        /// <inheritdoc/>
        public Vector3 Divide(Vector3 vector, double divisor)
        {
            return this.Multiply(vector, 1D / divisor);
        }

        /// <inheritdoc/>
        public double DotProduct(Vector3 vector, Vector3 otherVector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (otherVector == null)
            {
                throw new ArgumentNullException(nameof(otherVector));
            }

            return
                (vector.XCoordinate * otherVector.XCoordinate) +
                (vector.YCoordinate * otherVector.YCoordinate) +
                (vector.ZCoordinate * otherVector.ZCoordinate);
        }

        /// <inheritdoc/>
        public double GetLength(Vector3 vector)
        {
            return Math.Sqrt(this.DotProduct(vector, vector));
        }

        /// <inheritdoc/>
        public Vector3 Multiply(Vector3 vector, double factor)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            return new Vector3
            {
                XCoordinate = factor * vector.XCoordinate,
                YCoordinate = factor * vector.YCoordinate,
                ZCoordinate = factor * vector.ZCoordinate,
            };
        }

        /// <inheritdoc/>
        public Vector3 Normalize(Vector3 vector)
        {
            return this.Divide(vector, this.GetLength(vector));
        }

        /// <inheritdoc/>
        public Vector3 Subtract(Vector3 vector, Vector3 otherVector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (otherVector == null)
            {
                throw new ArgumentNullException(nameof(otherVector));
            }

            return new Vector3
            {
                XCoordinate = vector.XCoordinate - otherVector.XCoordinate,
                YCoordinate = vector.YCoordinate - otherVector.YCoordinate,
                ZCoordinate = vector.ZCoordinate - otherVector.ZCoordinate,
            };
        }
    }
}
