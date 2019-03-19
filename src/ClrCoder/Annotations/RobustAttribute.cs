// <copyright file="RobustAttribute.cs" company="ClrCoder project">
// Copyright (c) ClrCoder project. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace ClrCoder.Annotations
{
    using System;

    /// <summary>
    /// If pre-conditions satisfied, operation will throw only non processable exceptions.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Constructor | AttributeTargets.Delegate | AttributeTargets.Event | AttributeTargets.Method
        | AttributeTargets.Property)]
    public class RobustAttribute : Attribute
    {
    }
}
