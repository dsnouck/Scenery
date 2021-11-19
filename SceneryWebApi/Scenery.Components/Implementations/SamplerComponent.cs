// <copyright file="SamplerComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Scenery.Components.Interfaces;
    using Scenery.Models;

    /// <inheritdoc/>
    public class SamplerComponent : ISamplerComponent
    {
        private readonly IColorComponent colorComponent;
        private readonly IFuncDoubleDoubleComponent funcDoubleDoubleComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplerComponent"/> class.
        /// </summary>
        /// <param name="colorComponent">An <see cref="IColorComponent"/>.</param>
        /// <param name="funcDoubleDoubleComponent">An <see cref="IFuncDoubleDoubleComponent"/>.</param>
        public SamplerComponent(
            IColorComponent colorComponent,
            IFuncDoubleDoubleComponent funcDoubleDoubleComponent)
        {
            this.colorComponent = colorComponent;
            this.funcDoubleDoubleComponent = funcDoubleDoubleComponent;
        }

        /// <inheritdoc/>
        public List<List<Color>> SampleImageToBitmap(Func<Vector2, Color> image, SamplerSettings samplerSettings)
        {
            if (samplerSettings == null)
            {
                throw new ArgumentNullException(nameof(samplerSettings));
            }

            var columnToX = this.GetColumnToX(samplerSettings);
            var rowToY = this.GetRowToY(samplerSettings);

            return Enumerable.Range(0, samplerSettings.Rows)
                .AsParallel()
                .AsOrdered()
                .Select(row => Enumerable.Range(0, samplerSettings.Columns)
                    .Select(column => this.colorComponent.Average(
                        Enumerable.Range(0, samplerSettings.Subsamples)
                        .Select(subrow => rowToY((row * samplerSettings.Subsamples) + subrow))
                        .SelectMany(y => Enumerable.Range(0, samplerSettings.Subsamples)
                            .Select(subcolumn => columnToX((column * samplerSettings.Subsamples) + subcolumn))
                            .Select(x => new Vector2 { X = x, Y = y })
                            .Select(image))
                        .ToList()))
                    .ToList())
                .ToList();
        }

        private Func<double, double> GetColumnToX(SamplerSettings samplerSettings)
        {
            // The interval [-1D, 1D] is divided into samplerSettings.Columns * samplerSettings.Subsamples equal subintervals.
            // The x-component for a column is the center of the subinterval.
            // The columns are counted from left to right.
            // The x-component runs from left to right.
            return this.funcDoubleDoubleComponent.GetLineThrough(
                new Vector2 { X = -0.5D, Y = -1D },
                new Vector2 { X = (samplerSettings.Columns * samplerSettings.Subsamples) - 0.5D, Y = 1D });
        }

        private Func<double, double> GetRowToY(SamplerSettings samplerSettings)
        {
            var aspectRatio = (double)samplerSettings.Rows / samplerSettings.Columns;

            // The interval [-aspectRatio, aspectRatio] is divided into samplerSettings.Rows * samplerSettings.Subsamples equal subintervals.
            // The y-component for a row is the center of the subinterval.
            // The rows are counted from top to bottom.
            // The y-component runs from bottom to top.
            return this.funcDoubleDoubleComponent.GetLineThrough(
                new Vector2 { X = -0.5D, Y = aspectRatio },
                new Vector2 { X = (samplerSettings.Rows * samplerSettings.Subsamples) - 0.5D, Y = -aspectRatio });
        }
    }
}
