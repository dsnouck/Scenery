// <copyright file="IBitmapComponent.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces
{
    using Scenery.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Provides operations concerning bitmaps.
    /// </summary>
    public interface IBitmapComponent
    {
        /// <summary>
        /// Creates a <see cref="System.Drawing.Bitmap"/> from <paramref name="bitmap"/>.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns>A <see cref="System.Drawing.Bitmap"/> created from <paramref name="bitmap"/>.</returns>
        System.Drawing.Bitmap CreateSystemDrawingBitmap(List<List<Color>> bitmap);
    }
}
