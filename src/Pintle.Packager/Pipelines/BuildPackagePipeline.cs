namespace Pintle.Packager.Pipelines
{
	using Sitecore.Pipelines;
	using Configuration;
	using System.Collections.Generic;
	using System.Collections.Specialized;

	public class BuildPackagePipeline
	{
		public static BuildPackageArgs BuildPackage(
			PackageConfiguration packageConfiguration,
			IDictionary<string, string> parameters)
		{
			var args = new BuildPackageArgs(packageConfiguration, parameters);
			CorePipeline.Run("pintle.buildPackage", args);
			return args;
		}

		public static BuildPackageArgs BuildPackage(
			PackageConfiguration packageConfiguration,
			NameValueCollection parameters)
		{
			var args = new BuildPackageArgs(packageConfiguration, parameters);
			CorePipeline.Run("pintle.buildPackage", args);
			return args;
		}
	}
}