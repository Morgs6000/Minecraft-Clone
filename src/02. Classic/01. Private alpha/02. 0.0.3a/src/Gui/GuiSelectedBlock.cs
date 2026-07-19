using Minecraft.Inputs;
using Minecraft.Levels;
using Minecraft.Levels.Blocks;
using Minecraft.Mathematics;
using Minecraft.Meshing;
using Minecraft.Rendering;
using Minecraft.Utilities;

namespace Minecraft.Gui;

public class GuiSelectedBlock
{
    public static int PaintTexture = Block.Rock.ID;

    private Level _level = null!;

    private MeshQuad _mesh = null!;
    
    private MeshRender _meshRender = null!;

    public void Init(Level level)
    {
        _level = level;

        _mesh = new MeshQuad();
        _meshRender = new MeshRender();

        Selected();
    }

    public void Update()
    {
        int previous = PaintTexture;

        if (Input.GetKeyDown(KeyCode.Number1))
        {
            PaintTexture = Block.Rock.ID;
        }
        if (Input.GetKeyDown(KeyCode.Number2))
        {
            PaintTexture = Block.Dirt.ID;
        }
        if (Input.GetKeyDown(KeyCode.Number3))
        {
            PaintTexture = Block.StoneBrick.ID;
        }
        if (Input.GetKeyDown(KeyCode.Number4))
        {
            PaintTexture = Block.Wood.ID;
        }
        if (Input.GetKeyDown(KeyCode.Number6))
        {
            PaintTexture = Block.Bush.ID;
        }

        if (previous != PaintTexture)
        {
            Selected();
        }
    }

    public void Render(Shader shader)
    {
        Matrix4 model = Matrix4.Identity;

        // Centraliza o bloco no próprio eixo antes de rodar (se necessário)
        model *= Matrix4.Translate(1.5f, -0.5f, -0.5f);

        // Converte Z‑up → Y‑up (rotação de -90° em torno de X)
        model *= Matrix4.Rotate(Vector3.PositiveX, Mathf.Radians(-90.0f));

        // Visualização isométrica (opcional)
        model *= Matrix4.Rotate(Vector3.PositiveY, Mathf.Radians(-45.0f));
        model *= Matrix4.Rotate(Vector3.PositiveX, Mathf.Radians(30.0f));
        
        // Escala (Tamanho do ícone na UI)
        model *= Matrix4.Scale(32.0f, 32.0f);

        float w = (float)Screen.Width / GuiManager.ScaleFactor;
        float h = (float)Screen.Height / GuiManager.ScaleFactor;

        // Posiciona na tela (Coordenadas de Pixel, já que a projeção é ortográfica)
        model *= Matrix4.Translate(w - 32.0f, h - 32.0f);

        shader.SetUniform("uModel", model);

        _meshRender.Draw(shader);
    }

    private void Selected()
    {
        _mesh = new MeshQuad();
        _mesh.Clear();

        Block.Blocks[PaintTexture].Init(_mesh, _level, new Vector3Int(-2, 0, 0));

        _meshRender.Mesh = _mesh;
    }
}
