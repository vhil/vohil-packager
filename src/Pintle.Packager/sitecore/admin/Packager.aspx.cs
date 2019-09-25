namespace Pintle.Packager.sitecore.admin
{
	using System;
	using Sitecore.sitecore.admin;
	using System.Linq;
	using Configuration;
	using System.Collections.Generic;

	public partial class Packager : AdminPage
	{
		protected ICollection<PackageConfiguration> ConfiguredPackages;

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.CheckSecurity();

			this.ConfiguredPackages = PackageConfiguration.GetConfiguredPackages().ToList();
		}
	}
}