using System.Web;

namespace Pintle.Packager.Controllers
{
	using System.Text;
	using System.Web.Mvc;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	public class GeneratePackageController : Controller
	{
		protected PackagerService PackagerService;

		public GeneratePackageController()
		{
			this.PackagerService = new PackagerService();
		}

		public ActionResult GeneratePackage(string packageName)
		{
			if (string.IsNullOrWhiteSpace(packageName))
			{
				this.Response.StatusCode = 404;
				return this.Content($"'{nameof(packageName)}' query string parameter is required.");
			}

			var result = this.PackagerService.BuildPackage(packageName, this.Request.QueryString);
			return this.Json(result, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
		}

		public ActionResult GeneratePackageFile(string packageName)
		{
			if (string.IsNullOrWhiteSpace(packageName))
			{
				this.Response.StatusCode = 404;
				return this.Content($"'{nameof(packageName)}' query string parameter is required.");
			}

			var result = this.PackagerService.BuildPackage(packageName, this.Request.QueryString);

			if (!result.Success)
			{
				var content = new StringBuilder();
				foreach (var error in result.Errors)
				{
					content.AppendLine($"{error.Key}: {error.Value}");
				}

				this.Response.StatusCode = 404;
				return this.Content(content.ToString());
			}

			var filepath = result.PackageFilePath;
			var filedata = System.IO.File.ReadAllBytes(filepath);
			var contentType = MimeMapping.GetMimeMapping(filepath);

			var cd = new System.Net.Mime.ContentDisposition
			{
				FileName = result.PackageFileName,
				Inline = true,
			};

			this.Response.AppendHeader("Content-Disposition", cd.ToString());

			return this.File(filedata, contentType);
		}
	}
}