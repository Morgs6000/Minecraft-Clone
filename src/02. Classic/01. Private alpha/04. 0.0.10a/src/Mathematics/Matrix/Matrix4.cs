namespace Minecraft.Mathematics;

/// <summary>
/// Uma matriz de transformação 4x4 padrão.
/// </summary>
public struct Matrix4
{
    public float M11, M12, M13, M14;
    public float M21, M22, M23, M24;
    public float M31, M32, M33, M34;
    public float M41, M42, M43, M44;

    // 
    // --------------------------------------------------

    /// <summary>
    /// Retorna a matriz identidade (somente leitura).
    /// </summary>
    public static Matrix4 Identity => System.Numerics.Matrix4x4.Identity;

    // Contrutor
    // --------------------------------------------------

    public Matrix4(
        float m11, float m12, float m13, float m14,
        float m21, float m22, float m23, float m24,
        float m31, float m32, float m33, float m34,
        float m41, float m42, float m43, float m44
    )
    {
        M11 = m11; M12 = m12; M13 = m13; M14 = m14;
        M21 = m21; M22 = m22; M23 = m23; M24 = m24;
        M31 = m31; M32 = m32; M33 = m33; M34 = m34;
        M41 = m41; M42 = m42; M43 = m43; M44 = m44;
    }

    // Rotate
    // --------------------------------------------------

    public static Matrix4 Rotate(Vector3 axis, float angle)
    {
        axis.Normalize();

        return System.Numerics.Matrix4x4.CreateFromAxisAngle(axis, angle);
    }

    // Translate
    // --------------------------------------------------

    public static Matrix4 Translate(Vector3 position)
    {
        return System.Numerics.Matrix4x4.CreateTranslation(position);
    }

    public static Matrix4 Translate(Vector2 position, float z = 0.0f)
    {
        return System.Numerics.Matrix4x4.CreateTranslation(position.X, position.Y, z);
    }

    public static Matrix4 Translate(float x, float y, float z = 0.0f)
    {
        return System.Numerics.Matrix4x4.CreateTranslation(x, y, z);
    }

    // Scale
    // --------------------------------------------------

    /// <summary>
    /// Cria uma matriz de escala.
    /// </summary>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static Matrix4 Scale(Vector3 scale)
    {
        return System.Numerics.Matrix4x4.CreateScale(scale);
    }

    public static Matrix4 Scale(Vector2 scale, float z = 0.0f)
    {
        return System.Numerics.Matrix4x4.CreateScale(scale.X, scale.Y, z);
    }
    
    /// <summary>
    /// Cria uma matriz de escala.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Matrix4 Scale(float x, float y, float z = 0.0f)
    {
        return System.Numerics.Matrix4x4.CreateScale(x, y, z);
    }

    /// <summary>
    /// Cria uma matriz de escala.
    /// </summary>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static Matrix4 Scale(float scale)
    {
        return System.Numerics.Matrix4x4.CreateScale(scale);
    }

    // Orthographic
    // --------------------------------------------------

    /// <summary>
    /// Cria uma matriz de projeção ortogonal.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="bootom"></param>
    /// <param name="top"></param>
    /// <param name="zNear"></param>
    /// <param name="zFar"></param>
    /// <returns></returns>
    public static Matrix4 Orthographic(float left, float right, float bottom, float top, float zNear, float zFar)
    {
        return System.Numerics.Matrix4x4.CreateOrthographicOffCenter(left, right, bottom, top, zNear, zFar);
    }

    // Perspective
    // --------------------------------------------------

    /// <summary>
    /// Cria uma matriz de projeção em perspectiva.
    /// </summary>
    /// <param name="fov"></param>
    /// <param name="aspect"></param>
    /// <param name="zNear"></param>
    /// <param name="zFar"></param>
    /// <returns></returns>
    public static Matrix4 Perspective(float fov, float aspect, float zNear, float zFar)
    {
        return System.Numerics.Matrix4x4.CreatePerspectiveFieldOfView(fov, aspect, zNear, zFar);
    }

    // LookAt
    // --------------------------------------------------
    
    public static Matrix4 LookAt(Vector3 position, Vector3 target, Vector3 up)
    {
        return System.Numerics.Matrix4x4.CreateLookAt(position, target, up);
    }

    // Conversão Implicita
    // --------------------------------------------------

    public static implicit operator System.Numerics.Matrix4x4(Matrix4 matrix)
    {
        return new System.Numerics.Matrix4x4(
            matrix.M11, matrix.M12, matrix.M13, matrix.M14,
            matrix.M21, matrix.M22, matrix.M23, matrix.M24,
            matrix.M31, matrix.M32, matrix.M33, matrix.M34,
            matrix.M41, matrix.M42, matrix.M43, matrix.M44
        );
    }
    
    public static implicit operator Matrix4(System.Numerics.Matrix4x4 matrix)
    {
        return new Matrix4(
            matrix.M11, matrix.M12, matrix.M13, matrix.M14,
            matrix.M21, matrix.M22, matrix.M23, matrix.M24,
            matrix.M31, matrix.M32, matrix.M33, matrix.M34,
            matrix.M41, matrix.M42, matrix.M43, matrix.M44
        );
    }

    // Multiplicação
    // --------------------------------------------------

    public static Matrix4 operator *(Matrix4 a, Matrix4 b)
    {
        return new Matrix4(
            a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41,
            a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42,
            a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43,
            a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44,
            
            a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41,
            a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42,
            a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43,
            a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44,
            
            a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41,
            a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42,
            a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43,
            a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44,
            
            a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41,
            a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42,
            a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43,
            a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44
        );
    }
}
