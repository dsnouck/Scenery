// <copyright file="IColorComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces;

using Scenery.Models;

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
    /// Gets the <see cref="System.Drawing.Color"/> from <paramref name="color"/>.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <returns>The alpha, red, green and blue bytes from <paramref name="color"/>.</returns>
    System.Drawing.Color GetSystemDrawingColorFromColor(Color color);

    /// <summary>
    /// Multiplies <paramref name="color"/> with <paramref name="factor"/>.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <param name="factor">The factor.</param>
    /// <returns><paramref name="color"/> multiplied with <paramref name="factor"/>.</returns>
    Color Multiply(Color color, double factor);
}
