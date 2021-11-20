// <copyright file="PlaneValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators.SceneValidators
{
    using FluentValidation;
    using Scenery.Models.Scenes;

    /// <summary>
    /// Represents a validator for <see cref="Plane"/>.
    /// </summary>
    public class PlaneValidator : AbstractValidator<Plane>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaneValidator"/> class.
        /// </summary>
        public PlaneValidator()
        {
            this.RuleFor(scene => scene.Normal)
                .NotNull();
        }
    }
}
