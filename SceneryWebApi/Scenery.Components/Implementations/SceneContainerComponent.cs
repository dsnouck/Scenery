// <copyright file="SceneContainerComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using Scenery.Models.Scenes;

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

        /// <inheritdoc/>
        public SceneContainer GetExample()
        {
            return new SceneContainer
            {
                Scene = new Intersection
                {
                    Scenes = new List<Scene>
                    {
                        new Scaled
                            {
                                Factor = 1 / Math.Sqrt(3D),
                                Scene = new Cube(),
                            },
                        new Inverted
                        {
                            Scene = new Scaled
                            {
                                Factor = 0.9D * Math.Sqrt(2D / 3D),
                                Scene = new Colored
                                {
                                    Color = new Color
                                    {
                                        R = 1D,
                                        G = 0D,
                                        B = 0D,
                                    },
                                    Scene = new Sphere(),
                                },
                            },
                        },
                    },
                },
                Projector = new ProjectorSettings
                {
                    Eye = new Vector3
                    {
                        X = 2.1D,
                        Y = 2.7D,
                        Z = 2D,
                    },
                    Focus = new Vector3
                    {
                        X = 0D,
                        Y = 0D,
                        Z = 0D,
                    },
                    FieldOfView = Math.PI / 4D,
                    Background = new Color
                    {
                        R = 0D,
                        G = 0D,
                        B = 0D,
                    },
                },
                Sampler = new SamplerSettings
                {
                    Columns = 160,
                    Rows = 120,
                    Subsamples = 2,
                },
            };
        }

        /// <inheritdoc/>
        public Stream GetStream(SceneContainer sceneContainer)
        {
            var image = this.projectorComponent.ProjectSceneToImage(sceneContainer.Scene, sceneContainer.Projector);
            var bitmap = this.samplerComponent.SampleImageToBitmap(image, sceneContainer.Sampler);
            return this.bitmapFileComponent.GetStream(bitmap);
        }
    }
}
