﻿using System.Web;

namespace Sfa.Das.ApprenticeshipInfoService.UnitTests.Controllers
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Moq;
    using NUnit.Framework;
    using Sfa.Das.ApprenticeshipInfoService.Api.Controllers;
    using Sfa.Das.ApprenticeshipInfoService.Api.Helpers;
    using Sfa.Das.ApprenticeshipInfoService.Core.Models;
    using Sfa.Das.ApprenticeshipInfoService.Core.Services;

    [TestFixture]
    public class StandardControllerTest
    {
        [Test]
        public void GetAllShouldReturnValidListOfStandards()
        {
            var mockGetStandards = new Mock<IGetStandards>();

            mockGetStandards.Setup(x => x.GetAllStandards()).Returns(new List<StandardSummary>());

            StandardsController sc = new StandardsController(mockGetStandards.Object);

            var standardListResponse = sc.Get();

            standardListResponse.Should().NotBeNull();
            standardListResponse.Should().BeOfType<List<StandardSummary>>();

        }

        [Test]
        public void GetAllShouldReturnValidStandard()
        {
            var mockGetStandards = new Mock<IGetStandards>();

            mockGetStandards.Setup(x => x.GetStandardById(It.IsAny<int>())).Returns(new Standard());

            StandardsController sc = new StandardsController(mockGetStandards.Object);

            var standardResponse = sc.GetStandard(1);

            standardResponse.Should().NotBeNull();
            standardResponse.Should().BeOfType<Standard>();
        }
    }
}
