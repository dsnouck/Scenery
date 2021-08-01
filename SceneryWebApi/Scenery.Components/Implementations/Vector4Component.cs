// <copyright file="Vector4Component.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Implementations
{
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using System;

    /// <inheritdoc/>
    public class Vector4Component : IVector4Component
    {
        /// <inheritdoc/>
        public double DotProduct(Vector4 vector, Vector4 otherVector)
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
                (vector.ZCoordinate * otherVector.ZCoordinate) +
                (vector.WCoordinate * otherVector.WCoordinate);
        }
    }
}
