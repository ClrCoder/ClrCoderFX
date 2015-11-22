﻿// <copyright file="TypeEx{TType}.cs" company="ClrCoder project">
// Copyright (c) ClrCoder project. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>
namespace ClrCoder.System
{
    using global::System;
    using global::System.Linq;
    using global::System.Linq.Expressions;
    using global::System.Reflection;

    using JetBrains.Annotations;

    /// <summary>
    /// Extensions methods for the BCL reflection API.
    /// </summary>
    /// <typeparam name="TType"><c>Type</c> argument for extension methods.</typeparam>
    [PublicAPI]
    public static class TypeEx<TType>
    {
        /// <summary>
        /// Creates type constructor <see langword="delegate"/>.<br/>
        /// TODO: Add result caching.
        /// </summary>
        /// <exception cref="InvalidOperationException">Type does not have parameterless instance constructor.</exception>
        /// <returns>Type constructor delegate.</returns>
        public static Func<TType> CreateConstructorDelegate()
        {
            var type = typeof(TType);

            var ci =
                type.GetTypeInfo()
                    .DeclaredConstructors.Where(x => !x.GetParameters().Any() && !x.IsStatic)
                    .OrderBy(x => x.IsPublic ? 0 : 1)
                    .FirstOrDefault();
            if (ci == null)
            {
                throw new InvalidOperationException("Type does not have parameterless instance constructor.");
            }

            var body = Expression.New(ci);
            return Expression.Lambda<Func<TType>>(body).Compile();
        }
    }
}