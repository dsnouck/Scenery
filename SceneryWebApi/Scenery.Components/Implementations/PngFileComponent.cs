// <copyright file="PngFileComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations;

using Scenery.Components.Interfaces;
using Scenery.Models;
using SkiaSharp;

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
    public Stream GetBitmapFile(List<List<Color>> bitmap)
    {
        var stream = new MemoryStream();
        using var skiaStream = new SKManagedWStream(stream);
        var skiaBitmap = this.bitmapComponent.CreateSkiaBitmap(bitmap);
        skiaBitmap.Encode(skiaStream, SKEncodedImageFormat.Png, 100);
        stream.Position = 0;
        return stream;
    }
}
