namespace Pintle.Packager
{
	using Configuration;
	using Pipelines;

	public class PackagerService
	{
		public BuildPackageArgs BuildPackage(PackageConfiguration packageConfiguration)
		{
			return BuildPackagePipeline.BuildPackage(packageConfiguration);
		}
	}
}