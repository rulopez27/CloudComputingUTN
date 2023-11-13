using FluentAssertions;

namespace CloudComputingUTN.Service.UnitTests
{
    [TestFixture]
    public class ArtistsControllerTests
    {
        Mock<IMuseumDbRepository> _mockRepository;
        IMapper _mapper;
        Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        Mock<LinkGenerator> _mockLinkGenerator;
        ArtistsController? _controller;

        [SetUp]
        public void Setup()
        {
            //Mock IMuseumDbRepository
            _mockRepository = new Mock<IMuseumDbRepository>();

            //Mock IMapper
            var mappingConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ArtistProfile());
                mc.AddProfile(new ArtworkProfile());
            });

            _mapper = mappingConfiguration.CreateMapper();

            //Mock IHttpContextAccesor
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockHttpContextAccessor.Setup(ca => ca.HttpContext).Returns(new DefaultHttpContext());
            
            _mockLinkGenerator = new Mock<LinkGenerator>();
        }

        [Test]
        public async Task GetArtists_WhenCalled_ReturnsOk() 
        {
            _mockRepository.Setup(m => m.GetArtists()).ReturnsAsync(DatabaseMocking.ArtistsCollection());
            _controller = new ArtistsController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object);
            var actionResult = await _controller.Get(_mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task GetArtists_ServerError_ReturnsServerError()
        {
            _mockRepository.Setup(m => m.GetArtists()).Throws<Exception>();
            _controller = new ArtistsController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object);
            var actionResult = await _controller.Get(_mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            actionResult.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}