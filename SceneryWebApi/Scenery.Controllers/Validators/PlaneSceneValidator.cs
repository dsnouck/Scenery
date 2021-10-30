// <copyright file="PlaneSceneValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators
{
    using FluentValidation;
    using Scenery.Models.Scenes;

    /// <summary>
    /// Represents a validator for <see cref="PlaneScene"/>.
    /// </summary>
    public class PlaneSceneValidator : AbstractValidator<PlaneScene>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaneSceneValidator"/> class.
        /// </summary>
        public PlaneSceneValidator()
        {
            this.RuleFor(scene => scene.Normal)
                .NotNull();
        }
    }
}