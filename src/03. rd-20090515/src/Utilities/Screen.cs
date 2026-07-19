using Silk.NET.Windowing;

namespace RubyDung.Utilities;

/// <summary>
/// Acesso para exibir informações.
/// </summary>
public class Screen
{
    /// <summary>
    /// Largura atual da janela da tela em pixels (somente leitura).
    /// </summary>
    public static int Width => _window.Size.X;

    /// <summary>
    /// Altura atual da janela da tela em pixels (somente leitura).
    /// </summary>
    public static int Height => _window.Size.Y;

    // --------------------------------------------------

    private static IWindow _window = null!;

    // --------------------------------------------------

    public static void Initialize(IWindow window)
    {
        _window = window;
    }
}
