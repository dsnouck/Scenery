// <copyright file="BitmapComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations;

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

        var rows = bitmap.Count;
        var columns = bitmap.First().Count;

        var systemDrawingBitmap = new System.Drawing.Bitmap(columns, rows);
        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                systemDrawingBitmap.SetPixel(
                    column,
                    row,
                    this.colorComponent.GetSystemDrawingColorFromColor(bitmap[row][column]));
            }
        }

        return systemDrawingBitmap;
    }
}
