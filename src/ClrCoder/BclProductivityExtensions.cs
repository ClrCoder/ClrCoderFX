﻿// <copyright file="BclProductivityExtensions.cs" company="ClrCoder project">
// Copyright (c) ClrCoder project. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>
namespace ClrCoder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using JetBrains.Annotations;

    /// <summary>
    /// Extensions for core BCL classes designed to boost development productivity.
    /// </summary>
    [PublicAPI]
    public static class BclProductivityExtensions
    {
        /// <summary>
        /// <see cref="string.Format(string, object[])"/> alias.
        /// </summary>
        /// <param name="formatString">Format string.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>Formatted string.</returns>
        public static string Fmt(this string formatString, params object[] args)
        {
            return string.Format(formatString, args);
        }

        /// <summary>
        /// Gets random item from the <c>collection</c>.
        /// </summary>
        /// <remarks>
        /// This estension method is not thread safe.
        /// </remarks>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="rnd"><see cref="Random"/> <c>object</c>.</param>
        /// <param name="collection">Collection to get item from.</param>
        /// <returns>Random collection item.</returns>
        public static T From<T>(this Random rnd, IReadOnlyList<T> collection)
        {
            if (rnd == null)
            {
                throw new ArgumentNullException(nameof(rnd));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection[rnd.Next(collection.Count)];
        }

        /// <summary>
        /// Gets value from <c>dictionary</c> and returns default value if the specified <c>key</c> is not found.
        /// </summary>
        /// <typeparam name="TKey"><c>Type</c> of the <c>dictionary</c> <c>key</c>.</typeparam>
        /// <typeparam name="TValue"><c>Type</c> of the <c>dictionary</c> value</typeparam>
        /// <param name="dictionary"><c>Dictionary</c> to get value from.</param>
        /// <param name="key">Key to search value.</param>
        /// <returns>Value for the specified key or default value.</returns>
        public static TValue GetOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            TValue result;

            if (dictionary.TryGetValue(key, out result))
            {
                return result;
            }

            return default(TValue);
        }

        /// <summary>
        /// Gets exception short description in form ExceptionType: Message.
        /// </summary>
        /// <param name="ex"><c>Exception</c> to get short description for.</param>
        /// <returns>Short description string.</returns>
        public static string GetShortDescription(this Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            return $"{ex.GetType().Name}: {ex.Message}";
        }

        /// <summary>
        /// Verifies that <c>sequence</c> is empty or even <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Sequence element type.</typeparam>
        /// <param name="sequence">Sequence to test.</param>
        /// <returns>true, if sequence is null or empty.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> sequence)
        {
            return sequence == null || !sequence.Any();
        }

        /// <summary>
        /// Replaces the <paramref name="source"/> value with the <paramref name="substitute"/>, if it is equals to the 
        /// <paramref name="comparand"/>.
        /// </summary>
        /// <typeparam name="T">Type of source value.</typeparam>
        /// <param name="source">Source value.</param>
        /// <param name="comparand">Value to replace.</param>
        /// <param name="substitute">Replacement value.</param>
        /// <returns><paramref name="substitute"/> when <paramref name="source"/> is equals to the 
        /// <paramref name="comparand"/>, or <paramref name="source"/> otherwise.</returns>
        public static T Replace<T>(this T source, T comparand, T substitute) where T : IEquatable<T>
        {
            return source.Equals(comparand) ? substitute : source;
        }

        /// <summary>
        /// Calls <c>action</c> if it is not <c>null</c>.
        /// </summary>
        /// <param name="action"><c>Action</c> to call.</param>
        public static void SafeInvoke(this Action action)
        {
            action?.Invoke();
        }

        /// <summary>
        /// Calls <c>action</c> if it is not <c>null</c>.
        /// </summary>
        /// <typeparam name="T1"><c>Type</c> of the argument 1.</typeparam>
        /// <param name="action"><c>Action</c> to call.</param>
        /// <param name="arg1"><c>Action</c> argument 1.</param>
        public static void SafeInvoke<T1>(this Action<T1> action, T1 arg1)
        {
            action?.Invoke(arg1);
        }

        /// <summary>
        /// Calls <c>action</c> if it is not <c>null</c>.
        /// </summary>
        /// <typeparam name="T1"><c>Type</c> of the argument 1.</typeparam>
        /// <typeparam name="T2"><c>Type</c> of the argument 2.</typeparam>
        /// <param name="action"><c>Action</c> to call.</param>
        /// <param name="arg1"><c>Action</c> argument 1.</param>
        /// <param name="arg2"><c>Action</c> argument 2.</param>
        public static void SafeInvoke<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            action?.Invoke(arg1, arg2);
        }

        /// <summary>
        /// Calls <c>action</c> if it is not <c>null</c>.
        /// </summary>
        /// <typeparam name="T1"><c>Type</c> of the argument 1.</typeparam>
        /// <typeparam name="T2"><c>Type</c> of the argument 2.</typeparam>
        /// <typeparam name="T3"><c>Type</c> of the argument 3.</typeparam>
        /// <param name="action"><c>Action</c> to call.</param>
        /// <param name="arg1"><c>Action</c> argument 1.</param>
        /// <param name="arg2"><c>Action</c> argument 2.</param>
        /// <param name="arg3"><c>Action</c> argument 3.</param>
        public static void SafeInvoke<T1, T2, T3>(this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            action?.Invoke(arg1, arg2, arg3);
        }

        /// <summary>
        /// Calls <c>action</c> if it is not <c>null</c>.
        /// </summary>
        /// <typeparam name="T1"><c>Type</c> of the argument 1.</typeparam>
        /// <typeparam name="T2"><c>Type</c> of the argument 2.</typeparam>
        /// <typeparam name="T3"><c>Type</c> of the argument 3.</typeparam>
        /// <typeparam name="T4"><c>Type</c> of the argument 4.</typeparam>
        /// <param name="action"><c>Action</c> to call.</param>
        /// <param name="arg1"><c>Action</c> argument 1.</param>
        /// <param name="arg2"><c>Action</c> argument 2.</param>
        /// <param name="arg3"><c>Action</c> argument 3.</param>
        /// <param name="arg4"><c>Action</c> argument 4.</param>
        public static void SafeInvoke<T1, T2, T3, T4>(
            this Action<T1, T2, T3, T4> action, 
            T1 arg1, 
            T2 arg2, 
            T3 arg3, 
            T4 arg4)
        {
            action?.Invoke(arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Calls <c>action</c> if it is not <c>null</c>.
        /// </summary>
        /// <typeparam name="T1"><c>Type</c> of the argument 1.</typeparam>
        /// <typeparam name="T2"><c>Type</c> of the argument 2.</typeparam>
        /// <typeparam name="T3"><c>Type</c> of the argument 3.</typeparam>
        /// <typeparam name="T4"><c>Type</c> of the argument 4.</typeparam>
        /// <typeparam name="T5"><c>Type</c> of the argument 5.</typeparam>
        /// <param name="action"><c>Action</c> to call.</param>
        /// <param name="arg1"><c>Action</c> argument 1.</param>
        /// <param name="arg2"><c>Action</c> argument 2.</param>
        /// <param name="arg3"><c>Action</c> argument 3.</param>
        /// <param name="arg4"><c>Action</c> argument 4.</param>
        /// <param name="arg5"><c>Action</c> argument 5.</param>
        public static void SafeInvoke<T1, T2, T3, T4, T5>(
            this Action<T1, T2, T3, T4, T5> action, 
            T1 arg1, 
            T2 arg2, 
            T3 arg3, 
            T4 arg4, 
            T5 arg5)
        {
            action?.Invoke(arg1, arg2, arg3, arg4, arg5);
        }

        /// <summary>
        /// Calls event handler if it is not <c>null</c>.
        /// </summary>
        /// <typeparam name="T">Type of event args.</typeparam>
        /// <param name="eventHandler">Event handler <see langword="delegate"/>.</param>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="eventArgs">Event arguments.</param>
        public static void SafeInvoke<T>(this EventHandler<T> eventHandler, object sender, T eventArgs)
        {
            eventHandler?.Invoke(sender, eventArgs);
        }

        /// <summary>
        /// Converts string to <see langword="bool"/>.
        /// </summary>
        /// <param name="str">String to convert to <see langword="bool"/>.</param>
        /// <returns>Parsed bool value or null if string is null or invalid decimal.</returns>
        public static bool? ToBool(this string str)
        {
            bool result;
            return str != null && bool.TryParse(str, out result) ? (bool?)result : null;
        }

        /// <summary>
        /// Converts string to <see langword="decimal"/>.
        /// </summary>
        /// <param name="str">String to convert to <see langword="decimal"/>.</param>
        /// <returns>Parsed decimal value or null if string is null or invalid decimal.</returns>
        public static decimal? ToDecimal(this string str)
        {
            decimal result;
            return str != null
                   && decimal.TryParse(
                       str.Replace(",", "."),
                       NumberStyles.Any,
                       CultureInfo.InvariantCulture,
                       out result)
                       ? (decimal?)result
                       : null;
        }

        /// <summary>
        /// Converts string to <see langword="int"/>.
        /// </summary>
        /// <param name="str">String to convert to <see langword="int"/>.</param>
        /// <returns>Parsed integer value or null if string is null or invalid integer.</returns>
        public static int? ToInt(this string str)
        {
            int result;
            return str != null && int.TryParse(str, out result) ? (int?)result : null;
        }

        /// <summary>
        /// Converts timespan to <see langword="double"/> seconds value;
        /// </summary>
        /// <param name="timeSpan">Time span to convert.</param>
        /// <returns>Time span in seconds.</returns>
        public static double ToSeconds(this TimeSpan timeSpan)
        {
            return timeSpan.Ticks / (double)TimeSpan.TicksPerSecond;
        }
    }
}