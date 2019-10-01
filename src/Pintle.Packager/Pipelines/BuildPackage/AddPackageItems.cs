using Sitecore.Diagnostics;

namespace Pintle.Packager.Pipelines.BuildPackage
{
	using Sitecore.Configuration;
	using Sitecore.Install.Items;

	public class AddPackageItems : BuildPackageProcessor
	{
		public override void Process(BuildPackageArgs args)
		{
			if (this.AbortIfParametersErrors(args)) return;

			foreach (var itemConfig in args.PackageConfiguration.Items)
			{
				var item = Factory.GetDatabase(itemConfig.Database).Items.GetItem(itemConfig.Path);

				if (item != null)
				{
					if (itemConfig.IncludeChildren)
					{
						args.PackageSources.Add(new ItemSource
						{
							SkipVersions = true,
							Database = item.Uri.DatabaseName,
							Root = item.Uri.ItemID.ToString(),
						});
					}
					else
					{
						args.PackageItems.Entries.Add(new ItemReference(item.Uri, false).ToString());
					}
				}
				else
				{
					Log.Warn("[Pintle.Packager]: Unable to add item '" + itemConfig.Path + "' from '" + itemConfig.Database + "' database to the package. Item does not exist", null, this);
				}
			}
		}
	}
}