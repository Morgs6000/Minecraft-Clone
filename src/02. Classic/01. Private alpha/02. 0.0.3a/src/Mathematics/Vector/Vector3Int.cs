using System.Diagnostics.CodeAnalysis;

namespace Minecraft.Mathematics;

/// <summary>
/// Representação de vetores e pontos em 3D.
/// </summary>
public struct Vector3Int
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

    //
    // --------------------------------------------------

    /// <summary>
    /// Forma abreviada de escrever Vector3Int(0, 0, 0).
    /// </summary>
    public static Vector3Int Zero => new(0, 0, 0);

    /// <summary>
    /// Forma abreviada de escrever Vector3Int(1, 1, 1).
    /// </summary>
    public static Vector3Int Positive => new(1, 1, 1);

    /// <summary>
    /// Forma abreviada de escrever Vector3Int(-1, -1, -1).
    /// </summary>
    public static Vector3Int Negative => new(-1, -1, -1);

    /// <summary>
    /// Forma abreviada de escrever Vector3Int(1, 0, 0).
    /// </summary>
    public static Vector3Int PositiveX => new(1, 0, 0);

    /// <summary>
    /// Forma abreviada de escrever Vector3Int(-1, 0, 0).
    /// </summary>
    public static Vector3Int NegativeX => new(-1, 0, 0);

    /// <summary>
    /// Forma abreviada de escrever Vector3Int(0, 1, 0).
    /// </summary>
    public static Vector3Int PositiveY => new(0, 1, 0);

    /// <summary>
    /// Forma abreviada de escrever Vector3Int(0, -1, 0).
    /// </summary>
    public static Vector3Int NegativeY => new(0, -1, 0);

    /// <summary>
    /// Forma abreviada de escrever Vector3Int(0, 0, 1).
    /// </summary>
    public static Vector3Int PositiveZ => new(0, 0, 1);

    /// <summary>
    /// Forma abreviada de escrever Vector3Int(0, 0, -1).
    /// </summary>
    public static Vector3Int NegativeZ => new(0, 0, -1);

    // Construtor
    // --------------------------------------------------

    /// <summary>
    /// Constrói um novo vetor com as componentes x, y e z fornecidas.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Vector3Int(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Constrói um novo vetor com o valor fornecido.
    /// </summary>
    /// <param name="value"></param>
    public Vector3Int(int value)
    {
        X = value;
        Y = value;
        Z = value;
    }

    public Vector3Int(Vector3Int vector, int z)
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
    public static Vector3Int Normalize(Vector3Int value)
    {
        return System.Numerics.Vector3.Normalize(value);
    }

    // Cross
    // --------------------------------------------------

    public static Vector3Int Cross(Vector3Int a, Vector3Int b)
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
        return obj is Vector3Int other && Equals(other);
    }

    public bool Equals(Vector3Int other)
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

    public static implicit operator System.Numerics.Vector3(Vector3Int vector)
    {
        return new System.Numerics.Vector3(
            vector.X,
            vector.Y,
            vector.Z
        );
    }

    public static implicit operator Vector3Int(System.Numerics.Vector3 vector)
    {
        return new Vector3Int(
            (int)vector.X,
            (int)vector.Y,
            (int)vector.Z
        );
    }

    // Multiplicação
    // --------------------------------------------------

    public static Vector3Int operator *(Vector3Int a, Vector3Int b)
    {
        return new Vector3Int(
            a.X * b.X,
            a.Y * b.Y,
            a.Z * b.Z
        );
    }

    public static Vector3Int operator *(Vector3Int a, int b)
    {
        return new Vector3Int(
            a.X * b,
            a.Y * b,
            a.Z * b
        );
    }

    public static Vector3Int operator *(int a, Vector3Int b)
    {
        return new Vector3Int(
            a * b.X,
            a * b.Y,
            a * b.Z
        );
    }

    // Divisão
    // --------------------------------------------------

    public static Vector3Int operator /(Vector3Int a, Vector3Int b)
    {
        return new Vector3Int(
            a.X / b.X,
            a.Y / b.Y,
            a.Z / b.Z
        );
    }

    public static Vector3Int operator /(Vector3Int a, int b)
    {
        return new Vector3Int(
            a.X / b,
            a.Y / b,
            a.Z / b
        );
    }

    public static Vector3Int operator /(int a, Vector3Int b)
    {
        return new Vector3Int(
            a / b.X,
            a / b.Y,
            a / b.Z
        );
    }

    // Adição
    // --------------------------------------------------

    public static Vector3Int operator +(Vector3Int a, Vector3Int b)
    {
        return new Vector3Int(
            a.X + b.X,
            a.Y + b.Y,
            a.Z + b.Z
        );
    }

    public static Vector3Int operator +(Vector3Int a, int b)
    {
        return new Vector3Int(
            a.X + b,
            a.Y + b,
            a.Z + b
        );
    }

    public static Vector3Int operator +(int a, Vector3Int b)
    {
        return new Vector3Int(
            a + b.X,
            a + b.Y,
            a + b.Z
        );
    }

    // Subtração
    // --------------------------------------------------

    public static Vector3Int operator -(Vector3Int a, Vector3Int b)
    {
        return new Vector3Int(
            a.X - b.X,
            a.Y - b.Y,
            a.Z - b.Z
        );
    }

    public static Vector3Int operator -(Vector3Int a, int b)
    {
        return new Vector3Int(
            a.X - b,
            a.Y - b,
            a.Z - b
        );
    }

    public static Vector3Int operator -(int a, Vector3Int b)
    {
        return new Vector3Int(
            a - b.X,
            a - b.Y,
            a - b.Z
        );
    }

    // Igualdade
    // --------------------------------------------------

    public static bool operator ==(Vector3Int a, Vector3Int b)
    {
        return a.X == b.X &&
               a.Y == b.Y &&
               a.Z == b.Z;
    }

    public static bool operator ==(Vector3Int a, int b)
    {
        return a.X == b &&
               a.Y == b &&
               a.Z == b;
    }

    public static bool operator ==(int a, Vector3Int b)
    {
        return a == b.X &&
               a == b.Y &&
               a == b.Z;
    }

    // Desigualdade
    // --------------------------------------------------

    public static bool operator !=(Vector3Int a, Vector3Int b)
    {
        return a.X != b.X ||
               a.Y != b.Y ||
               a.Z != b.Z;
    }

    public static bool operator !=(Vector3Int a, int b)
    {
        return a.X != b ||
               a.Y != b ||
               a.Z != b;
    }

    public static bool operator !=(int a, Vector3Int b)
    {
        return a != b.X ||
               a != b.Y ||
               a != b.Z;
    }
}
