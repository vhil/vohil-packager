namespace Pintle.Packager.sitecore.admin
{
	using System;
	using Sitecore.sitecore.admin;
	using System.Linq;
	using Configuration;
	using System.Collections.Generic;
	using Sitecore;

	public partial class Packager : AdminPage
	{
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.CheckSecurity();
		}

		protected void Run(object sender, EventArgs e)
		{
			var firstPackage = PackageConfiguration.GetConfiguredPackages().FirstOrDefault();
			var parameters = new Dictionary<string, string>();
			var res = new PackagerService().BuildPackage(firstPackage, parameters);
			var url = MainUtil.UnmapPath(res.PackageFilePath);
			var filePath = res.PackageFilePath;
		}
	}
}