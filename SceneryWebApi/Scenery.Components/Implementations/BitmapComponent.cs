// <copyright file="BitmapComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations;

using Scenery.Components.Interfaces;
using Scenery.Models;
using SkiaSharp;

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
    public SKBitmap CreateSkiaBitmap(List<List<Color>> bitmap)
    {
        ArgumentNullException.ThrowIfNull(bitmap);

        return new SKBitmap(bitmap.First().Count, bitmap.Count)
        {
            Pixels = bitmap
                .SelectMany(row => row)
                .Select(this.colorComponent.CreateSkiaColorFromColor)
                .ToArray(),
        };
    }
}
