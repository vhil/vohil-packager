namespace Pintle.Packager.Pipelines.BuildPackage
{
	using System.IO;
	using Sitecore;
	using Sitecore.Diagnostics;

	public class AddPackageFiles : BuildPackageProcessor
	{
		public override void Process(BuildPackageArgs args)
		{
			if (this.AbortIfParametersErrors(args)) return;

			foreach (var fileConfig in args.PackageConfiguration.Files)
			{
				var pathMapped = MainUtil.MapPath(fileConfig.Path);

				if (!string.IsNullOrWhiteSpace(pathMapped) && File.Exists(pathMapped))
				{
					args.PackageFiles.Entries.Add(pathMapped);
				}
				else
				{
					Log.Warn("[Pintle.Packager]: Unable to add file '" + pathMapped + "' to the package. File does not exist", null, this);
				}
			}
		}
	}
}