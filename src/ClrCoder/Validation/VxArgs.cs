﻿// <copyright file="VxArgs.cs" company="ClrCoder project">
// Copyright (c) ClrCoder project. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace ClrCoder.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;

    using JetBrains.Annotations;

    /// <summary>
    /// Simple argument validation utils.
    /// </summary>
    [PublicAPI]
    public static class VxArgs
    {
        /// <summary>
        /// Validates that argument is positive and finite.
        /// </summary>
        /// <param name="value">Argument <c>value</c>.</param>
        /// <param name="name">Argument <c>name</c>.</param>
        public static void FinitPositive(double value, [InvokerParameterName] string name)
        {
            if (!(value > 0.0) && double.IsPositiveInfinity(value))
            {
                throw new ArgumentOutOfRangeException(name, $"{name} should be finite positive.");
            }
        }

        /// <summary>
        /// Validates that argument is positive and finite.
        /// </summary>
        /// <param name="value">Argument <c>value</c>.</param>
        /// <param name="name">Argument <c>name</c>.</param>
        public static void FinitPositive(double? value, [InvokerParameterName] string name)
        {
            if (value != null && !(value > 0.0) && double.IsPositiveInfinity(value.Value))
            {
                throw new ArgumentOutOfRangeException(name, $"{name} should be finite positive.");
            }
        }

        /// <summary>
        /// Validates that <c>value</c> fall in the specified range.
        /// </summary>
        /// <param name="value">Value to validate.</param>
        /// <param name="start">Range <c>start</c> (inclusive).</param>
        /// <param name="end">Range <c>end</c> (inclusive).</param>
        /// <param name="name">Argument <c>name</c>.</param>
        public static void InRange(double value, double start, double end, [InvokerParameterName] string name)
        {
            if (!(value >= start) || !(value <= end))
            {
                throw new ArgumentOutOfRangeException($"{name} should fall in range [{start}, {end}]", nameof(name));
            }
        }

        /// <summary>
        /// Validates argument string is not empty.
        /// </summary>
        /// <param name="str">String to validate.</param>
        /// <param name="name">Name of argument.</param>
        public static void NonNullOrWhiteSpace(string str, [InvokerParameterName] string name)
        {
            if (str == null)
            {
                throw new ArgumentNullException(name);
            }

            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException($"{name} should not be an empty or whitespace string.", name);
            }
        }

        /// <summary>
        /// Validates that <c>collection</c> is not empty. Also validates items not <c>null</c> in debug mode.
        /// </summary>
        /// <typeparam name="T">Type of <c>collection</c> item.</typeparam>
        /// <param name="collection">Collection to validate.</param>
        /// <param name="name">Name of an argument.</param>
        [Pure]
        public static void NotEmpty<T>(ICollection<T> collection, string name)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(name);
            }

            if (collection.Count == 0)
            {
                throw new ArgumentException($"{name} collection should not be empty.", name);
            }

#if DEBUG
            if (!typeof(T).GetTypeInfo().IsValueType)
            {
                foreach (T item in collection)
                {
                    if (item == null)
                    {
                        throw new ArgumentException($"{name} collection element is null.", name);
                    }
                }
            }

#endif
        }

        /// <summary>
        /// Validates that <c>collection</c> is not empty. Also validates items not <c>null</c> in debug mode.
        /// </summary>
        /// <typeparam name="T">Type of <c>collection</c> item.</typeparam>
        /// <param name="collection">Collection to validate.</param>
        /// <param name="name">Name of an argument.</param>
        public static void NotEmptyReadOnly<T>(IReadOnlyCollection<T> collection, string name)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(name);
            }

            if (collection.Count == 0)
            {
                throw new ArgumentException($"{name} collection should not be empty.", name);
            }

#if DEBUG
            if (!typeof(T).GetTypeInfo().IsValueType)
            {
                foreach (T item in collection)
                {
                    if (item == null)
                    {
                        throw new ArgumentException($"{name} collection element is null.", name);
                    }
                }
            }

#endif
        }

        /// <summary>
        /// Validates that Width and Height are both non zero positive.
        /// </summary>
        /// <param name="size">Size to validate.</param>
        /// <param name="name">Argument <c>name</c>.</param>
        public static void PositiveSize(Size size, [InvokerParameterName] string name)
        {
            if (size.Width <= 0 || size.Height <= 0)
            {
                throw new ArgumentOutOfRangeException(name, "Both Width and Height should be non-zero positive");
            }
        }

        /// <summary>
        /// Validates that Width and Height are both non zero positive.
        /// </summary>
        /// <param name="size">Size to validate.</param>
        /// <param name="name">Argument <c>name</c>.</param>
        public static void PositiveSize(Size? size, [InvokerParameterName] string name)
        {
            if (size != null && (size.Value.Width <= 0 || size.Value.Height <= 0))
            {
                throw new ArgumentOutOfRangeException(name, "Both Width and Height should be non-zero positive");
            }
        }

        /// <summary>
        /// Validates that provided Uri is valid http and absolute.
        /// </summary>
        /// <param name="uri">Uri to validate.</param>
        /// <param name="name">Argument <c>name</c>.</param>
        public static void ValidAbsoluteHttpUri(string uri, [InvokerParameterName] string name)
        {
            NonNullOrWhiteSpace(uri, name);

            try
            {
                // ReSharper disable once ObjectCreationAsStatement
                var uriObj = new Uri(uri, UriKind.Absolute);

                if (uriObj.Scheme != "http" && uriObj.Scheme != "https")
                {
                    throw new ArgumentException($"{name} should use http/https", name);
                }
            }
            catch (UriFormatException ex)
            {
                throw new ArgumentException(ex.Message, name, ex);
            }
        }

        /// <summary>
        /// Validates that provided Uri is valid and absolute.
        /// </summary>
        /// <param name="uri">Uri to validate.</param>
        /// <param name="name">Argument <c>name</c>.</param>
        public static void ValidAbsoluteUri(string uri, [InvokerParameterName] string name)
        {
            NonNullOrWhiteSpace(uri, name);

            try
            {
                // ReSharper disable once ObjectCreationAsStatement
                new Uri(uri, UriKind.Absolute);
            }
            catch (UriFormatException ex)
            {
                throw new ArgumentException(ex.Message, name, ex);
            }
        }
    }
}