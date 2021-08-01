// <copyright file="SamplerComponent.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Implementations
{
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

            var columnIndexToXCoordinate = this.GetColumnIndexToXCoordinate(samplerSettings);
            var rowIndexToYCoordinate = this.GetRowIndexToYCoordinate(samplerSettings);

            return Enumerable.Range(0, samplerSettings.RowCount)
                .AsParallel()
                .AsOrdered()
                .Select(rowIndex => Enumerable.Range(0, samplerSettings.ColumnCount)
                    .Select(columnIndex => this.colorComponent.Average(
                        Enumerable.Range(0, samplerSettings.SubsampleCount)
                        .Select(subrowIndex => rowIndexToYCoordinate((rowIndex * samplerSettings.SubsampleCount) + subrowIndex))
                        .SelectMany(yCoordinate => Enumerable.Range(0, samplerSettings.SubsampleCount)
                            .Select(subcolumnIndex => columnIndexToXCoordinate((columnIndex * samplerSettings.SubsampleCount) + subcolumnIndex))
                            .Select(xCoordinate => new Vector2 { XCoordinate = xCoordinate, YCoordinate = yCoordinate })
                            .Select(image))
                        .ToList()))
                    .ToList())
                .ToList();
        }

        private Func<double, double> GetColumnIndexToXCoordinate(SamplerSettings samplerSettings)
        {
            // The interval [-1D, 1D] is divided into samplerSettings.ColumnCount * samplerSettings.SubsampleCount equal subintervals.
            // The xCoordinate for a columnIndex is the center of the subinterval.
            // The columns are counted from left to right.
            // The xCoordinate runs from left to right.
            return this.funcDoubleDoubleComponent.GetLineThrough(
                new Vector2 { XCoordinate = -0.5D, YCoordinate = -1D },
                new Vector2 { XCoordinate = (samplerSettings.ColumnCount * samplerSettings.SubsampleCount) - 0.5D, YCoordinate = 1D });
        }

        private Func<double, double> GetRowIndexToYCoordinate(SamplerSettings samplerSettings)
        {
            var aspectRatio = (double)samplerSettings.RowCount / samplerSettings.ColumnCount;

            // The interval [-aspectRatio, aspectRatio] is divided into samplerSettings.RowCount * samplerSettings.SubsampleCount equal subintervals.
            // The yCoordinate for a rowIndex is the center of the subinterval.
            // The rows are counted from top to bottom.
            // The yCoordinate runs from bottom to top.
            return this.funcDoubleDoubleComponent.GetLineThrough(
                new Vector2 { XCoordinate = -0.5D, YCoordinate = aspectRatio },
                new Vector2 { XCoordinate = (samplerSettings.RowCount * samplerSettings.SubsampleCount) - 0.5D, YCoordinate = -aspectRatio });
        }
    }
}
