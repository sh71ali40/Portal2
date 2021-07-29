using Microsoft.Extensions.DependencyInjection;

namespace Portal.Infrustructure.Interface
{
    public interface IStartUp
    {
        void ConfigureServices(IServiceCollection services);
    }
}
