using Minecraft.Mathematics;

namespace Minecraft.Meshing;

/// <summary>
/// Uma classe que permite criar ou modificar malhas a partir de scripts.
/// </summary>
public class Mesh
{
    /// <summary>
    /// Retorna uma cópia das posições dos vértices ou atribui uma nova matriz de posições dos vértices.
    /// </summary>
    public Vector3[] Positions = [];

    /// <summary>
    /// Cores dos vértices da malha.
    /// </summary>
    public Color[] Colors = [];

    /// <summary>
    /// As coordenadas de textura base da malha.
    /// </summary>
    public TexCoord[] TexCoords = [];

    /// <summary>
    /// Uma matriz contendo todos os triângulos da malha.
    /// </summary>
    public uint[] Indices = [];

    public bool _noColor = false;

    //
    // --------------------------------------------------

    protected uint _vertexCount;
    protected uint _indexCount;

    protected bool _useColor = false;
    protected Color _color;

    protected bool _useTexture = false;
    protected TexCoord _texCoord;

    // clear
    // --------------------------------------------------

    public void Clear()
    {
        Positions = Array.Empty<Vector3>();
        Colors = Array.Empty<Color>();
        TexCoords = Array.Empty<TexCoord>();
        Indices = Array.Empty<uint>();

        _vertexCount = 0;
        _indexCount = 0;

        _useColor = false;
        _useTexture = false;
        _noColor = false;
    }

    public void NoColor()
    {
        _noColor = true;
    }
}
