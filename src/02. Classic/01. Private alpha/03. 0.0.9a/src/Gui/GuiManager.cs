using Minecraft.Core;
using Minecraft.Levels;
using Minecraft.Mathematics;
using Minecraft.Rendering;
using Minecraft.Utilities;
using Silk.NET.OpenGL;
using Shader = Minecraft.Rendering.Shader;

namespace Minecraft.Gui;

public class GuiManager
{
    private GL _gl = Game.GL;

    private Level _level = null!;

    public static float ScaleFactor = 2.0f;

    private Shader _shader = null!;

    private GuiCrosshair _guiCrosshair = null!;
    private GuiSelectedBlock _guiSelectedBlock = null!;
    private GuiDebugScreen _guiDebugScreen = null!;

    public void Init(Level level)
    {
        _level = level;

        _shader = new Shader("gui");

        _guiCrosshair = new GuiCrosshair();
        _guiCrosshair.Init();

        _guiSelectedBlock = new GuiSelectedBlock();
        _guiSelectedBlock.Init(level);

        _guiDebugScreen = new GuiDebugScreen();
        _guiDebugScreen.Init();
    }

    public void Update()
    {
        _guiSelectedBlock.Update();
        _guiDebugScreen.Update();
    }

    public void Render()
    {
        _shader.Use();

        Matrix4 model = Matrix4.Identity;
        _shader.SetUniform("uModel", model);

        Matrix4 projection = GetProjectionMatrix();
        _shader.SetUniform("uProjection", projection);        

        _gl.Disable(EnableCap.DepthTest);

        _guiCrosshair.Render(_shader);
        _guiSelectedBlock.Render(_shader);
        _guiDebugScreen.Render(_shader);

        _gl.Enable(EnableCap.DepthTest);
    }

    private Matrix4 GetProjectionMatrix()
    {
        return Matrix4.Orthographic(
            left:   0.0f, 
            right:  (float)Screen.Width / ScaleFactor, 
            bottom: 0.0f, 
            top:    (float)Screen.Height / ScaleFactor, 
            zNear:  0.3f, 
            zFar:   1000.0f
        );
    }
}
