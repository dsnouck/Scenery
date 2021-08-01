// <copyright file="DependencyInjections.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components
{
    using Microsoft.Extensions.DependencyInjection;
    using Scenery.Components.Implementations;
    using Scenery.Components.Implementations.SceneComponents;
    using Scenery.Components.Interfaces;
    using Scenery.Components.Interfaces.SceneComponents;

    /// <summary>
    /// Provides dependency injections for components.
    /// </summary>
    public static class DependencyInjections
    {
        /// <summary>
        /// Adds components to <paramref name="serviceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">The original <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> with components.</returns>
        public static IServiceCollection AddComponents(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBitmapComponent, BitmapComponent>();
            serviceCollection.AddScoped<IBitmapFileComponent, PngFileComponent>();
            serviceCollection.AddScoped<IColorComponent, ColorComponent>();
            serviceCollection.AddScoped<IFuncDoubleDoubleComponent, FuncDoubleDoubleComponent>();
            serviceCollection.AddScoped<IFuncVector2Vector3Component, FuncVector2Vector3Component>();
            serviceCollection.AddScoped<ILine3Component, Line3Component>();
            serviceCollection.AddScoped<IMatrix4Component, Matrix4Component>();
            serviceCollection.AddScoped<IProjectorComponent, ProjectorComponent>();
            serviceCollection.AddScoped<ISamplerComponent, SamplerComponent>();
            serviceCollection.AddScoped<ISceneComponentFactory, SceneComponentFactory>();
            serviceCollection.AddScoped<ISceneContainerComponent, SceneContainerComponent>();
            serviceCollection.AddScoped<IVector3Component, Vector3Component>();
            serviceCollection.AddScoped<IVector4Component, Vector4Component>();

            return serviceCollection;
        }
    }
}
