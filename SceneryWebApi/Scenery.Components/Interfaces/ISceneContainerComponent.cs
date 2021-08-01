// <copyright file="ISceneContainerComponent.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces
{
    using Scenery.Models;
    using System.IO;

    /// <summary>
    /// Provides operations concerning <see cref="SceneContainer"/>s.
    /// </summary>
    public interface ISceneContainerComponent
    {
        Stream GetStream(SceneContainer sceneContainer);
    }
}
