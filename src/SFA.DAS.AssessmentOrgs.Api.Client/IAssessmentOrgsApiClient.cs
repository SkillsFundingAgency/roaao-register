﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Apprenticeships.Api.Types.AssessmentOrgs;

namespace SFA.DAS.AssessmentOrgs.Api.Client
{
    public interface IAssessmentOrgsApiClient : IDisposable
    {
        /// <summary>
        /// Get a single organisation details
        /// GET /assessmentorgs/{organisationId}
        /// </summary>
        /// <param name="organisationId">an integer for the organisation id</param>
        /// <returns>a organisation details based on id</returns>
        Organisation Get(string organisationId);

        /// <summary>
        /// Get a single organisation details
        /// GET /assessmentorgs/{organisationId}
        /// </summary>
        /// <param name="organisationId">an integer for the organisation id</param>
        /// <returns>a organisation details based on id</returns>
        Task<Organisation> GetAsync(string organisationId);

        /// <summary>
        /// Get a collection of organisations
        /// GET /assessment-organisations/standards/{standardId}
        /// </summary>
        /// <returns>a collection of organisation</returns>
        IEnumerable<Organisation> ByStandard(int standardId);

        /// <summary>
        /// Get a collection of organisations
        /// GET /assessment-organisations/standards/{standardId}
        /// </summary>
        /// <returns>a collection of organisation</returns>
        Task<IEnumerable<Organisation>> ByStandardAsync(int standardId);

        /// <summary>
        /// Get a collection of organisations
        /// GET /assessment-organisations/standards/{standardId}
        /// </summary>
        /// <returns>a collection of organisation</returns>
        IEnumerable<Organisation> ByStandard(string standardId);

        /// <summary>
        /// Get a collection of organisations
        /// GET /assessment-organisations/standards/{standardId}
        /// </summary>
        /// <returns>a collection of organisation</returns>
        Task<IEnumerable<Organisation>> ByStandardAsync(string standardId);

        /// <summary>
        /// Get a collection of organisations
        /// GET /frameworks
        /// </summary>
        /// <returns>a collection of organisation summaries</returns>
        IEnumerable<OrganisationSummary> FindAll();

        /// <summary>
        /// Get a collection of organisations
        /// GET /frameworks
        /// </summary>
        /// <returns>a collection of organisation summaries</returns>
        Task<IEnumerable<OrganisationSummary>> FindAllAsync();

        /// <summary>
        /// Check if a assessment organisation exists
        /// HEAD /assessmentorgs/{organisationId}
        /// </summary>
        /// <param name="organisationId">an integer for the organisation id</param>
        /// <returns>bool</returns>
        bool Exists(string organisationId);

        /// <summary>
        /// Check if a assessment organisation exists
        /// HEAD /assessmentorgs/{organisationId}
        /// </summary>
        /// <param name="organisationId">an integer for the organisation id</param>
        /// <returns>bool</returns>
        Task<bool> ExistsAsync(string organisationId);
    }
}