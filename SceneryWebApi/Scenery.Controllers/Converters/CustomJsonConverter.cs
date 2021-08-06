// <copyright file="CustomJsonConverter.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Converters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <inheritdoc/>
    public abstract class CustomJsonConverter<T> : JsonConverter<T>
    {
        /// <summary>
        /// Reads a start object.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected static void ReadStartObject(ref Utf8JsonReader reader)
        {
            if (reader.TokenType != JsonTokenType.StartObject && (!reader.Read() || reader.TokenType != JsonTokenType.StartObject))
            {
                throw new JsonException();
            }
        }

        /// <summary>
        /// Reads an end object.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected static void ReadEndObject(ref Utf8JsonReader reader)
        {
            if (!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
            {
                throw new JsonException();
            }
        }

        /// <summary>
        /// Reads a start array.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected static void ReadStartArray(ref Utf8JsonReader reader)
        {
            if (!reader.Read() || reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }
        }

        /// <summary>
        /// Reads an end array.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>Whether the end array could be read.</returns>
        protected static bool ReadEndArray(ref Utf8JsonReader reader)
        {
            if (!reader.Read())
            {
                throw new JsonException();
            }

            return reader.TokenType == JsonTokenType.EndArray;
        }

        /// <summary>
        /// Reads a property name.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="propertyName">The property name.</param>
        protected static void ReadPropertyName(ref Utf8JsonReader reader, string propertyName)
        {
            if (!reader.Read() || reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            if (!string.Equals(reader.GetString(), propertyName, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new JsonException();
            }
        }

        /// <summary>
        /// Reads a double property.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>A double property.</returns>
        protected static double ReadDoubleProperty(ref Utf8JsonReader reader, string propertyName)
        {
            ReadPropertyName(ref reader, propertyName);

            if (!reader.Read() || reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            return reader.GetDouble();
        }

        /// <summary>
        /// Reads a string property.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>A string property.</returns>
        protected static string ReadStringProperty(ref Utf8JsonReader reader, string propertyName)
        {
            ReadPropertyName(ref reader, propertyName);

            if (!reader.Read() || reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            return reader.GetString() ?? throw new JsonException();
        }

        /// <summary>
        /// Writes a start object.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected static void WriteStartObject(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
        }

        /// <summary>
        /// Writes a end object.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected static void WriteEndObject(Utf8JsonWriter writer)
        {
            writer.WriteEndObject();
        }

        /// <summary>
        /// Writes a start array.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected static void WriteStartArray(Utf8JsonWriter writer)
        {
            writer.WriteStartArray();
        }

        /// <summary>
        /// Writes an end array.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected static void WriteEndArray(Utf8JsonWriter writer)
        {
            writer.WriteEndArray();
        }

        /// <summary>
        /// Writes a property name.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="propertyName">The property name.</param>
        protected static void WritePropertyName(Utf8JsonWriter writer, string propertyName)
        {
            writer.WritePropertyName(ConvertToCamelCase(propertyName));
        }

        /// <summary>
        /// Writes a number.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The value.</param>
        protected static void WriteNumber(Utf8JsonWriter writer, string propertyName, double value)
        {
            writer.WriteNumber(ConvertToCamelCase(propertyName), value);
        }

        /// <summary>
        /// Writes a string in camel case.
        /// </summary>
        /// <param name="writer">he writer.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The value.</param>
        protected static void WriteStringInCamelCase(Utf8JsonWriter writer, string propertyName, string value)
        {
            writer.WriteString(ConvertToCamelCase(propertyName), ConvertToCamelCase(value));
        }

        /// <summary>
        /// Converts a string to camel case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A string converted to camel case.</returns>
        protected static string ConvertToCamelCase(string value)
        {
            return $"{char.ToLowerInvariant(value[0])}{value[1..]}";
        }

        /// <summary>
        /// Converts a string to Pascal case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A string converted to Pascal case.</returns>
        protected static string ConvertToPascalCase(string value)
        {
            return $"{char.ToUpperInvariant(value[0])}{value[1..]}";
        }
    }
}
