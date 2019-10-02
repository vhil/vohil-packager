using System;

namespace Pintle.Packager.Pipelines.BuildPackage
{
	using Sitecore;
	using Sitecore.Install;
	using Sitecore.Install.Zip;
	using Logging;

	public class GeneratePackage : BuildPackageProcessor
    {
	    protected Logger Log = Logger.ConfiguredInstance;

		public override void Process(BuildPackageArgs args)
        {
	        if (this.AbortIfParametersErrors(args)) return;

			if (args.PackageFiles.Entries.Count > 0)
	        {
		        args.Package.Sources.Add(args.PackageFiles);
	        }

			if (args.PackageItems.Entries.Count > 0 || args.PackageSources.Sources.Count > 0)
	        {
		        args.Package.Sources.Add(args.PackageSources);
	        }

	        args.Package.SaveProject = true;

	        var prevSite = Context.Site.Name;

	        try
	        {
		        Context.SetActiveSite("shell");

		        this.Log.Debug("Generating package '" + args.PackageFilePath + "'", this);

		        using (var writer = new PackageWriter(args.PackageFilePath))
		        {
			        writer.Initialize(Installer.CreateInstallationContext());
			        PackageGenerator.GeneratePackage(args.Package, writer);
		        }
	        }
	        catch (Exception ex)
	        {
				this.Log.Error("Error generating package '" + args.PackageFilePath + "'", ex, this);
	        }
	        finally
	        {
		        Context.SetActiveSite(prevSite);
			}
		}
    }
}