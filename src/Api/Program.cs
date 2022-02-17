using Messages.Api.Apis.Interfaces;
using Messages.Api.Context;
using Messages.Api.Installers;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddPersistence();
services.InstallServicesInAssembly();

var app = builder.Build();

Configure(app);

var apis = app.Services.GetServices<IApi>();
foreach (var api in apis)
{
    if (api is null) throw new InvalidProgramException("Api not found");
    api.Register(app);
}

app.Run();


void Configure(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<MessageContext>();

    if (app.Environment.IsDevelopment())
    {
        db.Database.EnsureCreated();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    if (db.Database.GetPendingMigrations().Any())
    {
        try
        {
            db.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogError(ex, "An error occurred while migrating or seeding the database.");

            throw;
        }
    }

    app.UseHttpsRedirection();
}