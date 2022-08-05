using DMX.Sdk.Brokers.DmxApis;
using DMX.Sdk.Brokers.Loggings;
using DMX.Sdk.Models.Services.Foundations.Labs;
using DMX.Sdk.Services.Foundations.Labs;
using Moq;
using Tynamix.ObjectFiller;


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

            this.labService = new LabService(this.dmxApiBroker.Object,
                this.loggingBroker.Object);
        }

        private static List<Lab> CreateRandomLabs() =>
            CreateLabsFiller().Create(count: GetRandomNumber()).ToList();

        private static Filler<Lab> CreateLabsFiller() =>
            new Filler<Lab>();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();
    }
}