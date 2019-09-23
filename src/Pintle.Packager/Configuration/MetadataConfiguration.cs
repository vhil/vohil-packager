namespace Pintle.Packager.Configuration
{
	public class MetadataConfiguration
	{
		public MetadataConfiguration()
		{
			this.PackageName = string.Empty;
			this.Author = string.Empty;
			this.Version = string.Empty;
			this.Publisher = string.Empty;
			this.Comment = string.Empty;
			this.License = string.Empty;
			this.PackageId = string.Empty;
			this.Readme = string.Empty;
			this.Revision = string.Empty;
		}

		public string PackageName { get; set; }
		public string Author { get; set; }
		public string Version { get; set; }
		public string Publisher { get; set; }
		public string Comment { get; set; }
		public string License { get; set; }
		public string PackageId { get; set; }
		public string Readme { get; set; }
		public string Revision { get; set; }
	}
}