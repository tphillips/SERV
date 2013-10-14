using System;
using log4net;
using log4net.Config;

public class Logger
{
	
	public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

	public enum LogType
	{
		Info,
		Debug,
		Warn,
		Error,
		Fatal
	}

	public Logger()
	{
		XmlConfigurator.Configure();
		//Info("LOG CONFIGURED");
	}

	public void LogPerformace(DateTime started)
	{
		LogPerformace("", started, DateTime.Now, 2);
	}

	public void LogPerformace(string otherInfo, DateTime started)
	{
		LogPerformace(otherInfo, started, DateTime.Now, 2);
	}

	public void LogPerformace(string otherInfo, DateTime started, DateTime ended)
	{
		LogPerformace(otherInfo, started, ended, 2);
	}

	private void LogPerformace(string otherInfo, DateTime started, DateTime ended, int frames)
	{
		System.Diagnostics.StackFrame f = new System.Diagnostics.StackTrace().GetFrame(frames);
		System.Reflection.MethodBase m = f.GetMethod();
		Info(String.Format("PERFLOG: {0},{1},{2},{3},{4},{5}", m.DeclaringType.Name, m.Name, otherInfo, started.ToLongTimeString(), ended.ToLongTimeString(), (ended - started).Milliseconds.ToString()));
	}

	public DateTime LogStart()
	{
		if (Log.IsInfoEnabled)
		{
			System.Diagnostics.StackFrame f = new System.Diagnostics.StackTrace().GetFrame(1);
			System.Reflection.MethodBase m = f.GetMethod();
			Debug(m.DeclaringType.Name + "." + m.Name + "() START --------------------");
		}
		return DateTime.Now;
	}

	public void LogEnd()
	{
		if (Log.IsInfoEnabled)
		{
			System.Diagnostics.StackFrame f = new System.Diagnostics.StackTrace().GetFrame(1);
			System.Reflection.MethodBase m = f.GetMethod();
			Debug(m.DeclaringType.Name + "." + m.Name + "() END ----------------------");
		}
	}

	public void Info(object message)
	{
		if (Log.IsInfoEnabled) { Log.Info(message); }
	}

	public void Debug(object message)
	{
		if (Log.IsDebugEnabled) { Log.Debug(message); }
	}

	public void Warn(object message)
	{
		if (Log.IsWarnEnabled) { Log.Warn(message); }
	}

	public void Error(object message, Exception ex)
	{
		if (Log.IsErrorEnabled) 
		{
			if (ex == null) { Log.Error(message); return; }
			Log.Error(message, ex); 
		}
	}

	public void Fatal(object message, Exception ex)
	{
		if (Log.IsFatalEnabled) 
		{
			if (ex == null) { Log.Fatal(message); return; }
			Log.Fatal(message, ex); 
		}
	}

	/// <summary>
	/// Logs the message
	/// </summary>
	/// <param name="logType"></param>
	/// <param name="message"></param>
	/// <param name="ex">Exception to be specified in case of Fatal or Error LogType</param>
	public void LogMessage(LogType logType, object message, Exception ex)
	{
		if (logType == LogType.Info)
		{
			Info(message);
			return;
		}
		if (logType == LogType.Debug)
		{
			Debug(message);
			return;
		}
		if (logType == LogType.Error)
		{
			Error(message, ex);
			return;
		}
		if (logType == LogType.Fatal)
		{
			Fatal(message, ex);
			return;
		}
		if (logType == LogType.Warn)
		{
			Warn(message);
			return;
		}
	}

	/// <summary>
	/// Logs the message
	/// </summary>
	/// <param name="logType"></param>
	/// <param name="message"></param>
	public void LogMessage(LogType logType, object message)
	{
		LogMessage(logType, message, null);
	}

}

