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
    /// Calculates an average color.
    /// </summary>
    /// <param name="colors">The colors.</param>
    /// <returns>The average of the colors.</returns>
    Color Average(List<Color> colors);

    /// <summary>
    /// Creates a <see cref="SKColor"/> from a color.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <returns>The <see cref="SKColor"/> created from the color.</returns>
    SKColor CreateSkiaColorFromColor(Color color);

    /// <summary>
    /// Multiplies a color with a factor.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <param name="factor">The factor.</param>
    /// <returns>The color multiplied with the factor.</returns>
    Color Multiply(Color color, double factor);
}
