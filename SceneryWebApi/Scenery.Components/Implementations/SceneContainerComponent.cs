// <copyright file="SceneContainerComponent.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Implementations
{
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using System.IO;

    /// <inheritdoc/>
    public class SceneContainerComponent : ISceneContainerComponent
    {
        private readonly IBitmapFileComponent bitmapFileComponent;
        private readonly IProjectorComponent projectorComponent;
        private readonly ISamplerComponent samplerComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneContainerComponent"/> class.
        /// </summary>
        /// <param name="bitmapFileComponent">An <see cref="IBitmapFileComponent"/>.</param>
        /// <param name="projectorComponent">An <see cref="IProjectorComponent"/>.</param>
        /// <param name="samplerComponent">An <see cref="ISamplerComponent"/>.</param>
        public SceneContainerComponent(
            IBitmapFileComponent bitmapFileComponent,
            IProjectorComponent projectorComponent,
            ISamplerComponent samplerComponent)
        {
            this.bitmapFileComponent = bitmapFileComponent;
            this.projectorComponent = projectorComponent;
            this.samplerComponent = samplerComponent;
        }

        public Stream GetStream(SceneContainer sceneContainer)
        {
            var image = this.projectorComponent.ProjectSceneToImage(sceneContainer.Scene, sceneContainer.ProjectorSettings);
            var bitmap = this.samplerComponent.SampleImageToBitmap(image, sceneContainer.SamplerSettings);
            return this.bitmapFileComponent.GetStream(bitmap);
        }
    }
}
