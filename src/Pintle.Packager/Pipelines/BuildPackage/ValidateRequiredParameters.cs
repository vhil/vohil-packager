namespace Pintle.Packager.Pipelines.BuildPackage
{
	public class ValidateRequiredParameters : BuildPackageProcessor
	{
		public override void Process(BuildPackageArgs args)
		{
			foreach (var param in args.PackageConfiguration.Parameters)
			{
				if (!args.Parameters.ContainsKey(param.Key) && param.Value.Required)
				{
					args.Errors.Add(param.Key, $"Parameter '{param.Key}' is required but was not passed to the package builder.");
				}
				else if (args.Parameters.ContainsKey(param.Key) && param.Value.Required)
				{
					var parameter = args.Parameters[param.Key];
					if (string.IsNullOrWhiteSpace(parameter))
					{
						args.Errors.Add(param.Key, $"Parameter '{param.Key}' is required but was not passed to the package builder.");
					}
				}
			}

		}
	}
}