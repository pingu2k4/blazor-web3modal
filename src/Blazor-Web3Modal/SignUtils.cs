using Nethereum.ABI.EIP712;

namespace Blazor_Web3Modal;
public static class SignUtils
{
    /// <summary>
    /// Creates a <see cref="TypedData{TDomain}"/> for use with <see cref="IWeb3ModalInterop.SignTypedData{T}(TypedData{Domain}, T)"/>
    /// </summary>
    /// <param name="domain">Information on the domain</param>
    /// <param name="types">All types in the typed message you are signing</param>
    /// <returns>A valid <see cref="TypedData{TDomain}"/></returns>
    public static TypedData<Domain> GetTypedData(Domain domain, params Type[] types)
    {
        return new TypedData<Domain>
        {
            Domain = domain,
            Types = MemberDescriptionFactory.GetTypesMemberDescription(types)
        };
    }
}