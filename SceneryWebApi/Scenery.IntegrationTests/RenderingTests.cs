// <copyright file="RenderingTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.IntegrationTests
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Scenery.Controllers;
    using Xunit;

    /// <summary>
    /// Provides integration tests for rendering scenes.
    /// </summary>
    public class RenderingTests : IDisposable
    {
        private readonly WebApplicationFactory<Startup> factory;
        private readonly HttpClient client;
        private readonly Uri uri;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingTests"/> class.
        /// </summary>
        public RenderingTests()
        {
            this.factory = new WebApplicationFactory<Startup>();
            this.client = this.factory.CreateClient();
            this.uri = new Uri("http://localhost/Scenes");
        }

        /// <summary>
        /// Tests the WebApi.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task WhenGetIsCalledThenExampleScenesAreReturned()
        {
            // Act.
            var response = await this.client.GetAsync(this.uri);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var actualJson = await response.Content.ReadAsStringAsync();
            var expectedJson = File.ReadAllText("Scenes\\scenes.json");
            actualJson.Should().Be(expectedJson);
        }

        /// <summary>
        /// Tests the WebApi.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GivenABadSceneWhenPostIsCalledThenTheStatusCodeIsBadRequest()
        {
            // Arrange.
            var json = string.Empty;
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Act.
            var response = await this.client.PostAsync(this.uri, stringContent);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Tests the WebApi.
        /// </summary>
        /// <param name="jsonFilename">The name of a json file containing a scene.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Theory]
        [ClassData(typeof(SceneTestData))]
        public async Task GivenASceneWhenPostIsCalledThenItIsCorrectlyRendered(string jsonFilename)
        {
            // Arrange.
            if (string.IsNullOrWhiteSpace(jsonFilename))
            {
                throw new ArgumentException($"'{nameof(jsonFilename)}' cannot be null or whitespace.", nameof(jsonFilename));
            }

            var json = File.ReadAllText(jsonFilename);
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Act.
            var response = await this.client.PostAsync(this.uri, stringContent);

            // Assert.
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var actualFile = await response.Content.ReadAsByteArrayAsync();
            var expectedFile = File.ReadAllBytes(jsonFilename.Replace(".json", ".png"));
            actualFile.Should().BeEquivalentTo(expectedFile);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of resources.
        /// </summary>
        /// <param name="disposing">Whether we are disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.client.Dispose();
                this.factory.Dispose();
            }

            this.disposed = true;
        }
    }
}
