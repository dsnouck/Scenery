// <copyright file="AffinelyTransformedSceneComponent.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents
{
    using Scenery.Components.Interfaces;
    using Scenery.Components.Interfaces.SceneComponents;
    using Scenery.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <inheritdoc/>
    public class AffinelyTransformedSceneComponent : ISceneComponent
    {
        private readonly IMatrix4Component matrix4Component;
        private readonly ISceneComponent originalSceneComponent;
        private readonly Matrix4 transformation;
        private readonly Matrix4 backwardTransformation;

        /// <summary>
        /// Initializes a new instance of the <see cref="AffinelyTransformedSceneComponent"/> class.
        /// </summary>
        /// <param name="matrix4Component">An <see cref="IMatrix4Component"/>.</param>
        /// <param name="originalSceneComponent">The original <see cref="ISceneComponent"/>.</param>
        /// <param name="transformation">The forward transformation matrix.</param>
        /// <param name="backwardTransformation">The backward transformation matrix.</param>
        public AffinelyTransformedSceneComponent(
            IMatrix4Component matrix4Component,
            ISceneComponent originalSceneComponent,
            Matrix4 transformation,
            Matrix4 backwardTransformation)
        {
            this.matrix4Component = matrix4Component;
            this.originalSceneComponent = originalSceneComponent;
            this.transformation = transformation;
            this.backwardTransformation = backwardTransformation;
        }

        /// <inheritdoc/>
        public bool Contains(Vector3 point)
        {
            if (point == null)
            {
                throw new ArgumentNullException(nameof(point));
            }

            return this.originalSceneComponent.Contains(this.TransformedBackPoint(point));
        }

        /// <inheritdoc/>
        public List<Intercept> GetAllIntercepts(Line3 lineOfSight)
        {
            if (lineOfSight == null)
            {
                throw new ArgumentNullException(nameof(lineOfSight));
            }

            var transformedLineOfSight = new Line3
            {
                Origin = this.TransformedBackPoint(lineOfSight.Origin),
                Direction = this.TransformedBackDirection(lineOfSight.Direction),
            };

            return this.originalSceneComponent.GetAllIntercepts(transformedLineOfSight)
                .Select(intercept => new Intercept
                {
                    Color = intercept.Color,
                    Distance = intercept.Distance,
                    Normal = () => this.TransformedDirection(intercept.Normal()),
                })
                .ToList();
        }

        private Vector3 TransformedDirection(Vector3 direction)
        {
            var direction4 = new Vector4
            {
                XCoordinate = direction.XCoordinate,
                YCoordinate = direction.YCoordinate,
                ZCoordinate = direction.ZCoordinate,
                WCoordinate = 0D,
            };
            var transformedDirection4 = this.matrix4Component.Multiply(this.transformation, direction4);

            return new Vector3
            {
                XCoordinate = transformedDirection4.XCoordinate,
                YCoordinate = transformedDirection4.YCoordinate,
                ZCoordinate = transformedDirection4.ZCoordinate,
            };
        }

        private Vector3 TransformedBackDirection(Vector3 direction)
        {
            var direction4 = new Vector4
            {
                XCoordinate = direction.XCoordinate,
                YCoordinate = direction.YCoordinate,
                ZCoordinate = direction.ZCoordinate,
                WCoordinate = 0D,
            };
            var transformedDirection4 = this.matrix4Component.Multiply(this.backwardTransformation, direction4);

            return new Vector3
            {
                XCoordinate = transformedDirection4.XCoordinate,
                YCoordinate = transformedDirection4.YCoordinate,
                ZCoordinate = transformedDirection4.ZCoordinate,
            };
        }

        private Vector3 TransformedBackPoint(Vector3 point)
        {
            var point4 = new Vector4
            {
                XCoordinate = point.XCoordinate,
                YCoordinate = point.YCoordinate,
                ZCoordinate = point.ZCoordinate,
                WCoordinate = 1D,
            };
            var transformedPoint4 = this.matrix4Component.Multiply(this.backwardTransformation, point4);

            return new Vector3
            {
                XCoordinate = transformedPoint4.XCoordinate,
                YCoordinate = transformedPoint4.YCoordinate,
                ZCoordinate = transformedPoint4.ZCoordinate,
            };
        }
    }
}
