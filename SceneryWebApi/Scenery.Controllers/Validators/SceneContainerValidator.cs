﻿// <copyright file="SceneContainerValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators
{
    using FluentValidation;
    using Scenery.Components.Interfaces;
    using Scenery.Models;

    /// <summary>
    /// Represents a validator for <see cref="SceneContainer"/>.
    /// </summary>
    public class SceneContainerValidator : AbstractValidator<SceneContainer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneContainerValidator"/> class.
        /// </summary>
        /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
        public SceneContainerValidator(IVector3Component vector3Component)
        {
            // TODO: Add unit tests.
            this.RuleFor(sceneContainer => sceneContainer.Scene)
                .NotNull()
                .SetValidator(new SceneValidator(vector3Component));
            this.RuleFor(sceneContainer => sceneContainer.ProjectorSettings)
                .NotNull()
                .SetValidator(new ProjectorSettingsValidator(vector3Component));
            this.RuleFor(sceneContainer => sceneContainer.SamplerSettings)
                .NotNull()
                .SetValidator(new SamplerSettingsValidator());
        }
    }
}
