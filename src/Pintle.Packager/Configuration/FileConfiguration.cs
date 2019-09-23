namespace Pintle.Packager.Configuration
{
	public class FileConfiguration
	{
		public FileConfiguration(string path)
		{
			this.Path = path;
		}
		public string Path { get; protected set; }
	}
}