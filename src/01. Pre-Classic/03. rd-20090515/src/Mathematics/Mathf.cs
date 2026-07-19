namespace RubyDung.Mathematics;

/// <summary>
/// Uma coleção de funções matemáticas comuns.
/// </summary>
public struct Mathf
{
    // PI
    // --------------------------------------------------

    public static float PI => (float)Math.PI;

    // Radians
    // --------------------------------------------------

    public static float Radians(float degress)
    {
        return PI / 180.0f * degress;
    }

    // Abs
    // --------------------------------------------------

    /// <summary>
    /// Retorna o valor absoluto de value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Abs(float value)
    {
        return Math.Abs(value);
    }

    /// <summary>
    /// Retorna o valor absoluto de value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int Abs(int value)
    {
        return Math.Abs(value);
    }

    // Min
    // --------------------------------------------------

    /// <summary>
    /// Retorna o menor de dois ou mais valores.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static float Min(float a, float b)
    {
        return Math.Min(a, b);
    }

    /// <summary>
    /// Retorna o menor de dois ou mais valores.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int Min(int a, int b)
    {
        return Math.Min(a, b);
    }

    // Max
    // --------------------------------------------------

    /// <summary>
    /// Retorna o maior de dois ou mais valores.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static float Max(float a, float b)
    {
        return Math.Max(a, b);
    }

    /// <summary>
    /// Retorna o maior de dois ou mais valores.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int Max(int a, int b)
    {
        return Math.Max(a, b);
    }

    // Sin
    // --------------------------------------------------

    /// <summary>
    /// Retorna o seno do ângulo value em radianos.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Sin(float value)
    {
        return (float)Math.Sin(value);
    }

    // Cos
    // --------------------------------------------------

    /// <summary>
    /// Retorna o cosseno do ângulo value em radianos.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Cos(float value)
    {
        return (float)Math.Cos(value);
    }

    // Clamp
    // --------------------------------------------------

    /// <summary>
    /// Limita um valor entre um valor mínimo e um valor máximo do tipo ponto flutuante.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float Clamp(float value, float min, float max)
    {
        return Math.Clamp(value, min, max);
    }
    
    /// <summary>
    /// Limita o valor entre o mínimo e o máximo e retorna o valor.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int Clamp(int value, int min, int max)
    {
        return Math.Clamp(value, min, max);
    }

    // Floor
    // --------------------------------------------------

    /// <summary>
    /// Retorna o maior número inteiro menor ou igual a value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Floor(float value)
    {
        return (float)Math.Floor(value);
    }

    /// <summary>
    /// Retorna o maior número inteiro menor ou igual a value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int FloorToInt(float value)
    {
        return (int)Math.Floor(value);
    }

    // Ceil
    // --------------------------------------------------

    /// <summary>
    /// Retorna o menor número inteiro maior ou igual a value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Ceil(float value)
    {
        return (float)Math.Ceiling(value);
    }

    /// <summary>
    /// Retorna o menor número inteiro maior ou igual a value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int CeilToInt(float value)
    {
        return (int)Math.Ceiling(value);
    }

    // Sign
    // --------------------------------------------------

    /// <summary>
    /// Retorna o sinal de value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Sing(float value)
    {
        return Math.Sign(value);
    }

    /// <summary>
    /// Retorna o sinal de value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int SignToInt(float value)
    {
        return Math.Sign(value);
    }
}
