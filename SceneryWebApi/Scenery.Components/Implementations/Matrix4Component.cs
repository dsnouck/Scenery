// <copyright file="Matrix4Component.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Implementations
{
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using System;

    /// <inheritdoc/>
    public class Matrix4Component : IMatrix4Component
    {
        private readonly IVector3Component vector3Component;
        private readonly IVector4Component vector4Component;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4Component"/> class.
        /// </summary>
        /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
        /// <param name="vector4Component">An <see cref="IVector4Component"/>.</param>
        public Matrix4Component(
            IVector3Component vector3Component,
            IVector4Component vector4Component)
        {
            this.vector3Component = vector3Component;
            this.vector4Component = vector4Component;
        }

        /// <inheritdoc/>
        public Matrix4 GetRotationMatrix(Vector3 axis, double angle)
        {
            axis = this.vector3Component.Normalize(axis);
            var l = axis.XCoordinate;
            var m = axis.YCoordinate;
            var n = axis.ZCoordinate;
            var cosine = Math.Cos(angle);
            var sine = Math.Sin(angle);
            var oneMinusCosine = 1D - cosine;

            return new Matrix4
            {
                FirstRow = new Vector4
                {
                    XCoordinate = (l * l * oneMinusCosine) + cosine,
                    YCoordinate = (m * l * oneMinusCosine) - (n * sine),
                    ZCoordinate = (n * l * oneMinusCosine) + (m * sine),
                    WCoordinate = 0D,
                },
                SecondRow = new Vector4
                {
                    XCoordinate = (l * m * oneMinusCosine) + (n * sine),
                    YCoordinate = (m * m * oneMinusCosine) + cosine,
                    ZCoordinate = (n * m * oneMinusCosine) - (l * sine),
                    WCoordinate = 0D,
                },
                ThirdRow = new Vector4
                {
                    XCoordinate = (l * n * oneMinusCosine) - (m * sine),
                    YCoordinate = (m * n * oneMinusCosine) + (l * sine),
                    ZCoordinate = (n * n * oneMinusCosine) + cosine,
                    WCoordinate = 0D,
                },
                FourthRow = new Vector4
                {
                    XCoordinate = 0D,
                    YCoordinate = 0D,
                    ZCoordinate = 0D,
                    WCoordinate = 1D,
                },
            };
        }

        /// <inheritdoc/>
        public Matrix4 GetScalingMatrix(double factor)
        {
            return new Matrix4
            {
                FirstRow = new Vector4
                {
                    XCoordinate = factor,
                    YCoordinate = 0D,
                    ZCoordinate = 0D,
                    WCoordinate = 0D,
                },
                SecondRow = new Vector4
                {
                    XCoordinate = 0D,
                    YCoordinate = factor,
                    ZCoordinate = 0D,
                    WCoordinate = 0D,
                },
                ThirdRow = new Vector4
                {
                    XCoordinate = 0D,
                    YCoordinate = 0D,
                    ZCoordinate = factor,
                    WCoordinate = 0D,
                },
                FourthRow = new Vector4
                {
                    XCoordinate = 0D,
                    YCoordinate = 0D,
                    ZCoordinate = 0D,
                    WCoordinate = 1D,
                },
            };
        }

        /// <inheritdoc/>
        public Matrix4 GetTranslationMatrix(Vector3 translation)
        {
            if (translation == null)
            {
                throw new ArgumentNullException(nameof(translation));
            }

            return new Matrix4
            {
                FirstRow = new Vector4
                {
                    XCoordinate = 1D,
                    YCoordinate = 0D,
                    ZCoordinate = 0D,
                    WCoordinate = translation.XCoordinate,
                },
                SecondRow = new Vector4
                {
                    XCoordinate = 0D,
                    YCoordinate = 1D,
                    ZCoordinate = 0D,
                    WCoordinate = translation.YCoordinate,
                },
                ThirdRow = new Vector4
                {
                    XCoordinate = 0D,
                    YCoordinate = 0D,
                    ZCoordinate = 1D,
                    WCoordinate = translation.ZCoordinate,
                },
                FourthRow = new Vector4
                {
                    XCoordinate = 0D,
                    YCoordinate = 0D,
                    ZCoordinate = 0D,
                    WCoordinate = 1D,
                },
            };
        }

        /// <inheritdoc/>
        public Vector4 Multiply(Matrix4 matrix, Vector4 vector)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            return new Vector4
            {
                XCoordinate = this.vector4Component.DotProduct(matrix.FirstRow, vector),
                YCoordinate = this.vector4Component.DotProduct(matrix.SecondRow, vector),
                ZCoordinate = this.vector4Component.DotProduct(matrix.ThirdRow, vector),
                WCoordinate = this.vector4Component.DotProduct(matrix.FourthRow, vector),
            };
        }
    }
}
