namespace Pintle.Packager.Pipelines.BuildPackage
{
	using Sitecore.Configuration;
	using Sitecore.Install.Items;
	using Logging;

	public class AddPackageItems : BuildPackageProcessor
	{
		protected Logger Log = Logger.ConfiguredInstance;

		public override void Process(BuildPackageArgs args)
		{
			if (this.AbortIfErrorsDetected(args)) return;

			foreach (var itemConfig in args.PackageConfiguration.Items)
			{
				var item = Factory.GetDatabase(itemConfig.Database).Items.GetItem(itemConfig.Path);

				if (item != null)
				{
					var logMessage = "Added item '" + itemConfig.Path + "' from '" + itemConfig.Database + "' database ";

					if (itemConfig.IncludeChildren)
					{
						logMessage += "with children.";
						args.PackageSources.Add(new ItemSource
						{
							SkipVersions = false,
							Database = item.Uri.DatabaseName,
							Root = item.Uri.ItemID.ToString()
						});
					}
					else
					{
						logMessage += "no children.";
						args.PackageItems.Entries.Add(new ItemReference(item.Uri, false).ToString());
					}

					this.Log.Debug(logMessage, this);
				}
				else
				{
					this.Log.Warn("Unable to add item '" + itemConfig.Path + "' from '" + itemConfig.Database + "' database to the package. Item does not exist", null, this);
				}
			}
		}
	}
}