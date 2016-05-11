﻿// <copyright file="ILockToken.cs" company="ClrCoder project">
// Copyright (c) ClrCoder project. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>
namespace ClrCoder.Threading
{
    using System;

    /// <summary>
    /// Lock token abstraction.
    /// </summary>
    public interface ILockToken : IDisposable
    {
    }
}