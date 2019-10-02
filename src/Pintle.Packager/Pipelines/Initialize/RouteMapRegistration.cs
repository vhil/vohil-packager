namespace Pintle.Packager.Pipelines.Initialize
{
	using System.Web.Mvc;
	using System.Web.Routing;
	using Sitecore.Pipelines;
	using Logging;

	public class RouteMapRegistration
	{
		protected Logger Log = Logger.ConfiguredInstance;

		public void Process(PipelineArgs args)
		{
			this.Log.Debug("Registering Pintle.Packager MVC routes...", this);

			RouteTable.Routes.MapRoute(
				name: $"Pintle.Packager.GeneratePackage",
				url: "packager/generate",
				defaults: new
				{
					controller = "GeneratePackage",
					action = "GeneratePackage"
				},
				namespaces: new string[] { "Pintle.Packager.Controllers" }
			);

			RouteTable.Routes.MapRoute(
				name: $"Pintle.Packager.GeneratePackageFile",
				url: "packager/generate-file",
				defaults: new
				{
					controller = "GeneratePackage",
					action = "GeneratePackageFile"
				},
				namespaces: new string[] { "Pintle.Packager.Controllers" }
			);
		}
	}
}