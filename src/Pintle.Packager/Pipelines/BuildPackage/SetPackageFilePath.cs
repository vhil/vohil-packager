namespace Pintle.Packager.Pipelines.BuildPackage
{
	using System.IO;
	using Sitecore;
	using Sitecore.Configuration;
	using Logging;

	public class SetPackageFilePath : BuildPackageProcessor
	{
		protected Logger Log = Logger.ConfiguredInstance;

		public override void Process(BuildPackageArgs args)
		{
			if (this.AbortIfErrorsDetected(args)) return;

			var fileName = this.GetFileName(args);
			this.Log.Debug("File name will be '" + fileName + "'", this);

			var directoryPath = this.PackageStoragePath(args);
			this.Log.Debug("File directory path will be '" + directoryPath + "'", this);

			if (!Directory.Exists(directoryPath))
			{
				this.Log.Debug("Creating directory '" + directoryPath + "'...", this);
				Directory.CreateDirectory(directoryPath);
			}

			var filePath = Path.Combine(directoryPath, fileName);

			if (File.Exists(filePath))
			{
				this.Log.Debug("File exists, deleting file '" + filePath + "'...", this);
				File.Delete(filePath);
			}

			args.PackageFilePath = filePath;
		}

		protected virtual string PackageStoragePath(BuildPackageArgs args)
		{
			var storageSetting = Settings.GetSetting("Pintle.PackageStoragePath", "/packager");
			if (storageSetting.StartsWith("/"))
			{
				return MainUtil.MapPath(storageSetting);
			}

			return storageSetting;
		}

		protected virtual string GetFileName(BuildPackageArgs args)
		{
			return $"{args.PackageConfiguration.Metadata.PackageName} v.{args.PackageConfiguration.Metadata.Version} {About.VersionInformation()}.zip";
		}
	}
}