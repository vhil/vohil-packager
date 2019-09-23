using System.Collections.Generic;

namespace Pintle.Packager.Pipelines
{
	using Sitecore.Pipelines;
	using Configuration;

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
    }
}