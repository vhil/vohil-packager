namespace Pintle.Packager
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Pipelines;
	using Sitecore;

	[Serializable]
	public class BuildPackageResult
	{
		public BuildPackageResult()
		{
			this.Success = true;
			this.Errors = new Dictionary<string, string>();
			this.PackageUrl = string.Empty;
			this.PackageFileName = string.Empty;
			this.PackageFilePath = string.Empty;
		}

		public BuildPackageResult(Exception ex)
			: this()
		{
			this.Success = false;
			this.Errors.Add(ex.GetType().Name, ex.Message);
		}

		public BuildPackageResult(BuildPackageArgs args)
			:this()
		{
			if (args.Errors.Any())
			{
				this.Success = false;
				this.Errors = args.Errors;
			}
			else
			{
				this.PackageFilePath = args.PackageFilePath;
				this.PackageUrl = MainUtil.UnmapPath(args.PackageFilePath).Replace("\\", "/");
				var lastSlashIndex = args.PackageFilePath.LastIndexOf('\\');
				this.PackageFileName = args.PackageFilePath.Substring(lastSlashIndex + 1, args.PackageFilePath.Length - lastSlashIndex - 1);
				this.PackageUrl = this.PackageUrl.Replace(this.PackageFileName.ToLower(), this.PackageFileName);
			} 
		}

		public bool Success { get; set; }
		public IDictionary<string, string> Errors { get; set; }
		public string PackageUrl { get; set; }
		public string PackageFileName { get; set; }
		internal string PackageFilePath { get; set; }
	}
}