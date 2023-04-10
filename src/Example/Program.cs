using Blazor_Web3Modal;
using Example;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazorWeb3ModalComponents(options =>
{
    options.ProjectId = "40bbc71ff54bc794c55a448a49996fc7";
    options.Chains = new List<Chain>
    {
        Chain.Mainnet,
        Chain.Goerli
    };
    options.AutoConnect = true;
    options.ThemeOptions.ThemeVariables = new Dictionary<string, string>
    {
        { "--w3m-accent-color", "#FF0000" },
        { "--w3m-background-color", "#00FF00" }
    };
    options.TermsOfServiceUrl = "/terms";
    options.PrivacyPolicyUrl = "/privacy";
});

await builder.Build().RunAsync();
