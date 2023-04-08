using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace Example.Models;

[Function("setApprovalForAll")]
public class SetApproval : FunctionMessage
{
    [Parameter("address", "operator", 1)]
    public string Operator { get; set; } = null!;

    [Parameter("bool", "approved", 2)]
    public bool Approved { get; set; }
}