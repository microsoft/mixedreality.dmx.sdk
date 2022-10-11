// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Sdk.Brokers.DmxApis;
using DMX.Sdk.Brokers.Loggings;
using DMX.Sdk.Models.LabWorkflows;
using DMX.Sdk.Services.Foundations.LabWorkflows;
using Moq;
using RESTFulSense.Exceptions;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;
using Xunit.Sdk;

namespace DMX.Sdk.Tests.Unit.Services.Foundations.LabWorkflows
{
    public partial class LabWorkflowServiceTests
    {
        private readonly Mock<IDmxApiBroker> dmxApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly LabWorkflowService labWorkflowService;

        public LabWorkflowServiceTests()
        {
            this.dmxApiBrokerMock = new Mock<IDmxApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.labWorkflowService = new LabWorkflowService(
                dmxApiBroker: this.dmxApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData CriticalDependencyException()
        {
            return new TheoryData<Exception>
            {
                new HttpResponseUrlNotFoundException(),
                new HttpResponseUnauthorizedException(),
                new HttpResponseForbiddenException(),
            };
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<LabWorkflow> CreateRandomLabWorkflowFiller()
        {
            var filler = new Filler<LabWorkflow>();
            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset);

            return filler;
        }

        private static LabWorkflow CreateRandomLabWorkflow() =>
            CreateRandomLabWorkflowFiller().Create();

        private static Filler<Dictionary<string, List<string>>> CreateDictionaryFiller() =>
            new Filler<Dictionary<string, List<string>>>();

        private static Dictionary<string, List<string>> CreateRandomDictionary() =>
            CreateDictionaryFiller().Create();
    }
}
