namespace CloudComputingUTN.Service.UnitTests
{
    [TestFixture]
    public class ArtistsControllerTests
    {
        Mock<IMuseumDbRepository> _mockRepository;
        IMapper _mapper;
        Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        Mock<ILinkService> _mockLinkService;
        Mock<LinkGenerator> _mockLinkGenerator;
        ArtistsController? _controller;
        const string API_ARTISTS_CONTROLLER = "/api/v1/Artists/";

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
            _mockLinkService.Setup(mls => mls.Generate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>())).Returns(LinkMocking.CreateLink(API_ARTISTS_CONTROLLER));
        }

        [Test]
        public async Task GetArtists_WhenCalled_ReturnsOk() 
        {
            _mockRepository.Setup(m => m.GetArtists()).ReturnsAsync(DatabaseMocking.ArtistsCollection());
            _controller = new ArtistsController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Get(_mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task GetArtists_ServerError_ReturnsServerError()
        {
            _mockRepository.Setup(m => m.GetArtists()).Throws<Exception>();
            _controller = new ArtistsController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Get(_mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            actionResult.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Test]
        public async Task GetArtistById_RequestIsGood_ArtistExists_ReturnsOk()
        {
            _mockRepository.Setup(m => m.GetArtistById(1)).ReturnsAsync(DatabaseMocking.GetArtistById(1));
            _controller = new ArtistsController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Get(1,_mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task GetArtistById_InvalidId_ReturnsBadRequest()
        {
            _controller = new ArtistsController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Get(0, _mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf(typeof(BadRequestResult)));
        }

        [Test]
        public async Task GetArtistById_ValidId_ReturnsNotFound()
        {
            _mockRepository.Setup(m => m.GetArtistById(2)).Throws(new InvalidOperationException("Sequence contains no elements."));
            _controller = new ArtistsController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Get(2, _mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf(typeof(NotFoundResult)));
        }

        [Test]
        public async Task Post_WhenCalled_ReturnsCreted()
        {
            Artist artist = DatabaseMocking.GetNewArtist();
            _controller = new ArtistsController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Post(artist, _mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf(typeof(CreatedResult)));
        }

        [Test]
        public async Task Post_WhenCalled_ExceptionThrown_ReturnsServerError()
        {
            Artist artist = DatabaseMocking.GetNewArtist();
            _mockRepository.Setup(m => m.CreateArtist(It.IsAny<Artist>())).Throws(new Exception());
            _controller = new ArtistsController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Post(artist, _mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            actionResult.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Test]
        public async Task Put_WhenCalled_ReturnsOk()
        {
            Artist artist = DatabaseMocking.GetArtistById(1);
            _controller = new ArtistsController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Put(artist, _mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task Put_WhenCalled_ExceptionThrown_ReturnsServerError()
        {
            Artist artist = DatabaseMocking.GetNewArtist();
            _mockRepository.Setup(m => m.UpdateArtist(It.IsAny<Artist>())).Throws(new Exception());
            _controller = new ArtistsController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Put(artist, _mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            actionResult.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Test]
        public async Task Delete_WhenCalled_ReturnsOk()
        {
            _controller = new ArtistsController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Delete(1);
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task Delete_WhenCalled_ExceptionThrown_ReturnsServerError()
        {
            _mockRepository.Setup(m => m.DeleteArtist(It.IsAny<int>())).Throws(new Exception());
            _controller = new ArtistsController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object, _mockLinkService.Object);
            var actionResult = await _controller.Delete(1);
            Assert.IsNotNull(actionResult);
            actionResult.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}