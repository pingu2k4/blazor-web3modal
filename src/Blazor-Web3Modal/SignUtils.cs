using Nethereum.ABI.EIP712;

namespace Blazor_Web3Modal;
public static class SignUtils
{
    public static TypedData<Domain> GetTypedData(Domain domain, params Type[] types)
    {
        return new TypedData<Domain>
        {
            Domain = domain,
            Types = MemberDescriptionFactory.GetTypesMemberDescription(types)
        };
    }
}