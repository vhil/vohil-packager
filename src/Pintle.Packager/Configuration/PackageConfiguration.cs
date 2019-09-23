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
			this.Parameters = new Dictionary<string, ParameterConfiguration>();
	    }

		public string Name { get; set; }
		public MetadataConfiguration Metadata { get; set; }
		public ICollection<ItemConfiguration> Items { get; }
		public ICollection<FileConfiguration> Files { get; }
		public IDictionary<string, ParameterConfiguration> Parameters { get; }

		public void AddItem(string key, XmlNode node)
		{
			this.AddItem(node);
		}

		public void AddItem(XmlNode node)
		{
			var database = XmlUtil.GetAttribute("database", node);
			var path = XmlUtil.GetAttribute("path", node);
			var children = XmlUtil.GetAttribute("children", node);

			if (!string.IsNullOrWhiteSpace(path) && Factory.GetDatabaseNames().Any(x => x == database))
			{
				this.Items.Add(string.IsNullOrWhiteSpace(children)
					? new ItemConfiguration(database, path)
					: new ItemConfiguration(database, path, children.Equals("true", StringComparison.OrdinalIgnoreCase)));
			}
		}

		public void AddParameter(string key, XmlNode node)
		{
			this.AddParameter(node);
		}

		public void AddParameter(XmlNode node)
		{
			var name = XmlUtil.GetAttribute("name", node);
			var htmlType = XmlUtil.GetAttribute("htmlType", node);
			var displayName = XmlUtil.GetAttribute("displayName", node);
			var required = XmlUtil.GetAttribute("required", node);

			if (!string.IsNullOrWhiteSpace(name) && !this.Parameters.ContainsKey(name))
			{
				this.Parameters.Add(name, string.IsNullOrWhiteSpace(required)
					? new ParameterConfiguration(name, displayName, htmlType)
					: new ParameterConfiguration(name, displayName, htmlType, required.Equals("true", StringComparison.OrdinalIgnoreCase)));
			}
		}

		public void AddFile(string key, XmlNode node)
		{
			this.AddFile(node);
		}

		public void AddFile(XmlNode node)
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