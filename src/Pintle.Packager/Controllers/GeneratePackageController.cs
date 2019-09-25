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
			var result = this.PackagerService.BuildPackage(packageName, this.Request.QueryString);
			return this.Json(result, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
		}

		public ActionResult GeneratePackageFile(string packageName)
		{
			var result = this.PackagerService.BuildPackage(packageName, this.Request.QueryString);

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