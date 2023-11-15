using CloudComputingUTN.Entities;

namespace CloudComputingUTN.Service.UnitTests
{
    [TestFixture]
    public class ArtworksControllerTests
    {
        Mock<IMuseumDbRepository> _mockRepository;
        IMapper _mapper;
        Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        Mock<ILinkService> _mockLinkService;
        Mock<LinkGenerator> _mockLinkGenerator;
        ArtworksController? _controller;
        const string API_ARTWORKS_CONTROLLER = "/api/v1/Artists/";

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

            //Mock LinkGenerator
            _mockLinkGenerator = new Mock<LinkGenerator>();

            //Mock ILinkService
            _mockLinkService = new Mock<ILinkService>();
            _mockLinkService.Setup(mls => mls.Generate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>())).Returns(LinkMocking.CreateLink(API_ARTWORKS_CONTROLLER));
        }

        [Test]
        public async Task Get_Artowrks_WhenCalled_ReturnsOk()
        {
            _mockRepository.Setup(r => r.GetArtworks()).ReturnsAsync(DatabaseMocking.ArtworksCollection());
            _controller = new ArtworksController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Get(_mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task Get_Artowrks_ExceptionThrown_ReturnsServerError()
        {
            _mockRepository.Setup(r => r.GetArtworks()).Throws<Exception>();
            _controller = new ArtworksController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Get(_mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            actionResult.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Test]
        public async Task Get_Artowrk_WhenCalled_ReturnsOk()
        {
            _mockRepository.Setup(r => r.GetArtworkById(1)).ReturnsAsync(DatabaseMocking.GetArtwork(1));
            _controller = new ArtworksController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Get(1,_mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task Get_Artowrk_InvalidId_ReturnsBadRequest()
        {
            _controller = new ArtworksController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Get(0, _mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf(typeof(BadRequestResult)));
        }

        [Test]
        public async Task Get_Artwork_ValidId_ReturnsNotFound()
        {
            _mockRepository.Setup(m => m.GetArtworkById(2)).Throws(new InvalidOperationException("Sequence contains no elements."));
            _controller = new ArtworksController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Get(2, _mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf(typeof(NotFoundResult)));
        }

        [Test]
        public async Task Post_WhenCalled_ReturnsCreated()
        {
            Artwork newArtwork = DatabaseMocking.GetNewArtwork();
            _controller = new ArtworksController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Post(newArtwork, _mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf(typeof(CreatedResult)));
        }

        [Test]
        public async Task Post_WhenCalled_ExceptionThrown_ReturnsServerError()
        {
            Artwork artist = DatabaseMocking.GetNewArtwork();
            _mockRepository.Setup(m => m.CreateArtwork(It.IsAny<Artwork>())).Throws(new Exception());
            _controller = new ArtworksController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Post(artist, _mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            actionResult.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
