// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Linq.Expressions;
using DMX.Sdk.Brokers.DmxApis;
using DMX.Sdk.Brokers.Loggings;
using DMX.Sdk.Models.Services.Foundations.Labs;
using DMX.Sdk.Services.Foundations.Labs;
using Moq;
using RESTFulSense.Exceptions;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace DMX.Sdk.Tests.Unit.Services.Foundations.Labs
{
    public partial class LabServiceTests
    {
        private readonly Mock<IDmxApiBroker> dmxApiBroker;
        private readonly Mock<ILoggingBroker> loggingBroker;
        private readonly ILabService labService;

        public LabServiceTests()
        {
            this.dmxApiBroker = new Mock<IDmxApiBroker>();
            this.loggingBroker = new Mock<ILoggingBroker>();

            this.labService = new LabService(
                dmxApiBroker: this.dmxApiBroker.Object,
                loggingBroker: this.loggingBroker.Object);
        }

        public static TheoryData CriticalDependencyException()
        {
            return new TheoryData<Xeption>()
            {
                new HttpResponseUrlNotFoundException(),
                new HttpResponseUnauthorizedException(),
                new HttpResponseForbiddenException()
            };
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static List<Lab> CreateRandomLabs() =>
            CreateLabsFiller().Create(count: GetRandomNumber()).ToList();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualExpectedAssertException => actualExpectedAssertException.SameExceptionAs(expectedException);

        private static Filler<Lab> CreateLabsFiller() =>
            new Filler<Lab>();
    }
}