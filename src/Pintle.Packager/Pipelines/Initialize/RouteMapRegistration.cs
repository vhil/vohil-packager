namespace Pintle.Packager.Pipelines.Initialize
{
	using System.Web.Mvc;
	using System.Web.Routing;
	using Sitecore.Pipelines;

	public class RouteMapRegistration
	{
		public void Process(PipelineArgs args)
		{
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