using Sitecore.Install.Metadata;

namespace Pintle.Packager.Pipelines.BuildPackage
{
	public class SetPackageMetadata : BuildPackageProcessor
	{
		public override void Process(BuildPackageArgs args)
		{
			args.Package.Metadata = new MetadataSource
			{
				PackageName = args.PackageConfiguration.Metadata.PackageName,
				Author = args.PackageConfiguration.Metadata.Author,
				Version = args.PackageConfiguration.Metadata.Version,
				Publisher = args.PackageConfiguration.Metadata.Publisher,
				Comment = args.PackageConfiguration.Metadata.Comment,
				License = args.PackageConfiguration.Metadata.License,
				PackageID = args.PackageConfiguration.Metadata.PackageId,
				Readme = args.PackageConfiguration.Metadata.Readme,
				Revision = args.PackageConfiguration.Metadata.Revision
			};
		}
	}
}