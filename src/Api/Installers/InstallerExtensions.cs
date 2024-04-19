namespace Messages.Api.Installers;
using Context;

public static class InstallerExtensions
{
    private const LogLevel LoggerLevel = LogLevel.Information;

    public static void InstallServicesInAssembly(this IServiceCollection services)
    {
        var installers = typeof(Program).Assembly.ExportedTypes
            .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

        installers.ForEach(installer => installer.InstallServices(services));
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<MessageContext>(options =>
            options.UseNpgsql(connectionString ?? throw new ArgumentException($"{nameof(connectionString)} is not configured"), o => SetupNpgSqlDbOpts(o))
                            .EnableSensitiveDataLogging()
                            .UseLoggerFactory(MyLoggerFactory));
        return services;
    }

    public static NpgsqlDbContextOptionsBuilder SetupNpgSqlDbOpts(NpgsqlDbContextOptionsBuilder opts)
    {
        opts.SetPostgresVersion(new Version(14, 1));
        opts.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
        return opts;
    }

    public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
    {
        builder
            .AddConsole()
            .AddFilter((category, level)
                => category == DbLoggerCategory.Database.Command.Name
                   && level == LoggerLevel);
    });
}
