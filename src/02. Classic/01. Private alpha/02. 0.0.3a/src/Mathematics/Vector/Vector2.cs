using System.Diagnostics.CodeAnalysis;

namespace Minecraft.Mathematics;

/// <summary>
/// Representação de vetores e pontos em 2D.
/// </summary>
public struct Vector2
{
    /// <summary>
    /// Componente X do vetor.
    /// </summary>
    public float X;

    /// <summary>
    /// Componente Y do vetor.
    /// </summary>
    public float Y;

    //
    // --------------------------------------------------

    /// <summary>
    /// Forma abreviada de escrever Vector2(0.0f, 0.0f).
    /// </summary>
    public static Vector2 Zero => new(0.0f, 0.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector2(1.0f, 1.0f).
    /// </summary>
    public static Vector2 Positive => new(1.0f, 1.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector2(-1.0f, -1.0f).
    /// </summary>
    public static Vector2 Negative => new(-1.0f, -1.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector2(1.0f, 0.0f).
    /// </summary>
    public static Vector2 PositiveX => new(1.0f, 0.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector2(-1.0f, 0.0f).
    /// </summary>
    public static Vector2 NegativeX => new(-1.0f, 0.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector2(0.0f, 1.0f).
    /// </summary>
    public static Vector2 PositiveY => new(0.0f, 1.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector2(0.0f, -1.0f).
    /// </summary>
    public static Vector2 NegativeY => new(0.0f, -1.0f);

    // Construtor
    // --------------------------------------------------

    /// <summary>
    /// Constrói um novo vetor com as componentes x e y fornecidas.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Constrói um novo vetor com o valor fornecido.
    /// </summary>
    /// <param name="value"></param>
    public Vector2(float value)
    {
        X = value;
        Y = value;
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
    public static Vector2 Normalize(Vector2 value)
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
        return $"{X}, {Y}";
    }

    // Equals
    // --------------------------------------------------

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Vector2 other && Equals(other);
    }

    public bool Equals(Vector2 other)
    {
        return X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    // GetHashCode
    // --------------------------------------------------

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    // Conversão Implicita
    // --------------------------------------------------

    public static implicit operator System.Numerics.Vector2(Vector2 vector)
    {
        return new System.Numerics.Vector2(
            vector.X,
            vector.Y
        );
    }

    public static implicit operator Vector2(System.Numerics.Vector2 vector)
    {
        return new Vector2(
            vector.X,
            vector.Y
        );
    }

    // Multiplicação
    // --------------------------------------------------

    public static Vector2 operator *(Vector2 a, Vector2 b)
    {
        return new Vector2(
            a.X * b.X,
            a.Y * b.Y
        );
    }

    public static Vector2 operator *(Vector2 a, float b)
    {
        return new Vector2(
            a.X * b,
            a.Y * b
        );
    }

    public static Vector2 operator *(float a, Vector2 b)
    {
        return new Vector2(
            a * b.X,
            a * b.Y
        );
    }

    // Divisão
    // --------------------------------------------------

    public static Vector2 operator /(Vector2 a, Vector2 b)
    {
        return new Vector2(
            a.X / b.X,
            a.Y / b.Y
        );
    }

    public static Vector2 operator /(Vector2 a, float b)
    {
        return new Vector2(
            a.X / b,
            a.Y / b
        );
    }

    public static Vector2 operator /(float a, Vector2 b)
    {
        return new Vector2(
            a / b.X,
            a / b.Y
        );
    }

    // Adição
    // --------------------------------------------------

    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(
            a.X + b.X,
            a.Y + b.Y
        );
    }

    public static Vector2 operator +(Vector2 a, float b)
    {
        return new Vector2(
            a.X + b,
            a.Y + b
        );
    }

    public static Vector2 operator +(float a, Vector2 b)
    {
        return new Vector2(
            a + b.X,
            a + b.Y
        );
    }

    // Subtração
    // --------------------------------------------------

    public static Vector2 operator -(Vector2 a, Vector2 b)
    {
        return new Vector2(
            a.X - b.X,
            a.Y - b.Y
        );
    }

    public static Vector2 operator -(Vector2 a, float b)
    {
        return new Vector2(
            a.X - b,
            a.Y - b
        );
    }

    public static Vector2 operator -(float a, Vector2 b)
    {
        return new Vector2(
            a - b.X,
            a - b.Y
        );
    }

    // Igualdade
    // --------------------------------------------------

    public static bool operator ==(Vector2 a, Vector2 b)
    {
        return a.X == b.X &&
               a.Y == b.Y;
    }

    public static bool operator ==(Vector2 a, float b)
    {
        return a.X == b &&
               a.Y == b;
    }

    public static bool operator ==(float a, Vector2 b)
    {
        return a == b.X &&
               a == b.Y;
    }

    // Desigualdade
    // --------------------------------------------------

    public static bool operator !=(Vector2 a, Vector2 b)
    {
        return a.X != b.X ||
               a.Y != b.Y;
    }

    public static bool operator !=(Vector2 a, float b)
    {
        return a.X != b ||
               a.Y != b;
    }

    public static bool operator !=(float a, Vector2 b)
    {
        return a != b.X ||
               a != b.Y;
    }
}
