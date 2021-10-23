// <copyright file="SceneContainerValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Validators
{
    using FluentValidation;
    using Scenery.Models;

    /// <summary>
    /// Represents a validator for <see cref="SceneContainer"/>s.
    /// </summary>
    public class SceneContainerValidator : AbstractValidator<SceneContainer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneContainerValidator"/> class.
        /// </summary>
        public SceneContainerValidator()
        {
            // TODO: Add unit tests.
            // TODO: Add validators for all properties.
            this.RuleFor(sceneContainer => sceneContainer.Scene)
                .NotNull()
                .SetValidator(new SceneValidator());
        }
    }
}
