[![Nuget version](https://img.shields.io/nuget/v/Blazor-Web3Modal?logo=nuget&style=for-the-badge)](https://www.nuget.org/packages/Blazor-Web3Modal/) [![Nuget downloads](https://img.shields.io/nuget/dt/Blazor-Web3Modal?logo=nuget&style=for-the-badge)](https://www.nuget.org/packages/Blazor-Web3Modal/) [![Latest Release](https://img.shields.io/github/v/release/pingu2k4/Blazor-Web3Modal?logo=github&style=for-the-badge)](https://github.com/pingu2k4/blazor-web3modal/releases)

# Blazor-Web3Modal
Blazor-Web3Modal is a library which provides extremely simple, yet fully featured usage of the [Web3Modal](https://web3modal.com/) library in your Blazor application. Connect your Blazor app to your users Web3 Wallets.

## 🌐 Example Site
You can see a fully featured [example website here](https://pingu2k4.github.io/blazor-web3modal/).

## 📦 Installing
To install the package you can either install with the following .NET CLI command:
```
dotnet add package Blazor-Web3Modal
```
You can search in Nuget Package Manager for `Blazor-Web3Modal` or you can add the following line to your .csproj file (replacing the placeholder version number with the number shown at the top of this file):
```
<PackageReference Include="Blazor-Web3Modal" Version="x.x.x" />
```

## 🛠️ Setup
In order to use Web3Modal, you will need a Project ID for their [explorer API](https://explorer.walletconnect.com/). It takes only moments - Once you have a project ID, keep note of it for future steps.

Next you should add the following line to your `_Imports.razor` file.
```c#
@using Blazor_Web3Modal;
```

### Blazor Server
You will need to register the services with the DI container in your `Startup.cs` file.
```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddBlazorWeb3ModalComponents("PROJECT_ID");
}
```
**OR**
```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddBlazorWeb3ModalComponents(options =>
    {
        options.ProjectId = "PROJECT_ID";
        // Configure other options on the options object here
    });
}
```


### Blazor WASM
You will need to register the services with the DI container in your `Program.cs` file.
```c#
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorWeb3ModalComponents("PROJECT_ID");

await builder.Build().RunAsync();
```
**OR**
```c#
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorWeb3ModalComponents(options =>
{
    options.ProjectId = "PROJECT_ID";
    // Configure other options on the options object here
});

await builder.Build().RunAsync();
```

## 🚀 Usage
### Components
There are 2 components, and an interface which will allow you to completely control Web3Modal. To use one of the provided components, add the following tags to a `.razor` file.

```html
<CoreButton Label="Custom Label" ShowIcon="true" ShowBalance="true" />
```
```html
<NetworkSwitch />
```

You can find full details on these components on the [Web3Modal docs site](https://docs.walletconnect.com/2.0/web3modal/html-js/components).

### Web3Modal Interop
To use the Web3Modal interop, you first must inject it into any page or component:
```c#
@inject IWeb3ModalInterop _web3ModalInterop;
```

If you already have a [Component](#components) on your page anywhere, then they will have called Configure themselves already and there is nothing more that you need to do. However, if you are not using them then you will first need to call Configure yourself. The JS is lazy loaded, so if you only sometimes need to use Web3Modal, you can delay calling Configure until you know you need it. Otherwise, its best to call it in OnAfterRenderAsync (as it makes a JS call):
```c#
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    await base.OnAfterRenderAsync(firstRender);

    if (firstRender)
    {
        await _web3ModalInterop.Configure();
    }
}
```

From this point, most of the features take a very simple one-liner to invoke. For example to Open the modal:

```c#
_web3ModalInterop.OpenModal();
```

There are however a few methods with a little more complexity, which I will outline next.

#### Signing Typed Data
Signing typed data depends upon Nethereum to help generate the typed data for us. First you will need to define the structure of your message. Take a look at this example:
```c#
using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Text.Json.Serialization;

[Struct("Mail")]
public class Mail
{
    [Parameter("tuple[]", "to", 1, "Person[]")]
    [JsonPropertyName("to")]
    public List<Person> To { get; set; } = new List<Person>();

    [Parameter("string", "contents", 2)]
    [JsonPropertyName("contents")]
    public string Contents { get; set; } = null!;
}

[Struct("Person")]
public class Person
{
    [Parameter("string", "name", 1)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [Parameter("address[]", "wallets", 2)]
    [JsonPropertyName("wallets")]
    public List<string> Wallets { get; set; } = new List<string>();
}
```
This library has a helper method to translate a message using these type(s) into the `TypedData<Domain>` which is expected. You must fill out details for a Domain, and provide a list of types to include. For example:
```c#
var typedData = SignUtils.GetTypedData(new Nethereum.ABI.EIP712.Domain
{
    Name = "Ether Mail",
    Version = "1",
    ChainId = 1,
    VerifyingContract = "0xCcCCccccCCCCcCCCCCCcCcCccCcCCCcCcccccccC"
}, typeof(Mail), typeof(Person));
```
With this done, we can create our message that we want signed, and send it through together with the typedData we just generated:
```c#
var message = new Mail()
{
    To = new List<Person>
{
    new Person
    {
        Name = "Bob",
        Wallets = new List<string>
        {
            "0xbBbBBBBbbBBBbbbBbbBbbbbBBbBbbbbBbBbbBBbB",
            "0xB0BdaBea57B0BDABeA57b0bdABEA57b0BDabEa57",
            "0xB0B0b0b0b0b0B000000000000000000000000000"
        }
    }
},
    Contents = "Hello, Bob!"
};

var result = await _interop.SignTypedData(typedData, message);
```

#### Sending Eth
Transactions (both sending ETH or interacting with a smart contract) also depend on Nethereum. This time we need a TransactionInput. Again, there is a helper method for sending ETH. We provide the from wallet, to wallet and the amount of Eth to send:
```c#
var transactionInput = TransactionUtils.SendEthereumTransactionInput("0x1234...", "0x5678...", 0.01m);
var result = await _interop.SendTransaction(transactionInput);
```

#### Sending Contract Interaction Transaction
Just like with [Signing Typed Data](#signing-typed-data), we need to first create our object. This time its a `FunctionMessage`, representing the method we are calling on the contract. For help creating these function messages, it would be best to take a look through the [Nethereum docs](https://docs.nethereum.com/en/latest/). For example, this is the setApprovalForAll function common to ERC721 contracts:
```c#
[Function("setApprovalForAll")]
public class SetApproval : FunctionMessage
{
    [Parameter("address", "operator", 1)]
    public string Operator { get; set; } = null!;

    [Parameter("bool", "approved", 2)]
    public bool Approved { get; set; }
}
```
Now that we have our FunctionMessage, we can send it through to the utility to get back the TransactionInput we require (along with the address we are sending from, and the address of the contract):
```c#
var setApproval = new SetApproval()
{
    Operator = "0x0000...",
    Approved = false
};
var transactionInput = TransactionUtils.SendContractTransactionInput(setApproval, "0x1234...", "0x5678...");
var result = await _interop.SendTransaction(transactionInput);
```

## ✅ Features
### Components
- Core Button
- Network Switch

### General
- Easily add to DI Container
- Lazy Loading - Only load the JS just before you need it
- Open Modal
- Close Modal
- Set Theme (for changing the theme after initialised)
- Disconnect

### Interactivity
- Sign Message
- Sign Typed Data
- Send Eth
- Send Contact Transactions

### Retrieving Data
- Get Account State
- Get Network State
- Get Balance
- Get ENS Avatar
- Get ENS Name
- Get ENS Address
- Get Block Number
- Get Fee Data
- Get Transaction
- Get Token

### Events
- Listen to connected account changed
- Listen to connected network changed
- Listen to Block number changed (must instruct this to fire seperately, and can stop it when not needed)
- Listen to when a transaction we sent previously has been mined