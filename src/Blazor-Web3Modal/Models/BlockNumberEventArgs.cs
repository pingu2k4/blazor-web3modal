namespace Blazor_Web3Modal;
public class BlockNumberEventArgs : EventArgs
{
    /// <summary>
    /// The new block number
    /// </summary>
    public required int BlockNumber { get; set; }
}