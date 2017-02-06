﻿// <copyright file="IIxInstance.cs" company="ClrCoder project">
// Copyright (c) ClrCoder project. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace ClrCoder.ComponentModel.IndirectX
{
    using System.Collections.Generic;

    using JetBrains.Annotations;

    public interface IIxInstance : IAsyncDisposable
    {
        IxHost Host { get; }

        IxProviderNode ProviderNode { get; }

        object Object { get; }

        IIxInstance ParentInstance { get; }

        IIxResolver Resolver { get; set; }

        object DataSyncRoot { get; }

        IReadOnlyCollection<IIxInstanceLock> OwnedLocks { get; }

        IReadOnlyCollection<IIxInstanceLock> Locks { get; }

        /// <summary>
        /// Identifies that instance will be dispose in progress.
        /// </summary>
        bool IsDisposing { get; }

        void AddLock(IIxInstanceLock instanceLock);

        void AddOwnedLock(IIxInstanceLock instanceLock);

        [CanBeNull]
        object GetData(IxProviderNode providerNode);

        void RemoveLock(IIxInstanceLock instanceLock);

        void RemoveOwnedLock(IIxInstanceLock instanceLock);

        void SetData(IxProviderNode providerNode, [CanBeNull] object data);
    }
}