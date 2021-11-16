// <copyright file="SceneContainerComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations
{
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
                Scene = new IntersectedScene
                {
                    Scenes =
                    {
                        new ColoredScene
                        {
                            Color = new Color
                            {
                                R = 1D,
                                G = 0D,
                                B = 0D,
                            },
                            OriginalScene = new CubeScene(),
                        },
                        new ColoredScene
                        {
                            Color = new Color
                            {
                                R = 0D,
                                G = 0D,
                                B = 1D,
                            },
                            OriginalScene = new InvertedScene
                            {
                                OriginalScene = new ScaledScene
                                {
                                    Factor = 1.3D,
                                    OriginalScene = new SphereScene(),
                                },
                            },
                        },
                    },
                },
                ProjectorSettings = new ProjectorSettings
                {
                    Eye = new Vector3
                    {
                        XCoordinate = 4.430761575624772D,
                        YCoordinate = -2.4205360806680094D,
                        ZCoordinate = 3.2418138352088386D,
                    },
                    Focus = new Vector3
                    {
                        XCoordinate = 0D,
                        YCoordinate = 0D,
                        ZCoordinate = 0D,
                    },
                    HorizontalOpeningAngle = 0.7853981633974483D,
                    BackgroundColor = new Color
                    {
                        R = 1D,
                        G = 1D,
                        B = 1D,
                    },
                },
                SamplerSettings = new SamplerSettings
                {
                    ColumnCount = 160,
                    RowCount = 120,
                    SubsampleCount = 2,
                },
            };
        }

        /// <inheritdoc/>
        public Stream GetStream(SceneContainer sceneContainer)
        {
            var image = this.projectorComponent.ProjectSceneToImage(sceneContainer.Scene, sceneContainer.ProjectorSettings);
            var bitmap = this.samplerComponent.SampleImageToBitmap(image, sceneContainer.SamplerSettings);
            return this.bitmapFileComponent.GetStream(bitmap);
        }
    }
}
