namespace Pintle.Packager.Extensions
{
	public static class StringExtensions
	{
		public static string EmptyIfNull(this string val)
		{
			return string.IsNullOrWhiteSpace(val) ? string.Empty : val;
		}
	}
}