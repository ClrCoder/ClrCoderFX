﻿// <copyright file="BclBehaviorTests.cs" company="ClrCoder project">
// Copyright (c) ClrCoder project. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>
namespace ClrCoder.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using FluentAssertions;

    using NUnit.Framework;

    /// <summary>
    /// Identifies behavior of BCL parts.
    /// </summary>
    [TestFixture]
    public class BclBehaviorTests
    {
        /// <summary>
        /// Long running TPL task should use background thread. This behavior required to implement supporting background services that is terminated on application exit.
        /// </summary>
        [Test]
        public void LongRunningTaskShouldUseBackgroundThreads()
        {
            var cts = new CancellationTokenSource();
#if !PCL
            var task = Task.Factory.StartNew(
                () =>
                    {
                        Thread.CurrentThread.IsBackground.Should()
                            .BeTrue("Assuming long running task spawn background thread.");

                        // Performing wait that will be terminated.
                        try
                        {
                            cts.Token.WaitHandle.WaitOne();
                        }
                        catch (ThreadAbortException)
                        {
                            // This code block actually executed, but NUnit skips this event.
                            // Inspect this message in Debug output window.
                            Trace.WriteLine("Gracefull termination");
                        }

                        // Right after the catch runtime will rise ThreadAbortException again.
                        Trace.WriteLine("Unreachable code!");
                    }, 
                TaskCreationOptions.LongRunning);
            task.Wait(TimeSpan.FromSeconds(1)).Should().BeFalse();
#else
            var task = Task.Factory.StartNew(
                () =>
                    {
                        Thread.CurrentThread.IsBackground.Should()
                            .BeTrue("Assuming long running task spawn background thread.");

                        // Performing wait that will be terminated.
                        try
                        {
                            cts.Token.WaitHandle.WaitOne();
                        }
                        catch (Exception ex)
                        {
                            // TODO: Identify exception!
                            TestContext.Out.WriteLine(ex.GetType());

                            // This code block actually executed, but NUnit skips this event.
                            // Inspect this message in Debug output window.
                            TestContext.Out.WriteLine("Gracefull termination");
                        }

                        // Right after the catch runtime will rise ThreadAbortException again.
                        TestContext.Out.WriteLine("Unreachable code!");
                    }, 
                TaskCreationOptions.LongRunning);
            task.Wait(TimeSpan.FromSeconds(1)).Should().BeFalse();
#endif
        }
    }
}