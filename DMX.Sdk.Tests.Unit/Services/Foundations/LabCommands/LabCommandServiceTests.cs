// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Sdk.Brokers.DmxApis;
using DMX.Sdk.Brokers.Loggings;
using DMX.Sdk.Models.LabCommands;
using DMX.Sdk.Services.Foundations.LabCommands;
using Moq;
using RESTFulSense.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace DMX.Sdk.Tests.Unit.Services.Foundations.LabCommands
{
    public partial class LabCommandServiceTests
    {
        private readonly Mock<IDmxApiBroker> dmxApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILabCommandService labCommandService;

        public LabCommandServiceTests()
        {
            this.dmxApiBrokerMock = new Mock<IDmxApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.labCommandService = new LabCommandService(
                dmxApiBroker: this.dmxApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData CriticalDependencyException()
        {
            string someMessage = GetRandomString();
            var someResponseMessage = new HttpResponseMessage();

            return new TheoryData<Xeption>()
            {
                new HttpResponseUrlNotFoundException(someResponseMessage, someMessage),
                new HttpResponseUnauthorizedException(someResponseMessage, someMessage),
                new HttpResponseForbiddenException(someResponseMessage, someMessage),
            };
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static LabCommand CreateRandomLabCommand() =>
            CreateLabCommandFiller().Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<LabCommand> CreateLabCommandFiller()
        {
            var filler = new Filler<LabCommand>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset);

            return filler;
        }

        private static Filler<Dictionary<string, List<string>>> CreateDictionaryFiller() =>
            new Filler<Dictionary<string, List<string>>>();

        private static Dictionary<string, List<string>> CreateRandomDictionary() =>
            CreateDictionaryFiller().Create();
    }
}
