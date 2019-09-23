namespace Pintle.Packager.Configuration
{
	public class ParameterConfiguration
	{
		public string Name { get; }
		public string DisplayName { get; }
		public string HtmlType { get; }
		public bool Required { get; }

		public ParameterConfiguration(string name, string displayName, string htmlType, bool required = false)
		{
			this.Name = name;
			this.DisplayName = displayName;
			this.HtmlType = htmlType;
			this.Required = required;
		}
	}
}