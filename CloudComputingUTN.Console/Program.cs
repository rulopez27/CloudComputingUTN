using CloudComputingUTN.Middleware.MySQL;
using Newtonsoft.Json;

MuseumDbRepository repository = new MuseumDbRepository();
var artists = repository.GetArtists().ToList();

artists.ForEach(artist =>
{
    Console.WriteLine(JsonConvert.SerializeObject(artist, Formatting.Indented, new JsonSerializerSettings()
    {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    }));
});

Console.ReadLine();