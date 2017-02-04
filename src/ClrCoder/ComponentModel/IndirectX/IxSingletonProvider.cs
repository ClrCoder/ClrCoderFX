﻿// <copyright file="IxSingletonProvider.cs" company="ClrCoder project">
// Copyright (c) ClrCoder project. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>
namespace ClrCoder.ComponentModel.IndirectX
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    public class IxSingletonProvider : IxProviderNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IxSingletonProvider"/> class.
        /// </summary>
        public IxSingletonProvider(
            IxHost host,
            [CanBeNull] IxProviderNode parentNode,
            IxStdProviderConfig config,
            [CanBeNull] IxHost.RawInstanceFactory rawInstanceFactory,
            IxHost.VisibilityFilter exportFilter,
            IxHost.VisibilityFilter exportToParentFilter,
            IxHost.VisibilityFilter importFilter,
            IxHost.ScopeBinderDelegate scopeBinder)
            : base(
                host,
                parentNode,
                config,
                rawInstanceFactory,
                exportFilter,
                exportToParentFilter,
                importFilter,
                scopeBinder)
        {
            if (parentNode == null)
            {
                throw new ArgumentNullException(nameof(parentNode));
            }

            if (rawInstanceFactory == null)
            {
                throw new ArgumentNullException(nameof(rawInstanceFactory));
            }
        }

        public override async Task<IIxInstance> GetInstance(IIxInstance parentInstance, IxHost.IxResolveContext context)
        {
            Task<IIxInstance> creationTask;
            lock (Host.InstanceTreeSyncRoot)
            {
                lock (parentInstance.DataSyncRoot)
                {
                    object data = parentInstance.GetData(this);
                    if (data == null)
                    {
                        creationTask = CreateInstance(parentInstance, context);
                        if (creationTask.IsCompleted)
                        {
                            // Returns good result or exception.
                            return creationTask.GetAwaiter().GetResult();
                        }

                        parentInstance.SetData(this, creationTask);
                    }
                    else if (data is Task)
                    {
                        creationTask = (Task<IIxInstance>)data;
                    }
                    else
                    {
                        // Object created.
                        return (IIxInstance)data;
                    }
                }
            }

            return await creationTask;
        }

        private async Task<IIxInstance> CreateInstance(IIxInstance parentInstance, IxHost.IxResolveContext context)
        {
            IIxInstance result = null;
            try
            {
                Debug.Assert(RawInstanceFactory != null, "RawInstanceFactory != null");
                object @object = await RawInstanceFactory(parentInstance, context);
                return new IxSingletonInstance(Host, this, parentInstance, @object);
            }
            finally
            {
                lock (parentInstance.DataSyncRoot)
                {
                    parentInstance.SetData(this, result);
                }
            }

            return result;
        }
    }
}