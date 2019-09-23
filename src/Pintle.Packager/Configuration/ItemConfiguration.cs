namespace Pintle.Packager.Configuration
{
	public class ItemConfiguration
	{
		public ItemConfiguration(string database, string path, bool includeChildren = true)
		{
			this.Database = database;
			this.Path = path;
			this.IncludeChildren = includeChildren;
		}

		public string Database { get; protected set; }
		public bool IncludeChildren { get; protected set; }
		public string Path { get; protected set; }
	}
}