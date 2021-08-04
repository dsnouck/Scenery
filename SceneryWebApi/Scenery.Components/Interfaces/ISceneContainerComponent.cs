// <copyright file="ISceneContainerComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces
{
    using System.IO;
    using Scenery.Models;

    /// <summary>
    /// Provides operations concerning <see cref="SceneContainer"/>s.
    /// </summary>
    public interface ISceneContainerComponent
    {
        /// <summary>
        /// Gets the <paramref name="sceneContainer"/> rendered to a <see cref="Stream"/>.
        /// </summary>
        /// <param name="sceneContainer">The scene container.</param>
        /// <returns>The <paramref name="sceneContainer"/> rendered to a <see cref="Stream"/>.</returns>
        Stream GetStream(SceneContainer sceneContainer);
    }
}
