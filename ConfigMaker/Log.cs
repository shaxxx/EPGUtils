using ConfigMaker.Properties;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConfigMaker
{
    public static class Log
    {
        private static readonly object Locker = new object();
        private static bool _configured;
        private static readonly ILog Log4NetLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Configure()
        {
            if (_configured) return;
            lock (Locker)
            {
                var baseDir = Directory.GetCurrentDirectory();
                var name = Assembly.GetExecutingAssembly().GetName().Name;
                var logFile = Path.Combine(baseDir, name + ".log4net");
                if (File.Exists(logFile))
                {
                    log4net.Config.XmlConfigurator.Configure(new FileInfo(logFile));
                }
                else
                {
                    log4net.Config.XmlConfigurator.Configure(new MemoryStream(Resources.log4netconfig));
                    try
                    {
                        var stream = new MemoryStream(Resources.log4netconfig);
                        File.WriteAllBytes(logFile, Resources.log4netconfig);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                _configured = true;
            }
        }

        public static void Debug(object message)
        {
            Log4NetLogger.Debug(message);
        }

        public static void Debug(object message, Exception exception)
        {
            Log4NetLogger.Debug(message, exception);
        }

        public static void DebugFormat(string format, params object[] args)
        {
            Log4NetLogger.DebugFormat(format, args);
        }

        public static void DebugFormat(string format, object arg0)
        {
            Log4NetLogger.DebugFormat(format, arg0);
        }

        public static void DebugFormat(string format, object arg0, object arg1)
        {
            Log4NetLogger.DebugFormat(format, arg0, arg1);
        }

        public static void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            Log4NetLogger.DebugFormat(format, arg0, arg1, arg2);
        }

        public static void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log4NetLogger.DebugFormat(provider, format, args);
        }

        public static void Info(object message)
        {
            Log4NetLogger.Info(message);
        }

        public static void Info(object message, Exception exception)
        {
            Log4NetLogger.Info(message, exception);
        }

        public static void InfoFormat(string format, params object[] args)
        {
            Log4NetLogger.InfoFormat(format, args);
        }

        public static void InfoFormat(string format, object arg0)
        {
            Log4NetLogger.InfoFormat(format, arg0);
        }

        public static void InfoFormat(string format, object arg0, object arg1)
        {
            Log4NetLogger.InfoFormat(format, arg0, arg1);
        }

        public static void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            Log4NetLogger.InfoFormat(format, arg0, arg1, arg2);
        }

        public static void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log4NetLogger.InfoFormat(provider, format, args);
        }

        public static void Warn(object message)
        {
            Log4NetLogger.Warn(message);
        }

        public static void Warn(object message, Exception exception)
        {
            Log4NetLogger.Warn(message, exception);
        }

        public static void WarnFormat(string format, params object[] args)
        {
            Log4NetLogger.WarnFormat(format, args);
        }

        public static void WarnFormat(string format, object arg0)
        {
            Log4NetLogger.WarnFormat(format, arg0);
        }

        public static void WarnFormat(string format, object arg0, object arg1)
        {
            Log4NetLogger.WarnFormat(format, arg0, arg1);
        }

        public static void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            Log4NetLogger.WarnFormat(format, arg0, arg1, arg2);
        }

        public static void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log4NetLogger.WarnFormat(provider, format, args);
        }

        public static void Error(object message)
        {
            Log4NetLogger.Error(message);
        }

        public static void Error(object message, Exception exception)
        {
            Log4NetLogger.Error(message, exception);
        }

        public static void ErrorFormat(string format, params object[] args)
        {
            Log4NetLogger.ErrorFormat(format, args);
        }

        public static void ErrorFormat(string format, object arg0)
        {
            Log4NetLogger.ErrorFormat(format, arg0);
        }

        public static void ErrorFormat(string format, object arg0, object arg1)
        {
            Log4NetLogger.ErrorFormat(format, arg0, arg1);
        }

        public static void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            Log4NetLogger.ErrorFormat(format, arg0, arg1, arg2);
        }

        public static void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log4NetLogger.ErrorFormat(provider, format, args);
        }

        public static void Fatal(object message)
        {
            Log4NetLogger.Fatal(message);
        }

        public static void Fatal(object message, Exception exception)
        {
            Log4NetLogger.Fatal(message, exception);
        }

        public static void FatalFormat(string format, params object[] args)
        {
            Log4NetLogger.FatalFormat(format, args);
        }

        public static void FatalFormat(string format, object arg0)
        {
            Log4NetLogger.FatalFormat(format, arg0);
        }

        public static void FatalFormat(string format, object arg0, object arg1)
        {
            Log4NetLogger.FatalFormat(format, arg0, arg1);
        }

        public static void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            Log4NetLogger.FatalFormat(format, arg0, arg1, arg2);
        }

        public static void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log4NetLogger.FatalFormat(provider, format, args);
        }

        public static bool IsDebugEnabled => Log4NetLogger.IsDebugEnabled;

        public static bool IsInfoEnabled => Log4NetLogger.IsInfoEnabled;

        public static bool IsWarnEnabled => Log4NetLogger.IsWarnEnabled;

        public static bool IsErrorEnabled => Log4NetLogger.IsErrorEnabled;

        public static bool IsFatalEnabled => Log4NetLogger.IsFatalEnabled;

        public static ILogger Logger => ((ILoggerWrapper)Log4NetLogger).Logger;
    }
}
