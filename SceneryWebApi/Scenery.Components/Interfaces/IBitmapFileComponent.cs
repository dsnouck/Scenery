// <copyright file="IBitmapFileComponent.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces
{
    using Scenery.Models;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Provides operations concerning bitmap files.
    /// </summary>
    public interface IBitmapFileComponent
    {
        Stream GetStream(List<List<Color>> bitmap);
    }
}
