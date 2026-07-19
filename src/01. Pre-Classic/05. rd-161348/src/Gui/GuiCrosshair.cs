using RubyDung.Mathematics;
using RubyDung.Meshing;
using RubyDung.Rendering;
using RubyDung.Utilities;

namespace RubyDung.Gui;

public class GuiCrosshair
{
    private MeshRender _meshRender = null!;

    public void Init()
    {
        MeshQuad mesh = new MeshQuad();
        mesh.Clear();

        float wc = (Screen.Width / GuiManager.ScaleFactor) / 2.0f;
        float hc = (Screen.Height / GuiManager.ScaleFactor) / 2.0f;

        mesh.AddQuad(
            [
                new Vector3(wc - 4, hc - 0, 0.0f),
                new Vector3(wc + 5, hc - 0, 0.0f),
                new Vector3(wc + 5, hc + 1, 0.0f),
                new Vector3(wc - 4, hc + 1, 0.0f)
            ]
        );

        mesh.AddQuad(
            [
                new Vector3(wc - 0, hc - 4, 0.0f),
                new Vector3(wc + 1, hc - 4, 0.0f),
                new Vector3(wc + 1, hc + 5, 0.0f),
                new Vector3(wc - 0, hc + 5, 0.0f)
            ]
        );

        _meshRender = new MeshRender();
        _meshRender.Mesh = mesh;
    }

    public void Render(Shader shader)
    {
        Matrix4 model = Matrix4.Identity;

        // model *= Matrix4.Scale(0.5f);

        shader.SetUniform("uModel", model);

        _meshRender.Draw(shader);
    }
}
