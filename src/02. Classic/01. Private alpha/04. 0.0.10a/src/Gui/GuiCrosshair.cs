using Minecraft.Mathematics;
using Minecraft.Meshing;
using Minecraft.Rendering;
using Minecraft.Utilities;

namespace Minecraft.Gui;

public class GuiCrosshair
{
    private MeshQuad _mesh = null!;
    private MeshRender _meshRender = null!;

    public void Init()
    {
        _mesh = new MeshQuad();
        _meshRender = new MeshRender();
    }

    public void Update()
    {         
        _mesh.Clear();

        float wc = (Screen.Width / GuiManager.ScaleFactor) / 2.0f;
        float hc = (Screen.Height / GuiManager.ScaleFactor) / 2.0f;

        _mesh.AddQuad(
            [
                new Vector3(wc - 4, hc - 0, 0.0f),
                new Vector3(wc + 5, hc - 0, 0.0f),
                new Vector3(wc + 5, hc + 1, 0.0f),
                new Vector3(wc - 4, hc + 1, 0.0f)
            ]
        );

        _mesh.AddQuad(
            [
                new Vector3(wc - 0, hc - 4, 0.0f),
                new Vector3(wc + 1, hc - 4, 0.0f),
                new Vector3(wc + 1, hc + 5, 0.0f),
                new Vector3(wc - 0, hc + 5, 0.0f)
            ]
        );

        _meshRender.Mesh = _mesh;
    }

    public void Render(Shader shader)
    {
        Matrix4 model = Matrix4.Identity;

        // model *= Matrix4.Scale(0.5f);

        shader.SetUniform("uModel", model);

        _meshRender.Draw(shader);
    }
}
