using System.Diagnostics.CodeAnalysis;

namespace RubyDung.Mathematics;

/// <summary>
/// Representação de vetores e pontos em 2D.
/// </summary>
public struct Vector2Int
{
    /// <summary>
    /// Componente X do vetor.
    /// </summary>
    public int X;

    /// <summary>
    /// Componente Y do vetor.
    /// </summary>
    public int Y;

    //
    // --------------------------------------------------

    /// <summary>
    /// Forma abreviada de escrever Vector2Int(0, 0).
    /// </summary>
    public static Vector2Int Zero => new(0, 0);

    /// <summary>
    /// Forma abreviada de escrever Vector2Int(1, 1).
    /// </summary>
    public static Vector2Int Positive => new(1, 1);

    /// <summary>
    /// Forma abreviada de escrever Vector2Int(-1, -1).
    /// </summary>
    public static Vector2Int Negative => new(-1, -1);

    /// <summary>
    /// Forma abreviada de escrever Vector2Int(1, 0).
    /// </summary>
    public static Vector2Int PositiveX => new(1, 0);

    /// <summary>
    /// Forma abreviada de escrever Vector2Int(-1, 0).
    /// </summary>
    public static Vector2Int NegativeX => new(-1, 0);

    /// <summary>
    /// Forma abreviada de escrever Vector2Int(0, 1).
    /// </summary>
    public static Vector2Int PositiveY => new(0, 1);

    /// <summary>
    /// Forma abreviada de escrever Vector2Int(0, -1).
    /// </summary>
    public static Vector2Int NegativeY => new(0, -1);

    // Construtor
    // --------------------------------------------------

    /// <summary>
    /// Constrói um novo vetor com as componentes x e y fornecidas.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Constrói um novo vetor com o valor fornecido.
    /// </summary>
    /// <param name="value"></param>
    public Vector2Int(int value)
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
    public static Vector2Int Normalize(Vector2 value)
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
        return obj is Vector2Int other && Equals(other);
    }

    public bool Equals(Vector2Int other)
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

    public static implicit operator System.Numerics.Vector2(Vector2Int vector)
    {
        return new System.Numerics.Vector2(
            vector.X,
            vector.Y
        );
    }

    public static implicit operator Vector2Int(System.Numerics.Vector2 vector)
    {
        return new Vector2Int(
            (int)vector.X,
            (int)vector.Y
        );
    }

    // Multiplicação
    // --------------------------------------------------

    public static Vector2Int operator *(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(
            a.X * b.X,
            a.Y * b.Y
        );
    }

    public static Vector2Int operator *(Vector2Int a, int b)
    {
        return new Vector2Int(
            a.X * b,
            a.Y * b
        );
    }

    public static Vector2Int operator *(int a, Vector2Int b)
    {
        return new Vector2Int(
            a * b.X,
            a * b.Y
        );
    }

    // Divisão
    // --------------------------------------------------

    public static Vector2Int operator /(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(
            a.X / b.X,
            a.Y / b.Y
        );
    }

    public static Vector2Int operator /(Vector2Int a, int b)
    {
        return new Vector2Int(
            a.X / b,
            a.Y / b
        );
    }

    public static Vector2Int operator /(int a, Vector2Int b)
    {
        return new Vector2Int(
            a / b.X,
            a / b.Y
        );
    }

    // Adição
    // --------------------------------------------------

    public static Vector2Int operator +(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(
            a.X + b.X,
            a.Y + b.Y
        );
    }

    public static Vector2Int operator +(Vector2Int a, int b)
    {
        return new Vector2Int(
            a.X + b,
            a.Y + b
        );
    }

    public static Vector2Int operator +(int a, Vector2Int b)
    {
        return new Vector2Int(
            a + b.X,
            a + b.Y
        );
    }

    // Subtração
    // --------------------------------------------------

    public static Vector2Int operator -(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(
            a.X - b.X,
            a.Y - b.Y
        );
    }

    public static Vector2Int operator -(Vector2Int a, int b)
    {
        return new Vector2Int(
            a.X - b,
            a.Y - b
        );
    }

    public static Vector2Int operator -(int a, Vector2Int b)
    {
        return new Vector2Int(
            a - b.X,
            a - b.Y
        );
    }

    // Igualdade
    // --------------------------------------------------

    public static bool operator ==(Vector2Int a, Vector2Int b)
    {
        return a.X == b.X &&
               a.Y == b.Y;
    }

    public static bool operator ==(Vector2Int a, int b)
    {
        return a.X == b &&
               a.Y == b;
    }

    public static bool operator ==(int a, Vector2Int b)
    {
        return a == b.X &&
               a == b.Y;
    }

    // Desigualdade
    // --------------------------------------------------

    public static bool operator !=(Vector2Int a, Vector2Int b)
    {
        return a.X != b.X ||
               a.Y != b.Y;
    }

    public static bool operator !=(Vector2Int a, int b)
    {
        return a.X != b ||
               a.Y != b;
    }

    public static bool operator !=(int a, Vector2Int b)
    {
        return a != b.X ||
               a != b.Y;
    }
}
