namespace Messages.Api.Installers
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services);
    }
}
