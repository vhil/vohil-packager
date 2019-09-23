using System.Collections.Generic;

namespace Pintle.Packager.Pipelines
{
	using Configuration;
	using Sitecore.Install;
	using Sitecore.Install.Files;
	using Sitecore.Install.Framework;
	using Sitecore.Install.Items;

	using Sitecore.Pipelines;

    public class BuildPackageArgs : PipelineArgs
    {
	    public PackageConfiguration PackageConfiguration { get; }
	    public IDictionary<string, string> Parameters { get; }
	    public PackageProject Package { get; }
		public ExplicitItemSource PackageItems { get; }
		public ExplicitFileSource PackageFiles { get; }
		public SourceCollection<PackageEntry> PackageSourses { get; }
		public string PackageFilePath { get; set; }
		public BuildPackageArgs(PackageConfiguration packageConfiguration, IDictionary<string, string> parameters)
        {
            this.PackageConfiguration = packageConfiguration;
            this.Parameters = parameters;
            this.Package = new PackageProject();

			this.PackageFiles = new ExplicitFileSource
			{
				Name = "Static files"
			};

			this.PackageItems = new ExplicitItemSource
			{
				Name = "Static items"
			};

			this.PackageSourses = new SourceCollection<PackageEntry>();

			this.PackageSourses.Add(this.PackageItems);
		}
    }
}