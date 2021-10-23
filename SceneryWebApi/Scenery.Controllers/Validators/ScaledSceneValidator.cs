// <copyright file="ScaledSceneValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators
{
    using FluentValidation;
    using Scenery.Models.Scenes;

    /// <summary>
    /// Represents a validator for <see cref="ScaledScene"/>s.
    /// </summary>
    public class ScaledSceneValidator : AbstractValidator<ScaledScene>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScaledSceneValidator"/> class.
        /// </summary>
        public ScaledSceneValidator()
        {
            this.RuleFor(scene => scene.OriginalScene)
                .NotNull()
                .SetValidator(new SceneValidator());
        }
    }
}
