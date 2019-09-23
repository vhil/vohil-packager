using Pintle.Packager.Configuration;

namespace Pintle.Packager.Pipelines
{
	using Sitecore.Pipelines;

    public class BuildPackageArgs : PipelineArgs
    {
        public PackageConfiguration PackageConfiguration { get; }

        public BuildPackageArgs(PackageConfiguration packageConfiguration)
        {
            this.PackageConfiguration = packageConfiguration;
        }
    }
}