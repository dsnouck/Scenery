﻿// <copyright file="IBitmapComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces;

using Scenery.Models;
using SkiaSharp;

/// <summary>
/// Provides operations concerning bitmaps.
/// </summary>
public interface IBitmapComponent
{
    /// <summary>
    /// Creates a <see cref="SKBitmap"/> from a bitmap.
    /// </summary>
    /// <param name="bitmap">The bitmap.</param>
    /// <returns>The <see cref="SKBitmap"/> created from the bitmap.</returns>
    SKBitmap CreateSkiaBitmap(List<List<Color>> bitmap);
}
