namespace Pintle.Packager.Configuration
{
	public class ParameterConfiguration
	{
		public string Name { get; }
		public string DisplayName { get; }
		public string HtmlType { get; }
		public string DefaultValue { get; }
		public bool Required { get; }

		public ParameterConfiguration(
			string name, 
			string displayName, 
			string htmlType, 
			string defaultValue = "", 
			bool required = false)
		{
			this.Name = name;
			this.DisplayName = displayName;
			this.HtmlType = htmlType;
			this.DefaultValue = defaultValue;
			this.Required = required;
		}
	}
}