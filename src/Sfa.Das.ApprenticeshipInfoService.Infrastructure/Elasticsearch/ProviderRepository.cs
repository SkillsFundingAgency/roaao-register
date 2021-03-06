﻿using System.Collections.Generic;

using Sfa.Das.ApprenticeshipInfoService.Core.Models.Responses;
using Sfa.Das.ApprenticeshipInfoService.Infrastructure.FeatureToggles;
using SFA.DAS.Apprenticeships.Api.Types.Providers;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.ApprenticeshipInfoService.Infrastructure.Elasticsearch
{
    using System;
    using System.Linq;
    using Nest;
    using FeatureToggle.Core.Fluent;
    using Sfa.Das.ApprenticeshipInfoService.Core.Configuration;
    using Sfa.Das.ApprenticeshipInfoService.Core.Models;
    using Sfa.Das.ApprenticeshipInfoService.Core.Services;
    using Sfa.Das.ApprenticeshipInfoService.Infrastructure.Mapping;

    public sealed class ProviderRepository : IGetProviders
    {
        private readonly IElasticsearchCustomClient _elasticsearchCustomClient;
        private readonly ILog _applicationLogger;
        private readonly IConfigurationSettings _applicationSettings;
        private readonly IProviderLocationSearchProvider _providerLocationSearchProvider;
        private readonly IProviderMapping _providerMapping;
        private readonly string _providerDocumentType;

        public ProviderRepository(
            IElasticsearchCustomClient elasticsearchCustomClient,
            ILog applicationLogger,
            IConfigurationSettings applicationSettings,
            IProviderLocationSearchProvider providerLocationSearchProvider,
            IProviderMapping providerMapping)
        {
            _elasticsearchCustomClient = elasticsearchCustomClient;
            _applicationLogger = applicationLogger;
            _applicationSettings = applicationSettings;
            _providerLocationSearchProvider = providerLocationSearchProvider;
            _providerMapping = providerMapping;

            _providerDocumentType = Is<RoatpProvidersFeature>.Enabled ? "providerapidocument" : "providerdocument";
        }

        public IEnumerable<ProviderSummary> GetAllProviders()
        {
            var take = GetProvidersTotalAmount();
            var results =
                _elasticsearchCustomClient.Search<Provider>(
                    s =>
                    s.Index(_applicationSettings.ProviderIndexAlias)
                        .Type(Types.Parse(_providerDocumentType))
                        .From(0)
                        .Sort(sort => sort.Ascending(f => f.Ukprn))
                        .Take(take)
                        .MatchAll());

            if (results.ApiCall.HttpStatusCode != 200)
            {
                throw new ApplicationException("Failed query all providers");
            }

            return results.Documents.Select(provider => _providerMapping.MapToProviderDto(provider)).ToList();
        }

        public Provider GetProviderByUkprn(long ukprn)
        {
            var results =
                _elasticsearchCustomClient.Search<Provider>(
                    s =>
                    s.Index(_applicationSettings.ProviderIndexAlias)
                        .Type(Types.Parse(_providerDocumentType))
                        .From(0)
                        .Sort(sort => sort.Ascending(f => f.Ukprn))
                        .Take(100)
                        .Query(q => q
                            .Terms(t => t
                                .Field(f => f.Ukprn)
                                .Terms(ukprn))));

            if (results.ApiCall.HttpStatusCode != 200)
            {
                throw new ApplicationException("Failed query provider by ukprn");
            }
            if (results.Documents.Count() > 1)
            {
                _applicationLogger.Warn($"found {results.Documents.Count()} providers for the ukprn {ukprn}");
            }
            return results.Documents.FirstOrDefault();
        }

        public List<StandardProviderSearchResultsItemResponse> GetByStandardIdAndLocation(int id, double lat, double lon, int page)
        {
            var coordinates = new Coordinate
            {
                Lat = lat,
                Lon = lon
            };

            var providers = _providerLocationSearchProvider.SearchStandardProviders(id, coordinates, page);

            return providers.Select(provider => _providerMapping.MapToStandardProviderResponse(provider)).ToList();
        }

        public List<FrameworkProviderSearchResultsItemResponse> GetByFrameworkIdAndLocation(int id, double lat, double lon, int page)
        {
            var coordinates = new Coordinate
            {
                Lat = lat,
                Lon = lon
            };

            var providers = _providerLocationSearchProvider.SearchFrameworkProviders(id, coordinates, page);

            return providers.Select(provider => _providerMapping.MapToFrameworkProviderResponse(provider)).ToList();
        }

        private int GetProvidersTotalAmount()
        {
            var results =
                _elasticsearchCustomClient.Search<Provider>(
                    s =>
                    s.Index(_applicationSettings.ProviderIndexAlias)
                        .Type(Types.Parse(_providerDocumentType))
                        .From(0)
                        .MatchAll());
            return (int)results.HitsMetaData.Total;
        }
    }
}
