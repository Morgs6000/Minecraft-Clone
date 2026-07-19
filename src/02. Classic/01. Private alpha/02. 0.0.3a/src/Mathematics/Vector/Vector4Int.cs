using System.Diagnostics.CodeAnalysis;

namespace Minecraft.Mathematics;

/// <summary>
/// Representação de vetores e pontos em 4D.
/// </summary>
public struct Vector4Int
{
    /// <summary>
    /// Componente X do vetor.
    /// </summary>
    public int X;

    /// <summary>
    /// Componente Y do vetor.
    /// </summary>
    public int Y;

    /// <summary>
    /// Componente Z do vetor.
    /// </summary>
    public int Z;

    /// <summary>
    /// Componente W do vetor.
    /// </summary>
    public int W;

    //
    // --------------------------------------------------

    /// <summary>
    /// Forma abreviada de escrever Vector4Int(0, 0, 0, 0f).
    /// </summary>
    public static Vector4Int Zero => new(0, 0, 0, 0);

    /// <summary>
    /// Forma abreviada de escrever Vector4Int(1, 1, 1, 1).
    /// </summary>
    public static Vector4Int Positive => new(1, 1, 1, 1);

    /// <summary>
    /// Forma abreviada de escrever Vector4Int(-1, -1, -1, -1).
    /// </summary>
    public static Vector4Int Negative => new(-1, -1, -1, -1);

    /// <summary>
    /// Forma abreviada de escrever Vector4Int(1, 0, 0, 0).
    /// </summary>
    public static Vector4Int PositiveX => new(1, 0, 0, 0);

    /// <summary>
    /// Forma abreviada de escrever Vector4Int(-1, 0, 0, 0).
    /// </summary>
    public static Vector4Int NegativeX => new(-1, 0, 0, 0);

    /// <summary>
    /// Forma abreviada de escrever Vector4Int(0, 1, 0, 0).
    /// </summary>
    public static Vector4Int PositiveY => new(0, 1, 0, 0);

    /// <summary>
    /// Forma abreviada de escrever Vector4Int(0, -1, 0, 0).
    /// </summary>
    public static Vector4Int NegativeY => new(0, -1, 0, 0);

    /// <summary>
    /// Forma abreviada de escrever Vector4Int(0, 0, 1, 0).
    /// </summary>
    public static Vector4Int PositiveZ => new(0, 0, 1, 0);

    /// <summary>
    /// Forma abreviada de escrever Vector4Int(0, 0, -1, 0).
    /// </summary>
    public static Vector4Int NegativeZ => new(0, 0, -1, 0);

    /// <summary>
    /// Forma abreviada de escrever Vector4Int(0, 0, 0, 1).
    /// </summary>
    public static Vector4Int PositiveW => new(0, 0, 0, 1);

    /// <summary>
    /// Forma abreviada de escrever Vector4Int(0, 0, 0, -1).
    /// </summary>
    public static Vector4Int NegativeW => new(0, 0, 0, -1);

    // Construtor
    // --------------------------------------------------

    /// <summary>
    /// Constrói um novo vetor com as componentes x, y, z e w fornecidas.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Vector4Int(int x, int y, int z, int w)
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
    public Vector4Int(int value)
    {
        X = value;
        Y = value;
        Z = value;
        W = value;
    }

    public Vector4Int(Vector2Int vector, int z, int w)
    {
        X = vector.X;
        Y = vector.Y;
        Z = z;
        W = w;
    }

    public Vector4Int(Vector3Int vector, int w)
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
    public static Vector4Int Normalize(Vector4Int value)
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
        return obj is Vector4Int other && Equals(other);
    }

    public bool Equals(Vector4Int other)
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

    public static implicit operator System.Numerics.Vector4(Vector4Int vector)
    {
        return new System.Numerics.Vector4(
            vector.X,
            vector.Y,
            vector.Z,
            vector.W
        );
    }

    public static implicit operator Vector4Int(System.Numerics.Vector4 vector)
    {
        return new Vector4Int(
            (int)vector.X,
            (int)vector.Y,
            (int)vector.Z,
            (int)vector.W
        );
    }

    // Multiplicação
    // --------------------------------------------------

    public static Vector4Int operator *(Vector4Int a, Vector4Int b)
    {
        return new Vector4Int(
            a.X * b.X,
            a.Y * b.Y,
            a.Z * b.Z,
            a.W * b.W
        );
    }

    public static Vector4Int operator *(Vector4Int a, int b)
    {
        return new Vector4Int(
            a.X * b,
            a.Y * b,
            a.Z * b,
            a.W * b
        );
    }

    public static Vector4Int operator *(int a, Vector4Int b)
    {
        return new Vector4Int(
            a * b.X,
            a * b.Y,
            a * b.Z,
            a * b.W
        );
    }

    // Divisão
    // --------------------------------------------------

    public static Vector4Int operator /(Vector4Int a, Vector4Int b)
    {
        return new Vector4Int(
            a.X / b.X,
            a.Y / b.Y,
            a.Z / b.Z,
            a.W / b.W
        );
    }

    public static Vector4Int operator /(Vector4Int a, int b)
    {
        return new Vector4Int(
            a.X / b,
            a.Y / b,
            a.Z / b,
            a.W / b
        );
    }

    public static Vector4Int operator /(int a, Vector4Int b)
    {
        return new Vector4Int(
            a / b.X,
            a / b.Y,
            a / b.Z,
            a / b.W
        );
    }

    // Adição
    // --------------------------------------------------

    public static Vector4Int operator +(Vector4Int a, Vector4Int b)
    {
        return new Vector4Int(
            a.X + b.X,
            a.Y + b.Y,
            a.Z + b.Z,
            a.W + b.W
        );
    }

    public static Vector4Int operator +(Vector4Int a, int b)
    {
        return new Vector4Int(
            a.X + b,
            a.Y + b,
            a.Z + b,
            a.W + b
        );
    }

    public static Vector4Int operator +(int a, Vector4Int b)
    {
        return new Vector4Int(
            a + b.X,
            a + b.Y,
            a + b.Z,
            a + b.W
        );
    }

    // Subtração
    // --------------------------------------------------

    public static Vector4Int operator -(Vector4Int a, Vector4Int b)
    {
        return new Vector4Int(
            a.X - b.X,
            a.Y - b.Y,
            a.Z - b.Z,
            a.W - b.Z
        );
    }

    public static Vector4Int operator -(Vector4Int a, int b)
    {
        return new Vector4Int(
            a.X - b,
            a.Y - b,
            a.Z - b,
            a.W - b
        );
    }

    public static Vector4Int operator -(int a, Vector4Int b)
    {
        return new Vector4Int(
            a - b.X,
            a - b.Y,
            a - b.Z,
            a - b.W
        );
    }

    // Igualdade
    // --------------------------------------------------

    public static bool operator ==(Vector4Int a, Vector4Int b)
    {
        return a.X == b.X &&
               a.Y == b.Y &&
               a.Z == b.Z &&
               a.W == b.W;
    }

    public static bool operator ==(Vector4Int a, int b)
    {
        return a.X == b &&
               a.Y == b &&
               a.Z == b &&
               a.W == b;
    }

    public static bool operator ==(int a, Vector4Int b)
    {
        return a == b.X &&
               a == b.Y &&
               a == b.Z &&
               a == b.W;
    }

    // Desigualdade
    // --------------------------------------------------

    public static bool operator !=(Vector4Int a, Vector4Int b)
    {
        return a.X != b.X ||
               a.Y != b.Y ||
               a.Z != b.Z ||
               a.W != b.W;
    }

    public static bool operator !=(Vector4Int a, int b)
    {
        return a.X != b ||
               a.Y != b ||
               a.Z != b ||
               a.W != b;
    }

    public static bool operator !=(int a, Vector4Int b)
    {
        return a != b.X ||
               a != b.Y ||
               a != b.Z ||
               a != b.W;
    }
}
