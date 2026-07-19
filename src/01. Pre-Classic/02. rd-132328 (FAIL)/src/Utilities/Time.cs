namespace RubyDung.Utilities;

/// <summary>
/// Classe estática responsável pelo gerenciamento de tempo no motor.
/// Fornece o tempo total decorrido desde o início e o delta de tempo entre frames.
/// </summary>
public static class Time
{
    /// <summary>
    /// Tempo total decorrido em segundos desde que o motor foi iniciado (ou desde a primeira chamada do <see cref="Update"/>).
    /// </summary>
    public static float ElapsedTime { get; private set; }

    /// <summary>
    /// Tempo decorrido entre o frame anterior e o frame atual (delta time) em segundos.
    /// Útil para movimentos suaves e independentes da taxa de quadros.
    /// </summary>
    public static float DeltaTime { get; private set; }

    /// <summary>
    /// Atualiza os valores de tempo. Deve ser chamado uma vez por frame.
    /// </summary>
    /// <param name="deltaTime">Tempo decorrido desde o último quadro (em segundos), geralmente fornecido pelo loop principal.</param>
    /// <remarks>
    /// Esta função acumula o <paramref name="deltaTime"/> em <see cref="ElapsedTime"/> e armazena o valor atual em <see cref="DeltaTime"/>.
    /// </remarks>
    public static void Update(double deltaTime)
    {
        ElapsedTime += (float)deltaTime;
        DeltaTime = (float)deltaTime;
    }
}
