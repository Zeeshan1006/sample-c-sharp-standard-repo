using System.Web.Http;
using SampleApp.DependencyResolver;

namespace SampleApp.Api
{
    public class Bootstarpper
    {
        public static void Run() => AutofacDependencyResolver.Initialize(GlobalConfiguration.Configuration);
    }
}