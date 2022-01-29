// <copyright file="ISceneComponentFactory.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces.SceneComponents;

using Scenery.Models.Scenes;

/// <summary>
/// A factory for creating <see cref="ISceneComponent"/>s.
/// </summary>
public interface ISceneComponentFactory
{
    /// <summary>
    /// Creates an <see cref="ISceneComponent"/> from a scene.
    /// </summary>
    /// <param name="scene">The scene.</param>
    /// <returns>The <see cref="ISceneComponent"/> created from the scene.</returns>
    ISceneComponent CreateSceneComponent(Scene scene);
}
