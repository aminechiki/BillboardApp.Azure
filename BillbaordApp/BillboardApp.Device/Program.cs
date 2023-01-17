using BillboardApp.Device;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<EdgeDevice>(); 
    })
    .Build();

await host.RunAsync();
