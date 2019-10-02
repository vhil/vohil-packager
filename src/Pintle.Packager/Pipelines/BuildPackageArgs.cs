namespace Pintle.Packager.Pipelines
{
	using Configuration;
	using Sitecore.Install;
	using Sitecore.Install.Files;
	using Sitecore.Install.Framework;
	using Sitecore.Install.Items;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using Extensions;
	using Sitecore.Pipelines;

	public class BuildPackageArgs : PipelineArgs
	{
		public PackageConfiguration PackageConfiguration { get; }
		public IDictionary<string, string> Errors { get; }
		public IDictionary<string, string> Parameters { get; }
		public PackageProject Package { get; }
		public ExplicitItemSource PackageItems { get; }
		public ExplicitFileSource PackageFiles { get; }
		public SourceCollection<PackageEntry> PackageSources { get; }
		public string PackageFilePath { get; set; }

		public BuildPackageArgs(PackageConfiguration packageConfiguration, NameValueCollection parameters)
			: this(packageConfiguration, parameters.ToDictionary())
		{
		}

		public BuildPackageArgs(PackageConfiguration packageConfiguration, IDictionary<string, string> parameters)
		{
			this.Errors = new Dictionary<string, string>();
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

			this.PackageSources = new SourceCollection<PackageEntry>();

			this.PackageSources.Add(this.PackageItems);
		}
	}
}