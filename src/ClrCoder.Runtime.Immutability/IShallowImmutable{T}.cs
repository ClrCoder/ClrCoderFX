﻿// <copyright file="IShallowImmutable{T}.cs" company="ClrCoder project">
// Copyright (c) ClrCoder project. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace System
{
    /// <summary>
    /// Implementing this contract is a one of ways how to show that instances will be always shallow immutable at part/aspect
    /// <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of part/aspect of instance that a defined as shallow immutable.</typeparam>
    public interface IShallowImmutable<T>
    {
    }
}