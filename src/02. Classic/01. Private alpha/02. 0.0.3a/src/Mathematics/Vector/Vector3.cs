using System.Diagnostics.CodeAnalysis;

namespace Minecraft.Mathematics;

/// <summary>
/// Representação de vetores e pontos em 3D.
/// </summary>
public struct Vector3
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

    //
    // --------------------------------------------------

    /// <summary>
    /// Forma abreviada de escrever Vector3(0.0f, 0.0f, 0.0f).
    /// </summary>
    public static Vector3 Zero => new(0.0f, 0.0f, 0.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector3(1.0f, 1.0f, 1.0f).
    /// </summary>
    public static Vector3 Positive => new(1.0f, 1.0f, 1.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector3(-1.0f, -1.0f, -1.0f).
    /// </summary>
    public static Vector3 Negative => new(-1.0f, -1.0f, -1.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector3(1.0f, 0.0f, 0.0f).
    /// </summary>
    public static Vector3 PositiveX => new(1.0f, 0.0f, 0.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector3(-1.0f, 0.0f, 0.0f).
    /// </summary>
    public static Vector3 NegativeX => new(-1.0f, 0.0f, 0.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector3(0.0f, 1.0f, 0.0f).
    /// </summary>
    public static Vector3 PositiveY => new(0.0f, 1.0f, 0.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector3(0.0f, -1.0f, 0.0f).
    /// </summary>
    public static Vector3 NegativeY => new(0.0f, -1.0f, 0.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector3(0.0f, 0.0f, 1.0f).
    /// </summary>
    public static Vector3 PositiveZ => new(0.0f, 0.0f, 1.0f);

    /// <summary>
    /// Forma abreviada de escrever Vector3(0.0f, 0.0f, -1.0f).
    /// </summary>
    public static Vector3 NegativeZ => new(0.0f, 0.0f, -1.0f);

    // Construtor
    // --------------------------------------------------

    /// <summary>
    /// Constrói um novo vetor com as componentes x, y e z fornecidas.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Vector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Constrói um novo vetor com o valor fornecido.
    /// </summary>
    /// <param name="value"></param>
    public Vector3(float value)
    {
        X = value;
        Y = value;
        Z = value;
    }

    public Vector3(Vector2 vector, float z)
    {
        X = vector.X;
        Y = vector.Y;
        Z = z;
    }

    // Normalize
    // --------------------------------------------------

    /// <summary>
    /// Faz com que este vetor tenha magnitude 1.
    /// </summary>
    public void Normalize()
    {
        this = System.Numerics.Vector3.Normalize(this);
    }

    /// <summary>
    /// Faz com que este vetor tenha magnitude 1.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Vector3 Normalize(Vector3 value)
    {
        return System.Numerics.Vector3.Normalize(value);
    }

    // Cross
    // --------------------------------------------------

    public static Vector3 Cross(Vector3 a, Vector3 b)
    {
        return System.Numerics.Vector3.Cross(a, b);
    }

    // To String
    // --------------------------------------------------

    /// <summary>
    /// Retorna uma string bem formatada para este vetor.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{X}, {Y}, {Z}";
    }

    // Equals
    // --------------------------------------------------

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Vector3 other && Equals(other);
    }

    public bool Equals(Vector3 other)
    {
        return X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    // GetHashCode
    // --------------------------------------------------

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    // Conversão Implicita
    // --------------------------------------------------

    public static implicit operator System.Numerics.Vector3(Vector3 vector)
    {
        return new System.Numerics.Vector3(
            vector.X,
            vector.Y,
            vector.Z
        );
    }

    public static implicit operator Vector3(System.Numerics.Vector3 vector)
    {
        return new Vector3(
            vector.X,
            vector.Y,
            vector.Z
        );
    }

    // Multiplicação
    // --------------------------------------------------

    public static Vector3 operator *(Vector3 a, Vector3 b)
    {
        return new Vector3(
            a.X * b.X,
            a.Y * b.Y,
            a.Z * b.Z
        );
    }

    public static Vector3 operator *(Vector3 a, float b)
    {
        return new Vector3(
            a.X * b,
            a.Y * b,
            a.Z * b
        );
    }

    public static Vector3 operator *(float a, Vector3 b)
    {
        return new Vector3(
            a * b.X,
            a * b.Y,
            a * b.Z
        );
    }

    // Divisão
    // --------------------------------------------------

    public static Vector3 operator /(Vector3 a, Vector3 b)
    {
        return new Vector3(
            a.X / b.X,
            a.Y / b.Y,
            a.Z / b.Z
        );
    }

    public static Vector3 operator /(Vector3 a, float b)
    {
        return new Vector3(
            a.X / b,
            a.Y / b,
            a.Z / b
        );
    }

    public static Vector3 operator /(float a, Vector3 b)
    {
        return new Vector3(
            a / b.X,
            a / b.Y,
            a / b.Z
        );
    }

    // Adição
    // --------------------------------------------------

    public static Vector3 operator +(Vector3 a, Vector3 b)
    {
        return new Vector3(
            a.X + b.X,
            a.Y + b.Y,
            a.Z + b.Z
        );
    }

    public static Vector3 operator +(Vector3 a, float b)
    {
        return new Vector3(
            a.X + b,
            a.Y + b,
            a.Z + b
        );
    }

    public static Vector3 operator +(float a, Vector3 b)
    {
        return new Vector3(
            a + b.X,
            a + b.Y,
            a + b.Z
        );
    }

    // Subtração
    // --------------------------------------------------

    public static Vector3 operator -(Vector3 a, Vector3 b)
    {
        return new Vector3(
            a.X - b.X,
            a.Y - b.Y,
            a.Z - b.Z
        );
    }

    public static Vector3 operator -(Vector3 a, float b)
    {
        return new Vector3(
            a.X - b,
            a.Y - b,
            a.Z - b
        );
    }

    public static Vector3 operator -(float a, Vector3 b)
    {
        return new Vector3(
            a - b.X,
            a - b.Y,
            a - b.Z
        );
    }

    // Igualdade
    // --------------------------------------------------

    public static bool operator ==(Vector3 a, Vector3 b)
    {
        return a.X == b.X &&
               a.Y == b.Y &&
               a.Z == b.Z;
    }

    public static bool operator ==(Vector3 a, float b)
    {
        return a.X == b &&
               a.Y == b &&
               a.Z == b;
    }

    public static bool operator ==(float a, Vector3 b)
    {
        return a == b.X &&
               a == b.Y &&
               a == b.Z;
    }

    // Desigualdade
    // --------------------------------------------------

    public static bool operator !=(Vector3 a, Vector3 b)
    {
        return a.X != b.X ||
               a.Y != b.Y ||
               a.Z != b.Z;
    }

    public static bool operator !=(Vector3 a, float b)
    {
        return a.X != b ||
               a.Y != b ||
               a.Z != b;
    }

    public static bool operator !=(float a, Vector3 b)
    {
        return a != b.X ||
               a != b.Y ||
               a != b.Z;
    }
}
