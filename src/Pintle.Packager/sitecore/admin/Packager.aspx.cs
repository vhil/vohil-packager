namespace Pintle.Packager.sitecore.admin
{
	using System;
	using Sitecore.sitecore.admin;
	using System.Linq;
	using Configuration;

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
			var res = new PackagerService().BuildPackage(firstPackage);
		}
	}
}