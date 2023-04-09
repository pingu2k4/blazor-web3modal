using System.ComponentModel;

namespace Blazor_Web3Modal;

/// <summary>
/// Select light or dark theme for the modal
/// </summary>
public enum ThemeMode
{
    [Description("light")]
    Light,
    [Description("dark")]
    Dark
}