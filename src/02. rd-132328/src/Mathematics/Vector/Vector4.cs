using System.Diagnostics.CodeAnalysis;

namespace RubyDung.Mathematics;

/// <summary>
/// Representação de vetores e pontos em 4D.
/// </summary>
public struct Vector4
{
    /// <summary>
    /// Componente X do vetor.
    /// </summary>
    public float X;

    /// <summary>
    /// Componente Y do vetor.
    /// </summary>
    public float Y;

    /// <summary>
    /// Componente Z do vetor.
    /// </summary>
    public float Z;

    /// <summary>
    /// Componente W do vetor.
    /// </summary>
    public float W;

    //
    // --------------------------------------------------

    /// <summary>
    /// Forma abreviada de escrever Vector4(0.0f, 0.0f, 0.0f, 0.0f).
    /// </summary>
    public static Vector4 Zero => new(0.0f, 0.0f, 0.0f, 0.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector4(1.0f, 1.0f, 1.0f, 1.0f).
    /// </summary>
    public static Vector4 Positive => new(1.0f, 1.0f, 1.0f, 1.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector4(-1.0f, -1.0f, -1.0f, -1.0f).
    /// </summary>
    public static Vector4 Negative => new(-1.0f, -1.0f, -1.0f, -1.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector4(1.0f, 0.0f, 0.0f, 0.0f).
    /// </summary>
    public static Vector4 PositiveX => new(1.0f, 0.0f, 0.0f, 0.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector4(-1.0f, 0.0f, 0.0f, 0.0f).
    /// </summary>
    public static Vector4 NegativeX => new(-1.0f, 0.0f, 0.0f, 0.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector4(0.0f, 1.0f, 0.0f, 0.0f).
    /// </summary>
    public static Vector4 PositiveY => new(0.0f, 1.0f, 0.0f, 0.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector4(0.0f, -1.0f, 0.0f, 0.0f).
    /// </summary>
    public static Vector4 NegativeY => new(0.0f, -1.0f, 0.0f, 0.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector4(0.0f, 0.0f, 1.0f, 0.0f).
    /// </summary>
    public static Vector4 PositiveZ => new(0.0f, 0.0f, 1.0f, 0.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector4(0.0f, 0.0f, -1.0f, 0.0f).
    /// </summary>
    public static Vector4 NegativeZ => new(0.0f, 0.0f, -1.0f, 0.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector4(0.0f, 0.0f, 0.0f, 1.0f).
    /// </summary>
    public static Vector4 PositiveW => new(0.0f, 0.0f, 0.0f, 1.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector4(0.0f, 0.0f, 0.0f, -1.0f).
    /// </summary>
    public static Vector4 NegativeW => new(0.0f, 0.0f, 0.0f, -1.0f);

    // Construtor
    // --------------------------------------------------

    /// <summary>
    /// Constrói um novo vetor com as componentes x, y, z e w fornecidas.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Vector4(float x, float y, float z, float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Constrói um novo vetor com o valor fornecido.
    /// </summary>
    /// <param name="value"></param>
    public Vector4(float value)
    {
        X = value;
        Y = value;
        Z = value;
        W = value;
    }

    public Vector4(Vector2 vector, float z, float w)
    {
        X = vector.X;
        Y = vector.Y;
        Z = z;
        W = w;
    }

    public Vector4(Vector3 vector, float w)
    {
        X = vector.X;
        Y = vector.Y;
        Z = vector.Z;
        W = w;
    }

    // Normalize
    // --------------------------------------------------

    /// <summary>
    /// Faz com que este vetor tenha magnitude 1.
    /// </summary>
    public void Normalize()
    {
        this = System.Numerics.Vector4.Normalize(this);
    }

    /// <summary>
    /// Faz com que este vetor tenha magnitude 1.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Vector4 Normalize(Vector4 value)
    {
        return System.Numerics.Vector4.Normalize(value);
    }

    // To String
    // --------------------------------------------------

    /// <summary>
    /// Retorna uma string bem formatada para este vetor.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{X}, {Y}, {Z}, {W}";
    }

    // Equals
    // --------------------------------------------------

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Vector4 other && Equals(other);
    }

    public bool Equals(Vector4 other)
    {
        return X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    // GetHashCode
    // --------------------------------------------------

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z, W);
    }

    // Conversão Implicita
    // --------------------------------------------------

    public static implicit operator System.Numerics.Vector4(Vector4 vector)
    {
        return new System.Numerics.Vector4(
            vector.X,
            vector.Y,
            vector.Z,
            vector.W
        );
    }

    public static implicit operator Vector4(System.Numerics.Vector4 vector)
    {
        return new Vector4(
            vector.X,
            vector.Y,
            vector.Z,
            vector.W
        );
    }

    // Multiplicação
    // --------------------------------------------------

    public static Vector4 operator *(Vector4 a, Vector4 b)
    {
        return new Vector4(
            a.X * b.X,
            a.Y * b.Y,
            a.Z * b.Z,
            a.W * b.W
        );
    }

    public static Vector4 operator *(Vector4 a, float b)
    {
        return new Vector4(
            a.X * b,
            a.Y * b,
            a.Z * b,
            a.W * b
        );
    }

    public static Vector4 operator *(float a, Vector4 b)
    {
        return new Vector4(
            a * b.X,
            a * b.Y,
            a * b.Z,
            a * b.W
        );
    }

    // Divisão
    // --------------------------------------------------

    public static Vector4 operator /(Vector4 a, Vector4 b)
    {
        return new Vector4(
            a.X / b.X,
            a.Y / b.Y,
            a.Z / b.Z,
            a.W / b.W
        );
    }

    public static Vector4 operator /(Vector4 a, float b)
    {
        return new Vector4(
            a.X / b,
            a.Y / b,
            a.Z / b,
            a.W / b
        );
    }

    public static Vector4 operator /(float a, Vector4 b)
    {
        return new Vector4(
            a / b.X,
            a / b.Y,
            a / b.Z,
            a / b.W
        );
    }

    // Adição
    // --------------------------------------------------

    public static Vector4 operator +(Vector4 a, Vector4 b)
    {
        return new Vector4(
            a.X + b.X,
            a.Y + b.Y,
            a.Z + b.Z,
            a.W + b.W
        );
    }

    public static Vector4 operator +(Vector4 a, float b)
    {
        return new Vector4(
            a.X + b,
            a.Y + b,
            a.Z + b,
            a.W + b
        );
    }

    public static Vector4 operator +(float a, Vector4 b)
    {
        return new Vector4(
            a + b.X,
            a + b.Y,
            a + b.Z,
            a + b.W
        );
    }

    // Subtração
    // --------------------------------------------------

    public static Vector4 operator -(Vector4 a, Vector4 b)
    {
        return new Vector4(
            a.X - b.X,
            a.Y - b.Y,
            a.Z - b.Z,
            a.W - b.Z
        );
    }

    public static Vector4 operator -(Vector4 a, float b)
    {
        return new Vector4(
            a.X - b,
            a.Y - b,
            a.Z - b,
            a.W - b
        );
    }

    public static Vector4 operator -(float a, Vector4 b)
    {
        return new Vector4(
            a - b.X,
            a - b.Y,
            a - b.Z,
            a - b.W
        );
    }

    // Igualdade
    // --------------------------------------------------

    public static bool operator ==(Vector4 a, Vector4 b)
    {
        return a.X == b.X &&
               a.Y == b.Y &&
               a.Z == b.Z &&
               a.W == b.W;
    }

    public static bool operator ==(Vector4 a, float b)
    {
        return a.X == b &&
               a.Y == b &&
               a.Z == b &&
               a.W == b;
    }

    public static bool operator ==(float a, Vector4 b)
    {
        return a == b.X &&
               a == b.Y &&
               a == b.Z &&
               a == b.W;
    }

    // Desigualdade
    // --------------------------------------------------

    public static bool operator !=(Vector4 a, Vector4 b)
    {
        return a.X != b.X ||
               a.Y != b.Y ||
               a.Z != b.Z ||
               a.W != b.W;
    }

    public static bool operator !=(Vector4 a, float b)
    {
        return a.X != b ||
               a.Y != b ||
               a.Z != b ||
               a.W != b;
    }

    public static bool operator !=(float a, Vector4 b)
    {
        return a != b.X ||
               a != b.Y ||
               a != b.Z ||
               a != b.W;
    }
}
