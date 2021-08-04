﻿// <copyright file="CylinderSceneComponent.cs" company="dsnouck">
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
    public class CylinderSceneComponent : ISceneComponent
    {
        private readonly IFuncDoubleDoubleComponent funcDoubleDoubleComponent;
        private readonly ILine3Component line3Component;
        private readonly IVector3Component vector3Component;

        /// <summary>
        /// Initializes a new instance of the <see cref="CylinderSceneComponent"/> class.
        /// </summary>
        /// <param name="funcDoubleDoubleComponent">An <see cref="IFuncDoubleDoubleComponent"/>.</param>
        /// <param name="line3Component">An <see cref="ILine3Component"/>.</param>
        /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
        public CylinderSceneComponent(
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

            var horizontalPoint = new Vector3
            {
                XCoordinate = point.XCoordinate,
                YCoordinate = point.YCoordinate,
                ZCoordinate = 0D,
            };

            return this.vector3Component.DotProduct(horizontalPoint, horizontalPoint) <= 1D;
        }

        /// <inheritdoc/>
        public List<Intercept> GetAllIntercepts(Line3 lineOfSight)
        {
            if (lineOfSight == null)
            {
                throw new ArgumentNullException(nameof(lineOfSight));
            }

            var horizontalLineOfSight = new Line3
            {
                Origin = new Vector3
                {
                    XCoordinate = lineOfSight.Origin.XCoordinate,
                    YCoordinate = lineOfSight.Origin.YCoordinate,
                    ZCoordinate = 0D,
                },
                Direction = new Vector3
                {
                    XCoordinate = lineOfSight.Direction.XCoordinate,
                    YCoordinate = lineOfSight.Direction.YCoordinate,
                    ZCoordinate = 0D,
                },
            };

            // These are the coefficients of the quadratic equation x ↦ ax² + bx + c we want to solve.
            var a = this.vector3Component.DotProduct(
                horizontalLineOfSight.Direction,
                horizontalLineOfSight.Direction);
            var b = this.vector3Component.DotProduct(
                horizontalLineOfSight.Direction,
                horizontalLineOfSight.Origin)
                * 2D;
            var c = this.vector3Component.DotProduct(
                horizontalLineOfSight.Origin,
                horizontalLineOfSight.Origin)
                - 1D;

            var zeros = this.funcDoubleDoubleComponent.GetRealZerosOfQuadraticFunction(a, b, c);

            return zeros
                .Select(zero => new Intercept
                {
                    Distance = zero,
                    Normal = () =>
                    {
                        var intercept = this.line3Component.GetPointAtDistance(lineOfSight, zero);
                        var horizontalIntercept = new Vector3
                        {
                            XCoordinate = intercept.XCoordinate,
                            YCoordinate = intercept.YCoordinate,
                            ZCoordinate = 0D,
                        };

                        return this.vector3Component.Multiply(
                            this.vector3Component.Normalize(horizontalIntercept),
                            this.vector3Component.GetLength(lineOfSight.Direction));
                    },
                })
                .ToList();
        }
    }
}
