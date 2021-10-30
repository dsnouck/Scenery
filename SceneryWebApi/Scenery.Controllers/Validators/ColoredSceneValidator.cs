// <copyright file="ColoredSceneValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators
{
    using FluentValidation;
    using Scenery.Components.Interfaces;
    using Scenery.Models.Scenes;

    /// <summary>
    /// Represents a validator for <see cref="ColoredScene"/>.
    /// </summary>
    public class ColoredSceneValidator : AbstractValidator<ColoredScene>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColoredSceneValidator"/> class.
        /// </summary>
        /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
        public ColoredSceneValidator(IVector3Component vector3Component)
        {
            this.RuleFor(scene => scene.Color)
                .NotNull()
                .SetValidator(new ColorValidator());
            this.RuleFor(scene => scene.OriginalScene)
                .NotNull()
                .SetValidator(new SceneValidator(vector3Component));
        }
    }
}