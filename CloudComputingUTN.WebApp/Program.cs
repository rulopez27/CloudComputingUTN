namespace CloudComputingUTN.WebApp
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args)
            .ConfigureAppConfiguration((builderContext, config) => 
            {
                var env = builderContext.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            })
            .Build()
            .Run();
        

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

    }
}
