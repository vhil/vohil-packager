using System.Collections.Generic;

namespace Pintle.Packager
{
	using Configuration;
	using Pipelines;

	public class PackagerService
	{
		public BuildPackageArgs BuildPackage(PackageConfiguration packageConfiguration, IDictionary<string, string> parameters)
		{
			return BuildPackagePipeline.BuildPackage(packageConfiguration, parameters);
		}
	}
}