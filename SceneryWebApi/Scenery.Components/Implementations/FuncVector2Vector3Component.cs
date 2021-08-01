// <copyright file="FuncVector2Vector3Component.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Implementations
{
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using System;

    /// <inheritdoc/>
    public class FuncVector2Vector3Component : IFuncVector2Vector3Component
    {
        private readonly IVector3Component vector3Component;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncVector2Vector3Component"/> class.
        /// </summary>
        /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
        public FuncVector2Vector3Component(
            IVector3Component vector3Component)
        {
            this.vector3Component = vector3Component;
        }

        /// <inheritdoc/>
        public Func<Vector2, Vector3> GetPlane(Vector3 origin, Vector3 xDirection, Vector3 yDirection)
        {
            return parameters => this.vector3Component.Add(
                origin,
                this.vector3Component.Add(
                    this.vector3Component.Multiply(xDirection, parameters.XCoordinate),
                    this.vector3Component.Multiply(yDirection, parameters.YCoordinate)));
        }
    }
}
