using System.Linq;

namespace Pintle.Packager.Pipelines
{
    public abstract class BuildPackageProcessor
    {
        public abstract void Process(BuildPackageArgs args);

        protected virtual bool AbortIfParametersErrors(BuildPackageArgs args)
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