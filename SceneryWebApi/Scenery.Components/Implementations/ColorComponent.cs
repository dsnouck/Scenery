// <copyright file="ColorComponent.cs" company="dsnouck">
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
    public class ColorComponent : IColorComponent
    {
        /// <inheritdoc/>
        public Color Average(List<Color> colors)
        {
            return new Color
            {
                RedComponent = colors.Average(color => color.RedComponent),
                GreenComponent = colors.Average(color => color.GreenComponent),
                BlueComponent = colors.Average(color => color.BlueComponent),
            };
        }

        /// <inheritdoc/>
        public System.Drawing.Color GetSystemDrawingColorFromColor(Color color)
        {
            if (color == null)
            {
                throw new ArgumentNullException(nameof(color));
            }

            return System.Drawing.Color.FromArgb(
                GetByteFromComponent(color.RedComponent),
                GetByteFromComponent(color.GreenComponent),
                GetByteFromComponent(color.BlueComponent));
        }

        /// <inheritdoc/>
        public List<byte> GetRedGreenBlueBytesFromColor(Color color)
        {
            if (color == null)
            {
                throw new ArgumentNullException(nameof(color));
            }

            var components = new List<double>
            {
                color.RedComponent,
                color.GreenComponent,
                color.BlueComponent,
            };

            return components.Select(GetByteFromComponent).ToList();
        }

        /// <inheritdoc/>
        public Color Multiply(Color color, double factor)
        {
            if (color == null)
            {
                throw new ArgumentNullException(nameof(color));
            }

            return new Color
            {
                RedComponent = color.RedComponent * factor,
                GreenComponent = color.GreenComponent * factor,
                BlueComponent = color.BlueComponent * factor,
            };
        }

        private static byte GetByteFromComponent(double component)
        {
            return (byte)Math.Floor(component * byte.MaxValue);
        }
    }
}
