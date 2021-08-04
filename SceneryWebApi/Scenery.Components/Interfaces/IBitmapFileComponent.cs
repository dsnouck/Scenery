// <copyright file="IBitmapFileComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces
{
    using System.Collections.Generic;
    using System.IO;
    using Scenery.Models;

    /// <summary>
    /// Provides operations concerning bitmap files.
    /// </summary>
    public interface IBitmapFileComponent
    {
        /// <summary>
        /// Gets the bitmap file as a stream.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns>The bitmap file as a stream.</returns>
        Stream GetStream(List<List<Color>> bitmap);
    }
}
