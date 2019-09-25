namespace Pintle.Packager
{
	using Configuration;
	using Pipelines;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System;

	public class PackagerService
	{
		public BuildPackageResult BuildPackage(PackageConfiguration packageConfiguration, IDictionary<string, string> parameters)
		{
			try
			{
				var args = BuildPackagePipeline.BuildPackage(packageConfiguration, parameters);
				return new BuildPackageResult(args);
			}
			catch (Exception ex)
			{
				return new BuildPackageResult(ex);
			}
		}

		public BuildPackageResult BuildPackage(string packageName, NameValueCollection parameters)
		{
			try
			{
				var packageConfiguration = PackageConfiguration.GetConfiguredPackage(packageName);
				var args = BuildPackagePipeline.BuildPackage(packageConfiguration, parameters);
				return new BuildPackageResult(args);
			}
			catch (Exception ex)
			{
				return new BuildPackageResult(ex);
			}
		}
	}
}