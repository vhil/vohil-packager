namespace Pintle.Packager.Pipelines
{
	using System.Linq;

	public abstract class BuildPackageProcessor
	{
		public abstract void Process(BuildPackageArgs args);

		protected virtual bool AbortIfErrorsDetected(BuildPackageArgs args)
		{
			if (args.Errors.Any())
			{
				args.AbortPipeline();
				return true;
			}

			return false;
		}
	}
}