namespace Pintle.Packager.Pipelines.BuildPackage
{
	using System.IO;
	using Sitecore;
	using Sitecore.Configuration;

	public class SetPackageFilePath : BuildPackageProcessor
	{
		public override void Process(BuildPackageArgs args)
		{
			var fileName = this.GetFileName(args);
			var directoryPath = this.PackageStoragePath(args);

			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}

			var filePath = Path.Combine(directoryPath, fileName);

			if (File.Exists(filePath))
			{
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