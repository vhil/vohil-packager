namespace Pintle.Packager.Logging
{
	using System;
	using Sitecore.Configuration;
	using Sitecore.Diagnostics;
	using log4net;

	public class Logger
	{
		public string LoggerName { get; }
		private readonly ILog log;

		public Logger(string loggerName)
		{
			this.LoggerName = loggerName;
			this.log = LoggerFactory.GetLogger(loggerName);
		}

		public static Logger ConfiguredInstance => Factory.CreateObject("pintle.packager/logger", true) as Logger;

		#region instance methods

		public virtual void Debug(string message, object owner)
		{
			message = this.FormatMessage(message, owner);
			this.log.Debug(message);
		}

		public virtual void Error(string message, object owner)
		{
			message = this.FormatMessage(message, owner);
			this.log.Error(message);
		}

		public virtual void Error(string message, Exception exception, object owner)
		{
			message = this.FormatMessage(message, owner);
			this.log.Error(message, exception);
		}

		public virtual void Fatal(string message, object owner)
		{
			message = this.FormatMessage(message, owner);
			this.log.Fatal(message);
		}

		public virtual void Fatal(string message, Exception exception, object owner)
		{
			message = this.FormatMessage(message, owner);
			this.log.Fatal(message, exception);
		}

		public virtual void Info(string message, object owner)
		{
			message = this.FormatMessage(message, owner);
			this.log.Info(message);
		}

		public virtual void Warn(string message, object owner)
		{
			message = this.FormatMessage(message, owner);
			this.log.Warn(message);
		}

		public virtual void Warn(string message, Exception exception, object owner)
		{
			message = this.FormatMessage(message, owner);
			this.log.Warn(message, exception);
		}

		protected virtual string FormatMessage(string message, object owner)
		{
			if (owner == null)
			{
				return "[NULL]" + message;
			}
			return "[" + owner.GetType().Name + "] " + message;
		}

		#endregion
	}
}