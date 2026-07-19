namespace RubyDung.Utilities;

/// <summary>
/// Classe que contém métodos para facilitar a depuração durante o desenvolvimento de um jogo.
/// </summary>
public static class Debug
{
    /// <summary>
    /// Registra uma mensagem no Console.
    /// </summary>
    /// <param name="menssage">String ou objeto a ser convertido em uma representação de string para exibição.</param>
    public static void Log(object? menssage)
    {
        WriteLine(menssage);
    }

    /// <summary>
    /// Uma variante de Debug.Log que registra uma mensagem de aviso no console.
    /// </summary>
    /// <param name="menssage">String ou objeto a ser convertido em uma representação de string para exibição.</param>
    public static void LogWarning(object? menssage)
    {
        WriteLine(menssage, ConsoleColor.Yellow);
    }

    /// <summary>
    /// Uma variante de Debug.Log que registra uma mensagem de erro no console.
    /// </summary>
    /// <param name="menssage">String ou objeto a ser convertido em uma representação de string para exibição.</param>
    public static void LogError(object? menssage)
    {
        WriteLine(menssage, ConsoleColor.Red);
    }

    private static void WriteLine(object? menssage, ConsoleColor? color = null)
    {
        Console.Write(DateTime.Now.ToString("[HH:mm:ss] "));

        Console.ForegroundColor = color ?? Console.ForegroundColor;
        Console.WriteLine(menssage);
        Console.ResetColor();
    }
}
