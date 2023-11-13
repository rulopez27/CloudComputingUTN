namespace CloudComputingUTN.Service.UnitTests
{
    [TestFixture]
    public class ArtistsControllerTests
    {
        Mock<IMuseumDbRepository> _mockRepository;
        IMapper _mapper;
        Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        Mock<LinkGenerator> _mockLinkGenerator;

        [SetUp]
        public void Setup()
        {
            //Mock IMuseumDbRepository
            _mockRepository = new Mock<IMuseumDbRepository>();
            _mockRepository.Setup(m => m.GetArtists()).ReturnsAsync(DatabaseMocking.ArtistsCollection());

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
            ArtistsController controller = new ArtistsController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object);
            var actionResult = await controller.Get(_mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf(typeof(OkObjectResult)));
        }
    }
}