// <copyright file="SceneTestData.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.IntegrationTests;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Provides test scenes for <see cref="RenderingTests"/>.
/// </summary>
public class SceneTestData : IEnumerable<object[]>
{
    private readonly List<object[]> jsonFilenames;

    /// <summary>
    /// Initializes a new instance of the <see cref="SceneTestData"/> class.
    /// </summary>
    public SceneTestData()
    {
        // scenes.json is used to test the output of GET ​/Scenes.
        this.jsonFilenames = Directory.GetFiles("Scenes", "*.json")
            .Where(filename => filename != "Scenes\\scenes.json")
            .Select(filename => (new object[] { filename }))
            .ToList();
    }

    /// <inheritdoc/>
    public IEnumerator<object[]> GetEnumerator()
    {
        return this.jsonFilenames.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
