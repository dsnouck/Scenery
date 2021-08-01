// <copyright file="ISceneComponentFactory.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces.SceneComponents
{
    using Scenery.Models.Scenes;

    /// <summary>
    /// Provides factory operations concerning <see cref="ISceneComponent"/>s.
    /// </summary>
    public interface ISceneComponentFactory
    {
        /// <summary>
        /// Creates an <see cref="ISceneComponent"/> from <paramref name="scene"/>.
        /// </summary>
        /// <param name="scene">The scene.</param>
        /// <returns>An <see cref="ISceneComponent"/> created from <paramref name="scene"/>.</returns>
        ISceneComponent CreateSceneComponent(Scene scene);
    }
}
