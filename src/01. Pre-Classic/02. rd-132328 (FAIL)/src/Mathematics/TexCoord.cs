using System.Diagnostics.CodeAnalysis;

namespace RubyDung.Mathematics;

/// <summary>
/// Representação de vetores e pontos em 2D.
/// </summary>
public struct TexCoord
{
    /// <summary>
    /// Componente U do vetor.
    /// </summary>
    public float U;

    /// <summary>
    /// Componente V do vetor.
    /// </summary>
    public float V;

    //
    // --------------------------------------------------

    // Construtor
    // --------------------------------------------------

    /// <summary>
    /// Constrói um novo vetor com as componentes x e y fornecidas.
    /// </summary>
    /// <param name="u"></param>
    /// <param name="v"></param>
    public TexCoord(float u, float v)
    {
        U = u;
        V = v;
    }

    // Normalize
    // --------------------------------------------------

    /// <summary>
    /// Faz com que este vetor tenha magnitude 1.
    /// </summary>
    public void Normalize()
    {
        this = System.Numerics.Vector2.Normalize(this);
    }

    /// <summary>
    /// Faz com que este vetor tenha magnitude 1.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TexCoord Normalize(TexCoord value)
    {
        return System.Numerics.Vector2.Normalize(value);
    }

    // To String
    // --------------------------------------------------

    /// <summary>
    /// Retorna uma string bem formatada para este vetor.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{U}, {V}";
    }

    // Equals
    // --------------------------------------------------

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is TexCoord other && Equals(other);
    }

    public bool Equals(TexCoord other)
    {
        return U.Equals(other.U) &&
               V.Equals(other.V);
    }

    // GetHashCode
    // --------------------------------------------------

    public override int GetHashCode()
    {
        return HashCode.Combine(U, V);
    }

    // Conversão Implicita
    // --------------------------------------------------

    public static implicit operator System.Numerics.Vector2(TexCoord vector)
    {
        return new System.Numerics.Vector2(
            vector.U,
            vector.V
        );
    }

    public static implicit operator TexCoord(System.Numerics.Vector2 vector)
    {
        return new TexCoord(
            vector.X,
            vector.Y
        );
    }

    // Multiplicação
    // --------------------------------------------------

    public static TexCoord operator *(TexCoord a, TexCoord b)
    {
        return new TexCoord(
            a.U * b.U,
            a.V * b.V
        );
    }

    public static TexCoord operator *(TexCoord a, float b)
    {
        return new TexCoord(
            a.U * b,
            a.V * b
        );
    }

    public static TexCoord operator *(float a, TexCoord b)
    {
        return new TexCoord(
            a * b.U,
            a * b.V
        );
    }

    // Divisão
    // --------------------------------------------------

    public static TexCoord operator /(TexCoord a, TexCoord b)
    {
        return new TexCoord(
            a.U / b.U,
            a.V / b.V
        );
    }

    public static TexCoord operator /(TexCoord a, float b)
    {
        return new TexCoord(
            a.U / b,
            a.V / b
        );
    }

    public static TexCoord operator /(float a, TexCoord b)
    {
        return new TexCoord(
            a / b.U,
            a / b.V
        );
    }

    // Adição
    // --------------------------------------------------

    public static TexCoord operator +(TexCoord a, TexCoord b)
    {
        return new TexCoord(
            a.U + b.U,
            a.V + b.V
        );
    }

    public static TexCoord operator +(TexCoord a, float b)
    {
        return new TexCoord(
            a.U + b,
            a.V + b
        );
    }

    public static TexCoord operator +(float a, TexCoord b)
    {
        return new TexCoord(
            a + b.U,
            a + b.V
        );
    }

    // Subtração
    // --------------------------------------------------

    public static TexCoord operator -(TexCoord a, TexCoord b)
    {
        return new TexCoord(
            a.U - b.U,
            a.V - b.V
        );
    }

    public static TexCoord operator -(TexCoord a, float b)
    {
        return new TexCoord(
            a.U - b,
            a.V - b
        );
    }

    public static TexCoord operator -(float a, TexCoord b)
    {
        return new TexCoord(
            a - b.U,
            a - b.V
        );
    }

    // Igualdade
    // --------------------------------------------------

    public static bool operator ==(TexCoord a, TexCoord b)
    {
        return a.U == b.U &&
               a.V == b.V;
    }

    public static bool operator ==(TexCoord a, float b)
    {
        return a.U == b &&
               a.V == b;
    }

    public static bool operator ==(float a, TexCoord b)
    {
        return a == b.U &&
               a == b.V;
    }

    // Desigualdade
    // --------------------------------------------------

    public static bool operator !=(TexCoord a, TexCoord b)
    {
        return a.U != b.U ||
               a.V != b.V;
    }

    public static bool operator !=(TexCoord a, float b)
    {
        return a.U != b ||
               a.V != b;
    }

    public static bool operator !=(float a, TexCoord b)
    {
        return a != b.U ||
               a != b.V;
    }
}
