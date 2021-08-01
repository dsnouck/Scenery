// <copyright file="PngFileComponent.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Implementations
{
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using System.Collections.Generic;
    using System.IO;

    /// <inheritdoc/>
    public class PngFileComponent : IBitmapFileComponent
    {
        private readonly IBitmapComponent bitmapComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="PngFileComponent"/> class.
        /// </summary>
        /// <param name="bitmapComponent">An <see cref="IBitmapComponent"/>.</param>
        /// <param name="fileComponent">An <see cref="IFileComponent"/>.</param>
        public PngFileComponent(
            IBitmapComponent bitmapComponent)
        {
            this.bitmapComponent = bitmapComponent;
        }

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
