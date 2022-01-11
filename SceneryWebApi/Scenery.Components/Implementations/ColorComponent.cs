// <copyright file="ColorComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations;

using Scenery.Components.Interfaces;
using Scenery.Models;
using SkiaSharp;

/// <inheritdoc/>
public class ColorComponent : IColorComponent
{
    /// <inheritdoc/>
    public Color Average(List<Color> colors)
    {
        return new Color
        {
            R = colors.Average(color => color.R),
            G = colors.Average(color => color.G),
            B = colors.Average(color => color.B),
        };
    }

    /// <inheritdoc/>
    public SKColor GetSkiaColorFromColor(Color color)
    {
        ArgumentNullException.ThrowIfNull(color);

        return new SKColor(
            GetByteFromComponent(color.R),
            GetByteFromComponent(color.G),
            GetByteFromComponent(color.B));
    }

    /// <inheritdoc/>
    public Color Multiply(Color color, double factor)
    {
        ArgumentNullException.ThrowIfNull(color);

        return new Color
        {
            R = color.R * factor,
            G = color.G * factor,
            B = color.B * factor,
        };
    }

    private static byte GetByteFromComponent(double component)
    {
        return (byte)Math.Floor(component * byte.MaxValue);
    }
}
