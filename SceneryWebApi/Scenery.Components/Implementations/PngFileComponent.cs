// <copyright file="PngFileComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations
{
    using System.Collections.Generic;
    using System.IO;
    using Scenery.Components.Interfaces;
    using Scenery.Models;

    /// <inheritdoc/>
    public class PngFileComponent : IBitmapFileComponent
    {
        private readonly IBitmapComponent bitmapComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="PngFileComponent"/> class.
        /// </summary>
        /// <param name="bitmapComponent">An <see cref="IBitmapComponent"/>.</param>
        public PngFileComponent(
            IBitmapComponent bitmapComponent)
        {
            this.bitmapComponent = bitmapComponent;
        }

        /// <inheritdoc/>
        public Stream GetStream(List<List<Color>> bitmap)
        {
            var stream = new MemoryStream();
            using var systemDrawingBitmap = this.bitmapComponent.CreateSystemDrawingBitmap(bitmap);
            systemDrawingBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            stream.Position = 0;
            return stream;
        }
    }
}
