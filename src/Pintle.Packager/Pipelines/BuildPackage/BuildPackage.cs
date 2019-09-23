namespace Pintle.Packager.Pipelines.BuildPackage
{
	using Sitecore;
	using Sitecore.Configuration;
	using Sitecore.Install;
	using Sitecore.Install.Files;
	using Sitecore.Install.Framework;
	using Sitecore.Install.Items;
	using Sitecore.Install.Zip;
	using System.IO;

	public class BuildPackage : AbstractBuildPackageProcessor
    {
        public override void Process(BuildPackageArgs args)
        {
			var packageProject = new PackageProject
	        {
		        Metadata =
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
		        }
	        };

	        //Creating Source for Files
	        var packageFileSource = new ExplicitFileSource
	        {
		        Name = "Static files"
	        };

	        //Creating Source for Items
	        var packageItemSource = new ExplicitItemSource
	        {
		        Name = "Static items"
	        };

	        //Instantiate a new SourceCollection
	        var sourceCollection = new SourceCollection<PackageEntry>();

	        //Add the Item Source to the SourceCollection
	        sourceCollection.Add(packageItemSource);

	        foreach (var itemConfig in args.PackageConfiguration.Items)
	        {
				var item = Factory.GetDatabase(itemConfig.Database).Items.GetItem(itemConfig.Path);

				if (item != null)
				{
					if (itemConfig.IncludeChildren)
					{
						sourceCollection.Add(new ItemSource
						{
							SkipVersions = true,
							Database = item.Uri.DatabaseName,
							Root = item.Uri.ItemID.ToString(),
						});
					}
					else
					{
						packageItemSource.Entries.Add(new ItemReference(item.Uri, false).ToString());
					}
				}
			}

	        foreach (var fileConfig in args.PackageConfiguration.Files)
	        {
				var pathMapped = MainUtil.MapPath(fileConfig.Path);

				packageFileSource.Entries.Add(pathMapped);
			}
			
	        if (packageFileSource.Entries.Count > 0)
	        {
		        packageProject.Sources.Add(packageFileSource);
	        }

	        if (packageItemSource.Entries.Count > 0 || sourceCollection.Sources.Count > 0)
	        {
		        packageProject.Sources.Add(sourceCollection);
	        }

	        packageProject.SaveProject = true;

	        var prevSite = Context.Site.Name;
			try
			{
		        Context.SetActiveSite("shell");

		        var packageStorage = MainUtil.MapPath(Settings.GetSetting("Pintle.PackageStoragePath", "/packager"));
				if (!Directory.Exists(packageStorage))
				{
					Directory.CreateDirectory(packageStorage);
				}

				var fileName = $"{args.PackageConfiguration.Metadata.PackageName} v.{args.PackageConfiguration.Metadata.Version} {About.VersionInformation()}.zip";
				var filePath = $"{packageStorage}\\{fileName}";

				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}

				using (var writer = new PackageWriter(filePath))
		        {
			        writer.Initialize(Installer.CreateInstallationContext());
			        PackageGenerator.GeneratePackage(packageProject, writer);
		        }
			}
	        finally
	        {
		        Context.SetActiveSite(prevSite);
			}
		}
    }
}