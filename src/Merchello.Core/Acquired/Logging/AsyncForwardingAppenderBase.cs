namespace Merchello.Core.Acquired.Logging
{
    using System;
    using System.Runtime.Remoting.Messaging;

    using log4net.Appender;
    using log4net.Core;
    using log4net.Util;

    using Merchello.Core.Logging;

    /// <summary>
    /// Borrowed from https://github.com/cjbhaines/Log4Net.Async - will reference Nuget packages directly in v8 REFACTOR remove when V8 Released
    /// </summary>
    /// UMBRACO_SRC
    public abstract class AsyncForwardingAppenderBase : ForwardingAppender
    {
        #region Private Members

        private const FixFlags DefaultFixFlags = FixFlags.Partial;
        private FixFlags fixFlags = DefaultFixFlags;
        private LoggingEventHelper loggingEventHelper;

        #endregion Private Members

        #region Properties

        public FixFlags Fix
        {
            get { return this.fixFlags; }
            set { this.SetFixFlags(value); }
        }

        /// <summary>
        /// Returns HttpContext.Current
        /// </summary>
        protected internal object HttpContext
        {
            get
            {
                return CallContext.HostContext;
            }
            set
            {
                CallContext.HostContext = value;
            }
        }

        /// <summary>
        /// The logger name that will be used for logging internal errors.
        /// </summary>
        protected abstract string InternalLoggerName { get; }

        public abstract int? BufferSize { get; set; }

        #endregion Properties

        public override void ActivateOptions()
        {
            base.ActivateOptions();
            this.loggingEventHelper = new LoggingEventHelper(this.InternalLoggerName, DefaultFixFlags);
            this.InitializeAppenders();
        }

        #region Appender Management

        public override void AddAppender(IAppender newAppender)
        {
            base.AddAppender(newAppender);
            this.SetAppenderFixFlags(newAppender);
        }

        private void SetFixFlags(FixFlags newFixFlags)
        {
            if (newFixFlags != this.fixFlags)
            {
                this.loggingEventHelper.Fix = newFixFlags;
                this.fixFlags = newFixFlags;
                this.InitializeAppenders();
            }
        }

        private void InitializeAppenders()
        {
            foreach (var appender in this.Appenders)
            {
                this.SetAppenderFixFlags(appender);
            }
        }

        private void SetAppenderFixFlags(IAppender appender)
        {
            var bufferingAppender = appender as BufferingAppenderSkeleton;
            if (bufferingAppender != null)
            {
                bufferingAppender.Fix = this.Fix;
            }
        }

        #endregion Appender Management

        #region Forwarding

        protected void ForwardInternalError(string message, Exception exception, Type thisType)
        {
            LogLog.Error(thisType, message, exception);
            var loggingEvent = this.loggingEventHelper.CreateLoggingEvent(Level.Error, message, exception);
            this.ForwardLoggingEvent(loggingEvent, thisType);
        }

        protected void ForwardLoggingEvent(LoggingEvent loggingEvent, Type thisType)
        {
            try
            {
                base.Append(loggingEvent);
            }
            catch (Exception exception)
            {
                LogLog.Error(thisType, "Unable to forward logging event", exception);
            }
        }

        #endregion Forwarding
    }
}