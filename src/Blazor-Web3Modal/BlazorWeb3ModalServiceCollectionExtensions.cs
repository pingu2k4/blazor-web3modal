using Blazor_Web3Modal;

namespace Microsoft.Extensions.DependencyInjection;
public static class BlazorWeb3ModalServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorWeb3ModalComponents(this IServiceCollection services, string projectId) => AddBlazorWeb3ModalComponents(services, (options) =>
    {
        options.ProjectId = projectId;
    });

    public static IServiceCollection AddBlazorWeb3ModalComponents(this IServiceCollection services, Action<Web3ModalOptions>? configure)
    {
        services.Configure<Web3ModalOptions>(options =>
        {
            configure?.Invoke(options);
            if(string.IsNullOrEmpty(options.ProjectId))
            {
                throw new Exception("You must provide a project Id to initialise Web3Modal.");
            }
        });

        services.AddScoped<IWeb3ModalInterop, Web3ModalInterop>();

        return services;
    }
}