﻿namespace Merchello.Core.Acquired.Logging
{
    using System;

    using Merchello.Core.Logging;

    /// <summary>
    /// Profiler extensions.
    /// </summary>
    /// <remarks>
    /// Direct port of Umbraco internal interface to get rid of hard dependency
    /// </remarks>
    internal static class ProfilerExtensions
    {
        /// <summary>
        /// Writes out a step prefixed with the type
        /// </summary>
        /// <typeparam name="T">The type of the step</typeparam>
        /// <param name="profiler">The <see cref="IProfiler"/></param>
        /// <param name="name">The name</param>
        /// <returns>The disposable step</returns>
        internal static IDisposable Step<T>(this IProfiler profiler, string name)
        {
            if (profiler == null) throw new ArgumentNullException(nameof(profiler));
            return profiler.Step(typeof(T), name);
        }

        /// <summary>
        /// Writes out a step prefixed with the type
        /// </summary>
        /// <param name="profiler">
        /// The <see cref="IProfiler"/>
        /// </param>
        /// <param name="objectType">
        /// The type of the step
        /// </param>
        /// <param name="name">
        /// The name
        /// </param>
        /// <returns>
        /// The disposable step
        /// </returns>
        internal static IDisposable Step(this IProfiler profiler, Type objectType, string name)
        {
            if (profiler == null) throw new ArgumentNullException(nameof(profiler));
            if (objectType == null) throw new ArgumentNullException(nameof(objectType));
            if (name == null) throw new ArgumentNullException(nameof(name));
            return profiler.Step(string.Format("[{0}] {1}", objectType.Name, name));
        }
    }
}