namespace Pintle.Packager.Configuration
{
	using System;
	using System.Linq;
	using Sitecore.Configuration;
	using System.Collections.Generic;
	using Sitecore.Xml;
	using System.Xml;

	public class PackageConfiguration
    {
	    public PackageConfiguration()
	    {
		    this.Name = string.Empty;
		    this.Metadata = new MetadataConfiguration();
			this.Items = new List<ItemConfiguration>();
			this.Files = new List<FileConfiguration>();
	    }

		public string Name { get; set; }
		public MetadataConfiguration Metadata { get; set; }
		public ICollection<ItemConfiguration> Items { get; }
		public ICollection<FileConfiguration> Files { get; }

		public void AddItem(string key, System.Xml.XmlNode node)
		{
			this.AddItem(node);
		}

		public void AddItem(System.Xml.XmlNode node)
		{
			var database = XmlUtil.GetAttribute("database", node);
			var path = XmlUtil.GetAttribute("path", node);
			var children = XmlUtil.GetAttribute("children", node);

			if (!string.IsNullOrWhiteSpace(path) && Factory.GetDatabaseNames().Any(x => x == database))
			{
				this.Items.Add(children == null
					? new ItemConfiguration(database, path)
					: new ItemConfiguration(database, path, children.Equals("true", StringComparison.OrdinalIgnoreCase)));
			}
		}

		public void AddFile(string key, System.Xml.XmlNode node)
		{
			this.AddFile(node);
		}

		public void AddFile(System.Xml.XmlNode node)
		{
			var path = XmlUtil.GetAttribute("path", node);
			if (!string.IsNullOrWhiteSpace(path))
			{
				this.Files.Add(new FileConfiguration(path));
			}
		}

		public static IEnumerable<PackageConfiguration> GetConfiguredPackages()
		{
			var packageNodes = Factory.GetConfigNodes("pintle.packager/packages//package");
			foreach (XmlNode packageNode in packageNodes)
			{
				var name = XmlUtil.GetAttribute("name", packageNode);
				var packageConfiguration = Factory.CreateObject<PackageConfiguration>(packageNode);
				packageConfiguration.Name = name;
				yield return packageConfiguration;
			}
		}

		public static PackageConfiguration GetConfiguredPackage(string packageName)
		{
			return GetConfiguredPackages().FirstOrDefault(x => x.Name.Equals(packageName, StringComparison.CurrentCultureIgnoreCase));
		}
	}
}