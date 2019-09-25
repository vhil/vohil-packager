using Newtonsoft.Json.Serialization;

namespace Pintle.Packager.sitecore.admin
{
	using System;
	using Sitecore.sitecore.admin;
	using System.Linq;
	using Configuration;
	using System.Collections.Generic;
	using System.Threading;
	using Newtonsoft.Json;

	public partial class Packager : AdminPage
	{
		protected ICollection<PackageConfiguration> ConfiguredPackages;
		protected PackagerService PackagerService;

		public Packager()
		{
			this.PackagerService = new PackagerService();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.CheckSecurity();

			this.ConfiguredPackages = PackageConfiguration.GetConfiguredPackages().ToList();

			var action = this.Request.QueryString["action"];
			var packageName = this.Request.QueryString["packageName"];

			if (!string.IsNullOrWhiteSpace(action))
			{
				this.Response.Clear();

				if (action == "generate-package")
				{
					var result = this.PackagerService.BuildPackage(packageName, this.Request.QueryString);

					var json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
					{
						ContractResolver = new CamelCasePropertyNamesContractResolver(),
						Formatting = Formatting.Indented
					});

					this.Response.ContentType = "application/json";
					this.Response.Write(json);
				}

				this.Response.End();

				return;
			}
		}

		//protected void Run(object sender, EventArgs e)
		//{
		//	var firstPackage = PackageConfiguration.GetConfiguredPackages().FirstOrDefault();
		//	var parameters = new Dictionary<string, string>();
		//	var res = new PackagerService().BuildPackage(firstPackage, parameters);
		//	var url = MainUtil.UnmapPath(res.PackageFilePath);
		//	var filePath = res.PackageFilePath;
		//}
	}
}