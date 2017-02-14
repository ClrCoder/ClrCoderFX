﻿// <copyright file="WebHostDeploymentConfig.cs" company="ClrCoder project">
// Copyright (c) ClrCoder project. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace ClrCoder.AspNetCore
{
    using JetBrains.Annotations;

    /// <summary>
    /// Web host deployment configuration.
    /// </summary>
    public class WebHostDeploymentConfig
    {
        [CanBeNull]
        public string UrlsEnvironmentVariableName { get; set; }
    }
}