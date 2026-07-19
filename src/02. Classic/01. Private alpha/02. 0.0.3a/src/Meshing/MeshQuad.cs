using Minecraft.Mathematics;

namespace Minecraft.Meshing;

public class MeshQuad : Mesh
{
    public void AddQuad(Vector3[] positions)
    {
        for (int i = 0; i < 4; i++)
        {
            AddVertex(positions[i]);
        }
    }

    public void AddQuad(Vector3[] positions, Color[] colors)
    {
        for (int i = 0; i < 4; i++)
        {
            SetColor(colors[i]);
            AddVertex(positions[i]);
        }
    }

    public void AddQuad(Vector3[] positions, TexCoord[] texCoords)
    {
        for (int i = 0; i < 4; i++)
        {
            SetTexCoord(texCoords[i]);
            AddVertex(positions[i]);
        }
    }

    public void AddQuad(Vector3[] positions, Color[] colors, TexCoord[] texCoords)
    {
        for (int i = 0; i < 4; i++)
        {
            SetColor(colors[i]);
            SetTexCoord(texCoords[i]);
            AddVertex(positions[i]);
        }
    }

    // Add Vertex
    // --------------------------------------------------

    private void AddVertex(Vector3 position)
    {
        if (_vertexCount >= Positions.Length)
        {
            int newSize = Positions.Length == 0 ? 4 : Positions.Length * 2;
            Array.Resize(ref Positions, newSize);

            if (_hasColor)
            {
                Array.Resize(ref Colors, newSize);
            }
            if (_hasTexture)
            {
                Array.Resize(ref TexCoords, newSize);
            }
        }

        // --------------------------------------------------

        Positions[_vertexCount] = position;

        if (_hasColor)
        {
            Colors[_vertexCount] = _color;
        }
        if (_hasTexture)
        {
            TexCoords[_vertexCount] = _texCoord;
        }

        _vertexCount++;

        if (_vertexCount % 4 == 0)
        {
            SetIndices();
        }
    }

    // Set Colors
    // --------------------------------------------------

    public void SetColor(Color color)
    {
        _hasColor = true;
        _color = color;
    }

    // Set TexCoords
    // --------------------------------------------------

    public void SetTexCoord(TexCoord texCoord)
    {
        _hasTexture = true;
        _texCoord = texCoord;
    }

    // SetIndices
    // --------------------------------------------------

    private void SetIndices()
    {
        if (_indexCount + 6 > Indices.Length)
        {
            int newSize = Indices.Length == 0 ? 6 : Indices.Length * 2;
            Array.Resize(ref Indices, newSize);
        }

        // --------------------------------------------------

        uint index = _vertexCount - 4;

        // primeiro triangulo
        Indices[_indexCount++] = 0 + index;
        Indices[_indexCount++] = 1 + index;
        Indices[_indexCount++] = 2 + index;

        // segundo triangulo
        Indices[_indexCount++] = 0 + index;
        Indices[_indexCount++] = 2 + index;
        Indices[_indexCount++] = 3 + index;
    }
}
