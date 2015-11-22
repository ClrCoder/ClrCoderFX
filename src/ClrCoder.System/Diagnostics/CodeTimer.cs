﻿// <copyright file="CodeTimer.cs" company="ClrCoder project">
// Copyright (c) ClrCoder project. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>
namespace ClrCoder.System.Diagnostics
{
    using global::System;
    using global::System.Diagnostics;
    using global::System.Globalization;
    using global::System.Runtime.CompilerServices;

    /// <summary>
    /// Lightweight timer for microbenchmarks.
    /// </summary>
    public struct CodeTimer
    {
        private static readonly Stopwatch TimerFromStart;

        private readonly double _startTime;

        private readonly int? _precision;

        static CodeTimer()
        {
            TimerFromStart = new Stopwatch();
            TimerFromStart.Start();
        }

        private CodeTimer(double startTime, int? precision = null)
        {
            _startTime = startTime;
            _precision = precision;
        }

        /// <summary>
        /// Time in seconds elapsed from timer start.
        /// </summary>
        public double Time => GetTimeFromAppStart() - _startTime;

        /// <summary>
        /// Forced class warmup (ensures that class constructor was called).
        /// </summary>
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static void WarmUp()
        {
        }

        /// <summary>
        /// Gets precise time in seconds from application starts (actually from <see langword="this"/> class initialization).
        /// </summary>
        /// <returns>Precise time in seconds.</returns>
        public static double GetTimeFromAppStart()
        {
            return TimerFromStart.ElapsedTicks / (double)Stopwatch.Frequency;
        }

        /// <summary>
        /// Starts new timer.
        /// </summary>
        /// <returns>Timer instance.</returns>
        public static CodeTimer Start()
        {
            return new CodeTimer(GetTimeFromAppStart());
        }

        /// <summary>
        /// Starts new timer.
        /// </summary>
        /// <param name="precision">Number of <c>precision</c> digits that is used in the <see cref="ToString"/> method.</param>
        /// <returns>Timer instance.</returns>
        public static CodeTimer Start(int precision)
        {
            if (precision < 0 || precision > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(precision));
            }

            return new CodeTimer(GetTimeFromAppStart(), precision);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            if (_precision != null)
            {
                return ("{0:F" + _precision + "}").Fmt(Time);
            }

            return Time.ToString(CultureInfo.CurrentCulture);
        }
    }
}