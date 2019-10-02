namespace Pintle.Packager.Pipelines.BuildPackage
{
	using System.IO;
	using Sitecore;
	using Logging;

	public class AddPackageFiles : BuildPackageProcessor
	{
		protected Logger Log = Logger.ConfiguredInstance;

		public override void Process(BuildPackageArgs args)
		{
			if (this.AbortIfErrorsDetected(args)) return;

			foreach (var fileConfig in args.PackageConfiguration.Files)
			{
				var pathMapped = MainUtil.MapPath(fileConfig.Path);

				if (!string.IsNullOrWhiteSpace(pathMapped) && File.Exists(pathMapped))
				{
					args.PackageFiles.Entries.Add(pathMapped);
					this.Log.Debug("Added file '" + pathMapped + "' to the package", this);
				}
				else
				{
					this.Log.Warn("Unable to add file '" + pathMapped + "' to the package. File does not exist", null, this);
				}
			}
		}
	}
}