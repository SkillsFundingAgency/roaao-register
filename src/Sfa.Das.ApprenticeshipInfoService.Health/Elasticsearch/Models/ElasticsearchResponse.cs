namespace Sfa.Das.ApprenticeshipInfoService.Health.Elasticsearch.Models
{
    using System;
    using System.Collections.Generic;

    using Health.Models;

    public class ElasticsearchResponse
    {
        public bool RequiredAliasesExist { get; set; }

        public List<ElasticsearchAlias> ElasticsearchAliases { get; set; }

        public Exception Exception { get; set; }
    }
}