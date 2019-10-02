namespace Pintle.Packager.Configuration
{
	using System;
	using System.Linq;
	using Sitecore.Configuration;
	using System.Collections.Generic;
	using Sitecore.Xml;
	using System.Xml;
	using Extensions;

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
			var database = XmlUtil.GetAttribute("database", node).EmptyIfNull();
			var path = XmlUtil.GetAttribute("path", node).EmptyIfNull();
			var children = XmlUtil.GetAttribute("children", node).EmptyIfNull();

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
			var name = XmlUtil.GetAttribute("name", node).EmptyIfNull();
			var htmlType = XmlUtil.GetAttribute("htmlType", node).EmptyIfNull();
			var displayName = XmlUtil.GetAttribute("displayName", node).EmptyIfNull();
			var defaultValue = XmlUtil.GetAttribute("defaultValue", node).EmptyIfNull();
			var required = XmlUtil.GetAttribute("required", node).EmptyIfNull();

			if (!string.IsNullOrWhiteSpace(name) && !this.Parameters.ContainsKey(name))
			{
				this.Parameters.Add(name, string.IsNullOrWhiteSpace(required)
					? new ParameterConfiguration(name, displayName, htmlType, defaultValue)
					: new ParameterConfiguration(name, displayName, htmlType, defaultValue, required.Equals("true", StringComparison.OrdinalIgnoreCase)));
			}
		}

		public void AddFile(string key, XmlNode node)
		{
			this.AddFile(node);
		}

		public void AddFile(XmlNode node)
		{
			var path = XmlUtil.GetAttribute("path", node).EmptyIfNull();
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
				var name = XmlUtil.GetAttribute("name", packageNode).EmptyIfNull();
				var packageConfiguration = Factory.CreateObject<PackageConfiguration>(packageNode);
				packageConfiguration.Name = name;
				yield return packageConfiguration;
			}
		}

		public static PackageConfiguration GetConfiguredPackage(string packageName)
		{
			var package = GetConfiguredPackages().FirstOrDefault(x => x.Name.Equals(packageName, StringComparison.CurrentCultureIgnoreCase));
			if (package == null)
			{
				throw new ArgumentException($"Package with name '{packageName}' was not found in configuration.");
			}

			return package;
		}
	}
}