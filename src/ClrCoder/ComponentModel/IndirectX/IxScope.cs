﻿// <copyright file="IxScope.cs" company="ClrCoder project">
// Copyright (c) ClrCoder project. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace ClrCoder.ComponentModel.IndirectX
{
    using System;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    public class IxScope : IxProviderNode
    {
        [CanBeNull]
        private IxScopeInstance _rootInstance;

        public IxScope(
            IxHost host,
            [CanBeNull] IxProviderNode parentNode,
            IxScopeConfig config,
            IxVisibilityFilter exportFilter,
            IxVisibilityFilter exportToParentFilter,
            IxVisibilityFilter importFilter)
            : base(
                host,
                parentNode,
                config,
                null,
                exportFilter,
                exportToParentFilter,
                importFilter,
                (a, b, c, d) => { throw new NotImplementedException(); },
                obj => Task.CompletedTask)
        {
        }

        public override async Task<IIxInstanceLock> GetInstance(
            IIxInstance parentInstance,
            IxHost.IxResolveContext context)
        {
            if (parentInstance == null)
            {
                throw new ArgumentNullException(nameof(parentInstance));
            }

            if (_rootInstance != null)
            {
                return new IxInstanceTempLock(_rootInstance);
            }

            // TODO: Implement smart singleton.
            throw new NotImplementedException();

            // return new IxScopeInstance(Host, this, parentInstance);
        }

        public IxScopeInstance GetRootInstance()
        {
            if (ParentNode != null)
            {
                throw new InvalidOperationException("Only root scope can produce root instance.");
            }

            return _rootInstance ?? (_rootInstance = new IxScopeInstance(Host, this, null));
        }
    }
}