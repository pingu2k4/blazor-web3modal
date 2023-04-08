using Example;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazorWeb3ModalComponents(options =>
{
    options.ProjectId = "1bb3663fdc2a0c496edb4535a776e7aa";
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
