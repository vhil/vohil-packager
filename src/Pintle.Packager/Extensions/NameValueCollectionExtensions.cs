namespace Pintle.Packager.Extensions
{
	using System.Collections.Generic;
	using System.Collections.Specialized;

	public static class NameValueCollectionExtensions
	{
		public static IDictionary<string, string> ToDictionary(this NameValueCollection nameValueCollection)
		{
			var dic = new Dictionary<string, string>();

			foreach (string param in nameValueCollection.Keys)
			{
				dic.Add(param, nameValueCollection[param]);
			}

			return dic;
		}
	}
}