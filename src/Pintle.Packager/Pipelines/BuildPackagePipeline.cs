namespace Pintle.Packager.Pipelines
{
	using Sitecore.Pipelines;
	using Configuration;

	public class BuildPackagePipeline
    {
        public static BuildPackageArgs BuildPackage(PackageConfiguration packageConfiguration)
        {
            var args = new BuildPackageArgs(packageConfiguration);
            CorePipeline.Run("pintle.buildPackage", args);
            return args;
        }
    }
}