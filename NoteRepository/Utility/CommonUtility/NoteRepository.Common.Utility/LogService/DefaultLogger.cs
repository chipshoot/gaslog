using System;
using System.Diagnostics;
using System.Globalization;
using log4net;
using log4net.Core;

namespace NoteRepository.Common.Utility.LogService
{
    public class DefaultLogger : BaseLogger, ILogService
    {
        #region private fields

        private readonly ILog _logger;

        #endregion private fields

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultLogger"/> class.
        /// </summary>
        public DefaultLogger() : this("defaultlogger")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultLogger"/> class with class name.
        /// </summary>
        /// <param name="className">The class name which logger will put in log string</param>
        public DefaultLogger(string className)
        {
            log4net.Config.XmlConfigurator.Configure();
            _logger = LogManager.GetLogger(className);
        }

        #endregion constructors

        #region overrides of BaseLogger

        /// <summary>
        ///  Checks if this logger is enabled for the <see cref="F:log4net.Core.Level.Debug" /> level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this logger is enabled for <see cref="F:log4net.Core.Level.Debug" /> events,
        /// <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        /// <para>
        ///  This function is intended to lessen the computational cost of
        ///  disabled log debug statements.
        /// </para>
        /// <para>
        ///  For some ILogService interface
        /// <c>log</c>, when you write:
        /// </para>
        /// <code>
        ///  log.Debug("This is entry number: " + i );
        /// </code>
        /// <para>
        ///  You incur the cost constructing the message, string construction and concatenation in
        ///  this case, regardless of whether the message is logged or not.
        /// </para>
        /// <para>
        ///  If you are worried about speed (who isn't), then you should write:
        /// </para>
        /// <code>
        ///  if (log.IsDebugEnabled)
        ///  {
        ///      log.Debug("This is entry number: " + i );
        ///  }
        /// </code>
        /// <para>
        ///  This way you will not incur the cost of parameter
        ///  construction if debugging is disabled for
        /// <c>log</c>. On
        ///  the other hand, if the
        /// <c>log</c> is debug enabled, you
        ///  will incur the cost of evaluating whether the logger is debug
        ///  since evaluating a logger takes about 1% of the time it
        ///  takes to actually log. This is the preferred style of logging.
        /// </para>
        /// <para>
        /// Alternatively if your logger is available statically then the is debug
        ///  enabled state can be stored in a static variable like this:
        /// </para>
        /// <code>
        ///  private static read-only bool isDebugEnabled = log.IsDebugEnabled;
        /// </code>
        /// <para>
        ///  Then when you come to log you can write:
        /// </para>
        /// <code>
        ///  if (isDebugEnabled)
        ///  {
        ///      log.Debug("This is entry number: " + i );
        ///  }
        /// </code>
        /// <para>
        ///  This way the debug enabled state is only queried once
        ///  when the class is loaded. Using a
        /// <c>private static read-only</c>
        ///  variable is the most efficient because it is a run time constant
        ///  and can be heavily optimized by the JIT compiler.
        /// </para>
        /// <para>
        ///  Of course if you use a static read-only variable to
        ///  hold the enabled state of the logger then you cannot
        ///  change the enabled state at runtime to vary the logging
        ///  that is produced. You have to decide if you need absolute
        ///  speed or runtime flexibility.
        /// </para>
        /// </remarks>
        public override bool IsDebugEnabled
        {
            get { return _logger.IsDebugEnabled; }
        }

        /// <summary>
        ///  Checks if this logger is enabled for the <see cref="F:log4net.Core.Level.Info" /> level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this logger is enabled for <see cref="F:log4net.Core.Level.Info" /> events,
        /// <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        ///  For more information see <see cref="P:log4net.ILog.IsDebugEnabled" />.
        /// </remarks>
        /// <seealso cref="P:log4net.ILog.IsDebugEnabled" />
        public override bool IsInfoEnabled
        {
            get { return _logger.IsInfoEnabled; }
        }

        /// <summary>
        ///  Checks if this logger is enabled for the <see cref="F:log4net.Core.Level.Warn" /> level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this logger is enabled for <see cref="F:log4net.Core.Level.Warn" /> events,
        /// <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        ///  For more information see <see cref="P:log4net.ILog.IsDebugEnabled" />.
        /// </remarks>
        /// <seealso cref="P:log4net.ILog.IsDebugEnabled" />
        public override bool IsWarnEnabled
        {
            get { return _logger.IsWarnEnabled; }
        }

        /// <summary>
        ///  Checks if this logger is enabled for the <see cref="F:log4net.Core.Level.Error" /> level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this logger is enabled for <see cref="F:log4net.Core.Level.Error" /> events,
        /// <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        ///  For more information see <see cref="P:log4net.ILog.IsDebugEnabled" />.
        /// </remarks>
        /// <seealso cref="P:log4net.ILog.IsDebugEnabled" />
        public override bool IsErrorEnabled
        {
            get { return _logger.IsErrorEnabled; }
        }

        /// <summary>
        ///  Checks if this logger is enabled for the <see cref="F:log4net.Core.Level.Fatal" /> level.
        /// </summary>
        /// <value>
        /// <c>true</c> if this logger is enabled for <see cref="F:log4net.Core.Level.Fatal" /> events,
        /// <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        ///  For more information see <see cref="P:log4net.ILog.IsDebugEnabled" />.
        /// </remarks>
        /// <seealso cref="P:log4net.ILog.IsDebugEnabled" />
        public override bool IsFatalEnabled
        {
            get { return _logger.IsFatalEnabled; }
        }

        /// <overloads>Log a message object with the <see cref="Level.Debug"/> level.</overloads>
        /// <summary>
        /// Log a message object with the <see cref="Level.Debug"/> level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>DEBUG</c>
        /// enabled by comparing the level of this logger with the
        /// <see cref="Level.Debug"/> level. If this logger is
        /// <c>DEBUG</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="log4net.ObjectRenderer.IObjectRenderer"/>. It then
        /// proceeds to call all the registered appenders in this logger
        /// and also higher in the hierarchy depending on the value of
        /// the additive flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="Exception"/>
        /// to this method will print the name of the <see cref="Exception"/>
        /// but no stack trace. To print a stack trace use the
        /// <see cref="ILogService.Debug(object,System.Exception)"/> form instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Debug(object,Exception)"/>
        /// <seealso cref="ILogService.IsDebugEnabled"/>
        public override void Debug(object message)
        {
            if (IsDebugEnabled)
            {
                _logger.Debug(LogMessage(message.ToString()));
            }
        }

        /// <summary>
        /// Log a message object with the <see cref="Level.Debug"/> level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="ILogService.Debug(object)"/> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Debug(object)"/>
        /// <seealso cref="ILogService.IsDebugEnabled"/>
        public override void Debug(object message, Exception exception)
        {
            if (IsDebugEnabled)
            {
                _logger.Debug(LogMessage(message.ToString()), exception);
            }
        }

        /// <overloads>Log a formatted string with the <see cref="Level.Debug"/> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Debug"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Debug(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Debug(object)"/>
        /// <seealso cref="ILogService.IsDebugEnabled"/>
        public override void DebugFormat(string format, params object[] args)
        {
            if (IsDebugEnabled)
            {
                _logger.DebugFormat(LogMessage(format), args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Debug"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Debug(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Debug(object)"/>
        /// <seealso cref="ILogService.IsDebugEnabled"/>
        public override void DebugFormat(string format, object arg0)
        {
            if (IsDebugEnabled)
            {
                _logger.DebugFormat(LogMessage(format), arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Debug"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">First Object to format</param>
        /// <param name="arg1">Second Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Debug(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Debug(object)"/>
        /// <seealso cref="ILogService.IsDebugEnabled"/>
        public override void DebugFormat(string format, object arg0, object arg1)
        {
            if (IsDebugEnabled)
            {
                _logger.DebugFormat(LogMessage(format), arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Debug"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">First Object to format</param>
        /// <param name="arg1">Second Object to format</param>
        /// <param name="arg2">Third Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Debug(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Debug(object)"/>
        /// <seealso cref="ILogService.IsDebugEnabled"/>
        public override void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            if (IsDebugEnabled)
            {
                _logger.DebugFormat(LogMessage(format), arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Debug"/> level.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Debug(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Debug(object)"/>
        /// <seealso cref="ILogService.IsDebugEnabled"/>
        public override void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (IsDebugEnabled)
            {
                _logger.DebugFormat(provider, LogMessage(format), args);
            }
        }

        /// <overloads>Log a message object with the <see cref="Level.Info"/> level.</overloads>
        /// <summary>
        /// Logs a message object with the <see cref="Level.Info"/> level.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>INFO</c>
        /// enabled by comparing the level of this logger with the
        /// <see cref="Level.Info"/> level. If this logger is
        /// <c>INFO</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="log4net.ObjectRenderer.IObjectRenderer"/>. It then
        /// proceeds to call all the registered appenders in this logger
        /// and also higher in the hierarchy depending on the value of the
        /// additive flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="Exception"/>
        /// to this method will print the name of the <see cref="Exception"/>
        /// but no stack trace. To print a stack trace use the
        /// <see cref="ILogService.Info(object,Exception)"/> form instead.
        /// </para>
        /// </remarks>
        /// <param name="message">The message object to log.</param>
        /// <seealso cref="ILogService.Info(object,Exception)"/>
        /// <seealso cref="ILogService.IsInfoEnabled"/>
        public override void Info(object message)
        {
            if (IsErrorEnabled)
            {
                _logger.Info(LogMessage(message.ToString()));
            }
        }

        /// <summary>
        /// Logs a message object with the <c>INFO</c> level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="ILogService.Info(object)"/> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Info(object)"/>
        /// <seealso cref="ILogService.IsInfoEnabled"/>
        public override void Info(object message, Exception exception)
        {
            if (IsInfoEnabled)
            {
                _logger.Info(LogMessage(message.ToString()), exception);
            }
        }

        /// <overloads>Log a formatted message string with the <see cref="Level.Info"/> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Info"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Info(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Info(object,Exception)"/>
        /// <seealso cref="ILogService.IsInfoEnabled"/>
        public override void InfoFormat(string format, params object[] args)
        {
            if (IsInfoEnabled)
            {
                _logger.InfoFormat(LogMessage(format), args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Info"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Info(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Info(object)"/>
        /// <seealso cref="ILogService.IsInfoEnabled"/>
        public override void InfoFormat(string format, object arg0)
        {
            if (IsInfoEnabled)
            {
                _logger.InfoFormat(LogMessage(format), arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Info"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">First Object to format</param>
        /// <param name="arg1">Second Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Info(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Info(object)"/>
        /// <seealso cref="ILogService.IsInfoEnabled"/>
        public override void InfoFormat(string format, object arg0, object arg1)
        {
            if (IsInfoEnabled)
            {
                _logger.InfoFormat(LogMessage(format), arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Info"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">First Object to format</param>
        /// <param name="arg1">Second Object to format</param>
        /// <param name="arg2">Third Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Info(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Info(object)"/>
        /// <seealso cref="ILogService.IsInfoEnabled"/>
        public override void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            if (IsInfoEnabled)
            {
                _logger.InfoFormat(LogMessage(format), arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Info"/> level.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Info(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Info(object,Exception)"/>
        /// <seealso cref="ILogService.IsInfoEnabled"/>
        public override void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (IsInfoEnabled)
            {
                _logger.InfoFormat(provider, LogMessage(format), args);
            }
        }

        /// <overloads>Log a message object with the <see cref="Level.Warn"/> level.</overloads>
        /// <summary>
        /// Log a message object with the <see cref="Level.Warn"/> level.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>WARN</c>
        /// enabled by comparing the level of this logger with the
        /// <see cref="Level.Warn"/> level. If this logger is
        /// <c>WARN</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="log4net.ObjectRenderer.IObjectRenderer"/>. It then
        /// proceeds to call all the registered appenders in this logger
        /// and also higher in the hierarchy depending on the value of the
        /// additive flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="Exception"/>
        /// to this method will print the name of the <see cref="Exception"/>
        /// but no stack trace. To print a stack trace use the
        /// <see cref="ILogService.Warn(object,Exception)"/> form instead.
        /// </para>
        /// </remarks>
        /// <param name="message">The message object to log.</param>
        /// <seealso cref="ILogService.Warn(object,Exception)"/>
        /// <seealso cref="ILogService.IsWarnEnabled"/>
        public override void Warn(object message)
        {
            if (IsWarnEnabled)
            {
                _logger.Warn(LogMessage(message.ToString()));
            }
        }

        /// <summary>
        /// Log a message object with the <see cref="Level.Warn"/> level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="ILogService.Warn(object)"/> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Warn(object)"/>
        /// <seealso cref="ILogService.IsWarnEnabled"/>
        public override void Warn(object message, Exception exception)
        {
            if (IsWarnEnabled)
            {
                _logger.Warn(LogMessage(message.ToString()), exception);
            }
        }

        /// <overloads>Log a formatted message string with the <see cref="Level.Warn"/> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Warn"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Warn(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Warn(object,Exception)"/>
        /// <seealso cref="ILogService.IsWarnEnabled"/>
        public override void WarnFormat(string format, params object[] args)
        {
            if (IsWarnEnabled)
            {
                _logger.WarnFormat(LogMessage(format), args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Warn"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Warn(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Warn(object)"/>
        /// <seealso cref="ILogService.IsWarnEnabled"/>
        public override void WarnFormat(string format, object arg0)
        {
            if (IsWarnEnabled)
            {
                _logger.WarnFormat(LogMessage(format), arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Warn"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">First Object to format</param>
        /// <param name="arg1">Second Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Warn(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Warn(object)"/>
        /// <seealso cref="ILogService.IsWarnEnabled"/>
        public override void WarnFormat(string format, object arg0, object arg1)
        {
            if (IsWarnEnabled)
            {
                _logger.WarnFormat(LogMessage(format), arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Warn"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">First Object to format</param>
        /// <param name="arg1">Second Object to format</param>
        /// <param name="arg2">Third Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Warn(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Warn(object)"/>
        /// <seealso cref="ILogService.IsWarnEnabled"/>
        public override void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            if (IsWarnEnabled)
            {
                _logger.WarnFormat(LogMessage(format), arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Warn"/> level.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Warn(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Warn(object,Exception)"/>
        /// <seealso cref="ILogService.IsWarnEnabled"/>
        public override void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (IsWarnEnabled)
            {
                _logger.WarnFormat(provider, LogMessage(format), args);
            }
        }

        /// <overloads>Log a message object with the <see cref="Level.Error"/> level.</overloads>
        /// <summary>
        /// Logs a message object with the <see cref="Level.Error"/> level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>ERROR</c>
        /// enabled by comparing the level of this logger with the
        /// <see cref="Level.Error"/> level. If this logger is
        /// <c>ERROR</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="log4net.ObjectRenderer.IObjectRenderer"/>. It then
        /// proceeds to call all the registered appenders in this logger
        /// and also higher in the hierarchy depending on the value of the
        /// additive flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="Exception"/>
        /// to this method will print the name of the <see cref="Exception"/>
        /// but no stack trace. To print a stack trace use the
        /// <see cref="ILogService.Error(object,Exception)"/> form instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Error(object,Exception)"/>
        /// <seealso cref="ILogService.IsErrorEnabled"/>
        public override void Error(object message)
        {
            if (IsErrorEnabled)
            {
                _logger.Error(LogMessage(message.ToString()));
            }
        }

        /// <summary>
        /// Log a message object with the <see cref="Level.Error"/> level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="ILogService.Error(object)"/> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Error(object)"/>
        /// <seealso cref="ILogService.IsErrorEnabled"/>
        public override void Error(object message, Exception exception)
        {
            if (IsErrorEnabled)
            {
                _logger.Error(LogMessage(message.ToString()), exception);
            }
        }

        /// <overloads>Log a formatted message string with the <see cref="Level.Error"/> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Error"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Error(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Error(object,Exception)"/>
        /// <seealso cref="ILogService.IsErrorEnabled"/>
        public override void ErrorFormat(string format, params object[] args)
        {
            if (IsErrorEnabled)
            {
                _logger.ErrorFormat(LogMessage(format), args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Error"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Error(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Error(object)"/>
        /// <seealso cref="ILogService.IsErrorEnabled"/>
        public override void ErrorFormat(string format, object arg0)
        {
            if (IsErrorEnabled)
            {
                _logger.ErrorFormat(LogMessage(format), arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Error"/> level.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Error(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// <seealso cref="ILogService.Error(object)"/>
        /// <seealso cref="ILogService.IsErrorEnabled"/>
        /// </remarks>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">First Object to format</param>
        /// <param name="arg1">Second Object to format</param>
        public override void ErrorFormat(string format, object arg0, object arg1)
        {
            if (IsErrorEnabled)
            {
                _logger.ErrorFormat(LogMessage(format), arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Error"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">First Object to format</param>
        /// <param name="arg1">Second Object to format</param>
        /// <param name="arg2">Third Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Error(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Error(object)"/>
        /// <seealso cref="ILogService.IsErrorEnabled"/>
        public override void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            if (IsErrorEnabled)
            {
                _logger.ErrorFormat(LogMessage(format), arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Error"/> level.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Error(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Error(object,Exception)"/>
        /// <seealso cref="ILogService.IsErrorEnabled"/>
        public override void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (IsErrorEnabled)
            {
                _logger.ErrorFormat(provider, LogMessage(format), args);
            }
        }

        /// <overloads>Log a message object with the <see cref="Level.Fatal"/> level.</overloads>
        /// <summary>
        /// Log a message object with the <see cref="Level.Fatal"/> level.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>FATAL</c>
        /// enabled by comparing the level of this logger with the
        /// <see cref="Level.Fatal"/> level. If this logger is
        /// <c>FATAL</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="log4net.ObjectRenderer.IObjectRenderer"/>. It then
        /// proceeds to call all the registered appenders in this logger
        /// and also higher in the hierarchy depending on the value of the
        /// additive flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="Exception"/>
        /// to this method will print the name of the <see cref="Exception"/>
        /// but no stack trace. To print a stack trace use the
        /// <see cref="ILogService.Fatal(object,Exception)"/> form instead.
        /// </para>
        /// </remarks>
        /// <param name="message">The message object to log.</param>
        /// <seealso cref="ILogService.Fatal(object,Exception)"/>
        /// <seealso cref="ILogService.IsFatalEnabled"/>
        public override void Fatal(object message)
        {
            if (IsFatalEnabled)
            {
                _logger.Fatal(LogMessage(message.ToString()));
            }
        }

        /// <summary>
        /// Log a message object with the <see cref="Level.Fatal"/> level including
        /// the stack trace of the <see cref="Exception"/> passed
        /// as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="ILogService.Fatal(object)"/> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Fatal(object)"/>
        /// <seealso cref="ILogService.IsFatalEnabled"/>
        public override void Fatal(object message, Exception exception)
        {
            if (IsFatalEnabled)
            {
                _logger.Fatal(LogMessage(message.ToString()), exception);
            }
        }

        /// <overloads>Log a formatted message string with the <see cref="Level.Fatal"/> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Fatal"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Fatal(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Fatal(object,Exception)"/>
        /// <seealso cref="ILogService.IsFatalEnabled"/>
        public override void FatalFormat(string format, params object[] args)
        {
            if (IsFatalEnabled)
            {
                _logger.FatalFormat(LogMessage(format), args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Fatal"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Fatal(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Fatal(object)"/>
        /// <seealso cref="ILogService.IsFatalEnabled"/>
        public override void FatalFormat(string format, object arg0)
        {
            if (IsFatalEnabled)
            {
                _logger.FatalFormat(LogMessage(format), arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Fatal"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">First Object to format</param>
        /// <param name="arg1">Second Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Fatal(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Fatal(object)"/>
        /// <seealso cref="ILogService.IsFatalEnabled"/>
        public override void FatalFormat(string format, object arg0, object arg1)
        {
            if (IsFatalEnabled)
            {
                _logger.FatalFormat(LogMessage(format), arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Fatal"/> level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">First Object to format</param>
        /// <param name="arg1">Second Object to format</param>
        /// <param name="arg2">Third Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Fatal(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Fatal(object)"/>
        /// <seealso cref="ILogService.IsFatalEnabled"/>
        public override void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            if (IsFatalEnabled)
            {
                _logger.FatalFormat(LogMessage(format), arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Fatal"/> level.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="String.Format(string, object[])"/> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="Exception"/> object to include in the
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Fatal(object)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Fatal(object,Exception)"/>
        /// <seealso cref="ILogService.IsFatalEnabled"/>
        public override void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (_logger.IsFatalEnabled)
            {
                _logger.FatalFormat(provider, format, args);
            }
        }

        #endregion overrides of BaseLogger

        #region override protected method of BaseLogger

        protected override string LogMessage(string message)
        {
            var methodInfo = new StackTrace().GetFrame(2).GetMethod();
            return string.Format(CultureInfo.InvariantCulture, "{0}.{1} - {2}", methodInfo.DeclaringType, methodInfo.Name, message);
        }

        #endregion override protected method of BaseLogger
    }
}