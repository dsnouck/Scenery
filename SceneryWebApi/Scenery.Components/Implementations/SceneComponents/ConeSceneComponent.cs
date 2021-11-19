// <copyright file="ConeSceneComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Scenery.Components.Interfaces;
    using Scenery.Components.Interfaces.SceneComponents;
    using Scenery.Models;

    /// <inheritdoc/>
    public class ConeSceneComponent : ISceneComponent
    {
        private readonly IFuncDoubleDoubleComponent funcDoubleDoubleComponent;
        private readonly ILine3Component line3Component;
        private readonly IVector3Component vector3Component;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConeSceneComponent"/> class.
        /// </summary>
        /// <param name="funcDoubleDoubleComponent">An <see cref="IFuncDoubleDoubleComponent"/>.</param>
        /// <param name="line3Component">An <see cref="ILine3Component"/>.</param>
        /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
        public ConeSceneComponent(
            IFuncDoubleDoubleComponent funcDoubleDoubleComponent,
            ILine3Component line3Component,
            IVector3Component vector3Component)
        {
            this.funcDoubleDoubleComponent = funcDoubleDoubleComponent;
            this.line3Component = line3Component;
            this.vector3Component = vector3Component;
        }

        /// <inheritdoc/>
        public bool Contains(Vector3 point)
        {
            if (point == null)
            {
                throw new ArgumentNullException(nameof(point));
            }

            var mirroredPoint = new Vector3
            {
                X = point.X,
                Y = point.Y,
                Z = -point.Z,
            };

            return this.vector3Component.DotProduct(
                point,
                mirroredPoint)
                < 0D;
        }

        /// <inheritdoc/>
        public List<Intercept> GetAllIntercepts(Line3 lineOfSight)
        {
            if (lineOfSight == null)
            {
                throw new ArgumentNullException(nameof(lineOfSight));
            }

            var mirroredLineOfSight = new Line3
            {
                Origin = new Vector3
                {
                    X = lineOfSight.Origin.X,
                    Y = lineOfSight.Origin.Y,
                    Z = -lineOfSight.Origin.Z,
                },
                Direction = new Vector3
                {
                    X = lineOfSight.Direction.X,
                    Y = lineOfSight.Direction.Y,
                    Z = -lineOfSight.Direction.Z,
                },
            };

            // These are the coefficients of the quadratic equation x ↦ ax² + bx + c we want to solve.
            var a = this.vector3Component.DotProduct(
                lineOfSight.Direction,
                mirroredLineOfSight.Direction);
            var b = this.vector3Component.DotProduct(
                lineOfSight.Direction,
                mirroredLineOfSight.Origin)
                * 2D;
            var c = this.vector3Component.DotProduct(
                lineOfSight.Origin,
                mirroredLineOfSight.Origin);

            var zeros = this.funcDoubleDoubleComponent.GetRealZerosOfQuadraticFunction(a, b, c);

            return zeros.
                Select(zero => new Intercept
                {
                    Distance = zero,
                    Normal = () =>
                    {
                        var intercept = this.line3Component.GetPointAtDistance(lineOfSight, zero);
                        var mirroredIntercept = new Vector3
                        {
                            X = intercept.X,
                            Y = intercept.Y,
                            Z = -intercept.Z,
                        };

                        return this.vector3Component.Multiply(
                            this.vector3Component.Normalize(mirroredIntercept),
                            this.vector3Component.GetLength(lineOfSight.Direction));
                    },
                })
                .ToList();
        }
    }
}
