﻿namespace Sfa.Das.ApprenticeshipInfoService.Api.Controllers
{
    using System.Configuration;
    using System.Diagnostics;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Description;
    using SFA.DAS.Apprenticeships.Api.Types.Stats;

    public class StatsController : ApiController
    {
        [Route("stats/version")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public VersionInformation Version()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly.GetName().Version.ToString();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string assemblyInformationalVersion = fileVersionInfo.ProductVersion;
            return new VersionInformation
            {
                BuildId = ConfigurationManager.AppSettings["BuildId"],
                Version = assemblyInformationalVersion,
                AssemblyVersion = version
            };
        }
    }
}
