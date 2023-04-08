using Blazor_Web3Modal;

namespace Microsoft.Extensions.DependencyInjection;
public static class BlazorWeb3ModalServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorWeb3ModalComponents(this IServiceCollection services) => AddBlazorWeb3ModalComponents(services, null);

    public static IServiceCollection AddBlazorWeb3ModalComponents(this IServiceCollection services, Action<Web3ModalOptions>? configure)
    {
        services.Configure<Web3ModalOptions>(options =>
        {
            configure?.Invoke(options);
        });

        services.AddScoped<Web3ModalJsInterop>();

        return services;
    }
}