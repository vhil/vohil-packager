namespace Pintle.Packager.Pipelines.BuildPackage
{
	using System.ComponentModel.DataAnnotations;

	public class ValidateRequiredParameters : BuildPackageProcessor
	{
		public override void Process(BuildPackageArgs args)
		{
			foreach (var param in args.PackageConfiguration.Parameters)
			{
				if (!args.Parameters.ContainsKey(param.Key) && param.Value.Required)
				{
					throw new ValidationException($"Parameter '{param.Key}' is required but was not passed to the package builder.");
				}
				else if (args.Parameters.ContainsKey(param.Key) && param.Value.Required)
				{
					var parameter = args.Parameters[param.Key];
					if (string.IsNullOrWhiteSpace(parameter))
					{
						throw new ValidationException($"Parameter '{param.Key}' is required but was passed to the package builder as null or empty.");
					}
				}
			}
		}
	}
}