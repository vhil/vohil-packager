namespace Pintle.Packager.Pipelines.BuildPackage
{
	using Sitecore.Install;
	using Sitecore.Install.Zip;
	using Logging;
	using System;
	using Sitecore.Data;
	using Sitecore.Sites;

	public class GeneratePackage : BuildPackageProcessor
	{
		protected Logger Log = Logger.ConfiguredInstance;

		public override void Process(BuildPackageArgs args)
		{
			if (this.AbortIfErrorsDetected(args)) return;

			if (args.PackageFiles.Entries.Count > 0)
			{
				args.Package.Sources.Add(args.PackageFiles);
			}

			if (args.PackageItems.Entries.Count > 0 || args.PackageSources.Sources.Count > 0)
			{
				args.Package.Sources.Add(args.PackageSources);
			}

			args.Package.SaveProject = true;

			try
			{
				this.Log.Debug("Generating package '" + args.PackageFilePath + "'...", this);

				using (new SiteContextSwitcher(SiteContext.GetSite("shell")))
				using (new DatabaseSwitcher(Database.GetDatabase("core")))
				using (var writer = new PackageWriter(args.PackageFilePath))
				{
					var context = Sitecore.Install.Serialization.IOUtils.SerializationContext;
					writer.Initialize(Installer.CreateInstallationContext());
					PackageGenerator.GeneratePackage(args.Package, writer);
				}

			}
			catch (Exception ex)
			{
				args.Errors.Add(ex.GetType().Name, ex.Message);
				this.Log.Error("Error generating package '" + args.PackageFilePath + "'", ex, this);
			}
		}
	}
}