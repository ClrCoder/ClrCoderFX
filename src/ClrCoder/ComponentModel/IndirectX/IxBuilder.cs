// <copyright file="IxBuilder.cs" company="ClrCoder project">
// Copyright (c) ClrCoder project. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace ClrCoder.ComponentModel.IndirectX
{
    public class IxBuilder<T> : IIxBuilder<T>
        where T : class
    {
        public T Config { get; set; }
    }
}
