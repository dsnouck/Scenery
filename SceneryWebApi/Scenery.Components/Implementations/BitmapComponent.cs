// <copyright file="BitmapComponent.cs" company="dsnouck">
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
    public class BitmapComponent : IBitmapComponent
    {
        private readonly IColorComponent colorComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapComponent"/> class.
        /// </summary>
        /// <param name="colorComponent">An <see cref="IColorComponent"/>.</param>
        public BitmapComponent(
            IColorComponent colorComponent)
        {
            this.colorComponent = colorComponent;
        }

        /// <inheritdoc/>
        public System.Drawing.Bitmap CreateSystemDrawingBitmap(List<List<Color>> bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            var rowCount = bitmap.Count;
            var columnCount = bitmap.First().Count;

            var systemDrawingBitmap = new System.Drawing.Bitmap(columnCount, rowCount);
            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    systemDrawingBitmap.SetPixel(
                        columnIndex,
                        rowIndex,
                        this.colorComponent.GetSystemDrawingColorFromColor(bitmap[rowIndex][columnIndex]));
                }
            }

            return systemDrawingBitmap;
        }
    }
}
