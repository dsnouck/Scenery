// <copyright file="IColorComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces;

using Scenery.Models;
using SkiaSharp;

/// <summary>
/// Provides operations concerning <see cref="Color"/>s.
/// </summary>
public interface IColorComponent
{
    /// <summary>
    /// Calculates the average of <paramref name="colors"/>.
    /// </summary>
    /// <param name="colors">The colors.</param>
    /// <returns>The average of <paramref name="colors"/>.</returns>
    Color Average(List<Color> colors);

    /// <summary>
    /// Gets the <see cref="SKColor"/> for <paramref name="color"/>.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <returns>The <see cref="SKColor"/> for <paramref name="color"/>.</returns>
    SKColor GetSkiaColorFromColor(Color color);

    /// <summary>
    /// Multiplies <paramref name="color"/> with <paramref name="factor"/>.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <param name="factor">The factor.</param>
    /// <returns><paramref name="color"/> multiplied with <paramref name="factor"/>.</returns>
    Color Multiply(Color color, double factor);
}
