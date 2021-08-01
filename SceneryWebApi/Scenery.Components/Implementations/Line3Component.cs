﻿// <copyright file="Line3Component.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Implementations
{
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using System;

    /// <inheritdoc/>
    public class Line3Component : ILine3Component
    {
        private readonly IVector3Component vector3Component;

        /// <summary>
        /// Initializes a new instance of the <see cref="Line3Component"/> class.
        /// </summary>
        /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
        public Line3Component(
            IVector3Component vector3Component)
        {
            this.vector3Component = vector3Component;
        }

        /// <inheritdoc/>
        public Vector3 GetPointAtDistance(Line3 line, double distance)
        {
            if (line == null)
            {
                throw new ArgumentNullException(nameof(line));
            }

            return this.vector3Component.Add(
                line.Origin,
                this.vector3Component.Multiply(
                    line.Direction,
                    distance));
        }
    }
}
