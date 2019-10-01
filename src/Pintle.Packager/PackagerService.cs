namespace Pintle.Packager
{
	using Configuration;
	using Pipelines;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System;
	using Sitecore.Diagnostics;

	public class PackagerService
	{
		public virtual BuildPackageResult BuildPackage(PackageConfiguration packageConfiguration, IDictionary<string, string> parameters)
		{
			try
			{
				var args = BuildPackagePipeline.BuildPackage(packageConfiguration, parameters);
				return new BuildPackageResult(args);
			}
			catch (Exception ex)
			{
				Log.Error("[Pintle.Packager]: Unable to build package '" + packageConfiguration.Name + "'.", ex, this);
				return new BuildPackageResult(ex);
			}
		}

		public virtual BuildPackageResult BuildPackage(string packageName, NameValueCollection parameters)
		{
			try
			{
				var packageConfiguration = PackageConfiguration.GetConfiguredPackage(packageName);
				var args = BuildPackagePipeline.BuildPackage(packageConfiguration, parameters);
				return new BuildPackageResult(args);
			}
			catch (Exception ex)
			{
				Log.Error("[Pintle.Packager]: Unable to build package '" + packageName + "'.", ex, this);
				return new BuildPackageResult(ex);
			}
		}
	}
}