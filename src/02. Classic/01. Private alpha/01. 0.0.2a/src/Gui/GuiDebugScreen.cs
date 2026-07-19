using Minecraft.Levels;
using Minecraft.Mathematics;
using Minecraft.Meshing;
using Minecraft.Rendering;
using Minecraft.Utilities;

namespace Minecraft.Gui;

public class GuiDebugScreen
{
    private MeshQuad _mesh = null!;

    private GuiFont _guiFont = null!;

    private float _timeAccumulator = 0.0f;
    private int _frames = 0;
    private string _fpsString = "";

    public void Init()
    {
        _mesh = new MeshQuad();
        _guiFont = new GuiFont(_mesh);
    }

    public void Update()
    {
        _frames++;
        _timeAccumulator += Time.DeltaTime;

        if (_timeAccumulator >= 1.0f)
        {
            _fpsString = $"{_frames} fps, {Chunk.Updates} chunk updates";

            _frames = 0;
            _timeAccumulator %= 1.0f;
            Chunk.Updates = 0;
        }

        float h = ((float)Screen.Height / GuiManager.ScaleFactor) - 8;

        _mesh.Clear();

        _guiFont.DrawShadow("0.0.2a", new Vector2(2, h - 2), 16777215);
        _guiFont.DrawShadow(_fpsString, new Vector2(2, h - 12), 16777215);        
    }

    public void Render(Shader shader)
    {
        _guiFont.Render(shader);
    }
}
