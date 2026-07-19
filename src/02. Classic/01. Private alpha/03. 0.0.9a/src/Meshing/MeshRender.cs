using Minecraft.Core;
using Silk.NET.OpenGL;
using Minecraft.Rendering;
using Shader = Minecraft.Rendering.Shader;
using Minecraft.Mathematics;

namespace Minecraft.Meshing;

public class MeshRender : IDisposable
{
    private GL _gl = Game.GL;

    private Mesh _mesh = null!;
    public Mesh Mesh
    {
        get => _mesh;
        set
        {
            _mesh = value;

            LoadMesh(
                _mesh.Positions,
                _mesh.Colors,
                _mesh.TexCoords,
                _mesh.Indices
            );
        }
    }
    
    private float[] _vertices = [];
    private uint[] _indices = [];

    private uint _vertexArrayObject;
    private uint _vertexBufferObject;
    private uint _elementBufferObject;

    private const int _sizePos = 3;
    private const int _sizeColor = 4;
    private const int _sizeTex = 2;

    private uint _vertexCount;
    private uint _indexCount;

    private uint _vertexSrtide;
    private uint _vertexPointer;

    private bool _useColor = false;
    private bool _useTexture = false;

    public MeshRender()
    {
        
    }

    public MeshRender(Mesh mesh)
    {
        LoadMesh(
            mesh.Positions,
            mesh.Colors,
            mesh.TexCoords,
            mesh.Indices
        );
    }

    public MeshRender(Vector3[] positions, Color[] colors, TexCoord[] texCoords, uint[] indices)
    {
        LoadMesh(
            positions, 
            colors, 
            texCoords, 
            indices
        );
    }

    public void Draw(Shader shader)
    {
        shader.SetUniform("useColor", _useColor);
        shader.SetUniform("useTexture", _useTexture);

        _gl.BindVertexArray(_vertexArrayObject);

        DrawElementsFill(shader);
        DrawElementsLine(shader);

        _gl.BindVertexArray(0);
    }

    private void DrawElementsFill(Shader shader)
    {
        if (Game.ShadingMode == ShadingMode.Shaded ||
            Game.ShadingMode == ShadingMode.Shaded_Wireframe)
        {
            shader.SetUniform("useWireframe", false);
            _gl.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Fill);   

            DrawElements();         
        }
    }

    private void DrawElementsLine(Shader shader)
    {
        if (Game.ShadingMode == ShadingMode.Wireframe ||
            Game.ShadingMode == ShadingMode.Shaded_Wireframe)
        {
            shader.SetUniform("useWireframe", true);

            // --- Desativa cor e textura para o modo linha ---
            if (_useColor) _gl.DisableVertexAttribArray(1);
            if (_useTexture) _gl.DisableVertexAttribArray(2);

            _gl.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Line);

            // Aplica um deslocamento de polígono para evitar z-fighting
            _gl.Enable(EnableCap.PolygonOffsetLine);
            _gl.PolygonOffset(-1.0f, -1.0f);  

            DrawElements();
            
            // --- Reativa os atributos para o próximo ciclo de renderização ---
            if (_useColor) _gl.EnableVertexAttribArray(1);
            if (_useTexture) _gl.EnableVertexAttribArray(2);
            
            // Limpa estado para não afetar outras chamadas
            _gl.Disable(EnableCap.PolygonOffsetLine);
        }
    }

    private void DrawElements()
    {
        unsafe
        {
            // renderiza o triângulo
            _gl.DrawElements(PrimitiveType.Triangles, _indexCount, DrawElementsType.UnsignedInt, (void*)0);
        }
    }

    private void LoadMesh(
        Vector3[] positions, 
        Color[] colors, 
        TexCoord[] texCoords, 
        uint[] indices
    )
    {
        _useColor = colors.Length > 0 && !_mesh._noColor;
        _useTexture = texCoords.Length > 0;

        //
        // --------------------------------------------------

        _vertexSrtide = _sizePos;

        if (_useColor)
        {
            _vertexSrtide += _sizeColor;
        }
        if (_useTexture)
        {
            _vertexSrtide += _sizeTex;
        }

        //
        // --------------------------------------------------

        _vertexPointer = 0;

        _vertexCount = (uint)positions.Length;
        _vertices = new float[_vertexCount * _vertexSrtide];

        for (int i = 0; i < _vertexCount; i++)
        {
            _vertices[_vertexPointer++] = positions[i].X;
            _vertices[_vertexPointer++] = positions[i].Y;
            _vertices[_vertexPointer++] = positions[i].Z;

            if (_useColor)
            {
                _vertices[_vertexPointer++] = colors[i].R;
                _vertices[_vertexPointer++] = colors[i].G;
                _vertices[_vertexPointer++] = colors[i].B;
                _vertices[_vertexPointer++] = colors[i].A;
            }
            if (_useTexture)
            {
                _vertices[_vertexPointer++] = texCoords[i].U;
                _vertices[_vertexPointer++] = texCoords[i].V;
            }
        }

        //
        // --------------------------------------------------

        _indexCount = (uint)indices.Length;
        _indices = new uint[_indexCount];

        for (int i = 0; i < _indexCount; i++)
        {
            _indices[i] = indices[i];
        }

        //
        // --------------------------------------------------

        SetupMesh();
    }

    private void SetupMesh()
    {
        _gl.GenVertexArrays(1, out _vertexArrayObject);

        // primeiro vincule o Vertex Array Object, depois vincule e configure o(s) buffer(s) de vértices e, em seguida, configure o(s) atributo(s) de vértice.
        _gl.BindVertexArray(_vertexArrayObject);

        _vertexBufferObject = BufferObject(BufferTargetARB.ArrayBuffer, _vertices);
        _elementBufferObject = BufferObject(BufferTargetARB.ElementArrayBuffer, _indices);

        uint stride = _vertexSrtide;
        int pointer = 0;

        // position attribute
        uint indexPos = 0;
        VertexAttributePointer(indexPos, _sizePos, stride, pointer);
        pointer += _sizePos;

        // color attribute
        if (_useColor)
        {
            uint indexColor = 1;
            VertexAttributePointer(indexColor, _sizeColor, stride, pointer);
            pointer += _sizeColor;
        }

        // texture coords attribute
        if (_useTexture)
        {
            uint indexTex = 2;
            VertexAttributePointer(indexTex, _sizeTex, stride, pointer);
        }

        // observe que isso é permitido; a chamada para glVertexAttribPointer registrou o VBO como o objeto de buffer de vértices vinculado ao atributo de vértice, portanto, podemos desvinculá-lo com segurança logo em seguida
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);

        // lembre-se: NÃO desvincule o EBO enquanto um VAO estiver ativo, pois o objeto de buffer de elementos vinculado ESTÁ armazenado no VAO; mantenha o EBO vinculado.
        // _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);

        // Você pode desvincular o VAO posteriormente para que outras chamadas de VAO não modifiquem acidentalmente este VAO, mas isso raramente acontece. Modificar outros VAOs exige uma chamada para glBindVertexArray de qualquer forma, então geralmente não desvinculamos VAOs (nem VBOs) quando não é diretamente necessário.
        _gl.BindVertexArray(0);
    }

    private uint BufferObject<T>(BufferTargetARB target, Span<T> data) where T : unmanaged
    {
        uint buffer;
        _gl.GenBuffers(1, out buffer);

        _gl.BindBuffer(target, buffer);
        unsafe
        {
            fixed (void* buf = data)
            {
                _gl.BufferData(target, (uint)(data.Length * sizeof(T)), buf, BufferUsageARB.StaticDraw);
            }
        }

        return buffer;
    }

    private void VertexAttributePointer(uint index, int size, uint stride, int pointer)
    {
        unsafe
        {
            _gl.VertexAttribPointer(index, size, VertexAttribPointerType.Float, false, stride * sizeof(float), (void*)(pointer * sizeof(float)));
        }
        _gl.EnableVertexAttribArray(index);
    }

    public void Dispose()
    {
        _gl.DeleteVertexArrays(1, ref _vertexArrayObject);
        _gl.DeleteBuffers(1, ref _vertexBufferObject);
        _gl.DeleteBuffers(1, ref _elementBufferObject);
    }
}
