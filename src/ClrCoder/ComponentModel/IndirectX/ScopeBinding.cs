﻿// <copyright file="ScopeBinding.cs" company="ClrCoder project">
// Copyright (c) ClrCoder project. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace ClrCoder.ComponentModel.IndirectX
{
    public class ScopeBinding
    {
        public static ScopeBinding Registration => new ScopeBinding();

        public static ScopeBinding ResolveOrigin => new ScopeBinding();

        public static ScopeBinding PerResolve => new ScopeBinding();

        public static ScopeBinding PerQuery => new ScopeBinding();
    }
}