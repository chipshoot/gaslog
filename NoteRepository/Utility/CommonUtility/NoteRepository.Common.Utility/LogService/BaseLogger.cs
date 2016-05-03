using System;
using log4net.Core;

namespace NoteRepository.Common.Utility.LogService
{
    public abstract class BaseLogger : ILogService
    {
        #region public properties

        public abstract bool IsDebugEnabled { get; }

        public abstract bool IsInfoEnabled { get; }

        public abstract bool IsWarnEnabled { get; }

        public abstract bool IsErrorEnabled { get; }

        public abstract bool IsFatalEnabled { get; }

        #endregion public properties

        #region public methods

        public virtual void Debug(Func<string> formattingCallback)
        {
            if (IsDebugEnabled)
            {
                Debug(formattingCallback());
            }
        }

        public virtual void Info(Func<string> formattingCallback)
        {
            if (IsInfoEnabled)
            {
                Info(formattingCallback());
            }
        }

        public virtual void Warn(Func<string> formattingCallback)
        {
            if (IsWarnEnabled)
            {
                Warn(formattingCallback());
            }
        }

        public virtual void Error(Func<string> formattingCallback)
        {
            if (IsErrorEnabled)
            {
                Error(formattingCallback());
            }
        }

        public virtual void Fatal(Func<string> formattingCallback)
        {
            if (IsFatalEnabled)
            {
                Fatal(formattingCallback());
            }
        }

        #endregion public methods

        #region Implementation of ILog

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
        /// <see cref="ILogService.Debug(object,Exception)"/> form instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Debug(object,Exception)"/>
        /// <seealso cref="ILogService.IsDebugEnabled"/>
        public abstract void Debug(object message);

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
        public abstract void Debug(object message, Exception exception);

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
        public abstract void DebugFormat(string format, params object[] args);

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
        public abstract void DebugFormat(string format, object arg0);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Debug"/> level.
        /// </summary>
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
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">First Object to format</param>
        /// <param name="arg1">Second Object to format</param>
        public abstract void DebugFormat(string format, object arg0, object arg1);

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
        public abstract void DebugFormat(string format, object arg0, object arg1, object arg2);

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
        public abstract void DebugFormat(IFormatProvider provider, string format, params object[] args);

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
        public abstract void Info(object message);

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
        public abstract void Info(object message, Exception exception);

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
        public abstract void InfoFormat(string format, params object[] args);

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
        public abstract void InfoFormat(string format, object arg0);

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
        public abstract void InfoFormat(string format, object arg0, object arg1);

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
        public abstract void InfoFormat(string format, object arg0, object arg1, object arg2);

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
        public abstract void InfoFormat(IFormatProvider provider, string format, params object[] args);

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
        public abstract void Warn(object message);

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
        public abstract void Warn(object message, Exception exception);

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
        public abstract void WarnFormat(string format, params object[] args);

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
        public abstract void WarnFormat(string format, object arg0);

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
        public abstract void WarnFormat(string format, object arg0, object arg1);

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
        public abstract void WarnFormat(string format, object arg0, object arg1, object arg2);

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
        public abstract void WarnFormat(IFormatProvider provider, string format, params object[] args);

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
        public abstract void Error(object message);

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
        public abstract void Error(object message, Exception exception);

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
        public abstract void ErrorFormat(string format, params object[] args);

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
        public abstract void ErrorFormat(string format, object arg0);

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Error"/> level.
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
        /// log event. To pass an <see cref="Exception"/> use one of the <see cref="ILogService.Error(object,Exception)"/>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="ILogService.Error(object)"/>
        /// <seealso cref="ILogService.IsErrorEnabled"/>
        public abstract void ErrorFormat(string format, object arg0, object arg1);

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
        public abstract void ErrorFormat(string format, object arg0, object arg1, object arg2);

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
        public abstract void ErrorFormat(IFormatProvider provider, string format, params object[] args);

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
        public abstract void Fatal(object message);

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
        public abstract void Fatal(object message, Exception exception);

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
        public abstract void FatalFormat(string format, params object[] args);

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
        public abstract void FatalFormat(string format, object arg0);

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
        public abstract void FatalFormat(string format, object arg0, object arg1);

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
        public abstract void FatalFormat(string format, object arg0, object arg1, object arg2);

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
        public abstract void FatalFormat(IFormatProvider provider, string format, params object[] args);

        #endregion Implementation of ILog

        #region protected methods

        protected virtual string LogMessage(string message)
        {
            return message;
        }

        #endregion protected methods
    }
}