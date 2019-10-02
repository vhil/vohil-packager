namespace Pintle.Packager
{
	using Configuration;
	using Pipelines;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System;
	using Logging;

	public class PackagerService
	{
		protected Logger Log = Logger.ConfiguredInstance;

		public virtual BuildPackageResult BuildPackage(PackageConfiguration packageConfiguration, IDictionary<string, string> parameters)
		{
			try
			{
				this.Log.Info("Start generating package '" + packageConfiguration.Name + "'...", this);

				var args = BuildPackagePipeline.BuildPackage(packageConfiguration, parameters);
				var result = new BuildPackageResult(args);
				this.LogResult(result);
				return result;
			}
			catch (Exception ex)
			{
				this.Log.Error("Unable to build package '" + packageConfiguration.Name + "'.", ex, this);
				return new BuildPackageResult(ex);
			}
		}

		public virtual BuildPackageResult BuildPackage(string packageName, NameValueCollection parameters)
		{
			try
			{
				this.Log.Info("Start generating package '" + packageName + "'...", this);

				var packageConfiguration = PackageConfiguration.GetConfiguredPackage(packageName);
				var args = BuildPackagePipeline.BuildPackage(packageConfiguration, parameters);
				var result = new BuildPackageResult(args);
				this.LogResult(result);
				return result;
			}
			catch (Exception ex)
			{
				this.Log.Error("Unable to build package '" + packageName + "'.", ex, this);
				return new BuildPackageResult(ex);
			}
		}

		protected void LogResult(BuildPackageResult result)
		{
			if (result.Success)
			{
				this.Log.Info("Package '" + result.PackageFileName + "' generated successfully.", this);
			}
			else
			{
				this.Log.Warn("Package generation was not successful.", this);

				foreach (var error in result.Errors)
				{
					this.Log.Error("errorKey: '" + error.Key + "', message: " + error.Value, this);
				}
			}
		}
	}
}