using Minecraft.Inputs;
using Minecraft.Utilities;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Minecraft.Mathematics;
using Minecraft.Rendering;
using Shader = Minecraft.Rendering.Shader;
using Texture = Minecraft.Rendering.Texture;
using Minecraft.Meshing;
using Minecraft.Levels;
using Minecraft.Gui;

namespace Minecraft.Core;

public class Game
{
    private IWindow _window = null!;

    private GL _gl = null!;
    public static GL GL = null!;

    private Shader _shader = null!;
    public static ShadingMode ShadingMode = ShadingMode.Shaded;

    private Texture _texture1 = null!;

    private Level _level = null!;
    private LevelRenderer _levelRenderer = null!;

    private Camera _camera = null!;
    private BlockInteraction _blockInteraction = null!;

    private GuiManager _guiManager = null!;

    private bool _load = false;
    private bool _pause = false;

    public Game()
    {
        WindowOptions options = WindowOptions.Default;
        options.Size = new Vector2D<int>(1280, 720);
        options.Title = "Minecraft Clone - by Morgana Stradivarius";
        options.IsVisible = false;

        _window = Window.Create(options);

        _window.Load += () =>
        {
            _window.Center();
            _window.IsVisible = true;

            Screen.Initialize(_window);
            Input.Initialize(_window);

            _gl = _window.CreateOpenGL();
            GL = _gl;

            OnLoad();
        };

        _window.Resize += newSize =>
        {
            OnResize(newSize);
        };

        _window.Update += deltaTime =>
        {            
            Time.Update(deltaTime);
            Input.NewFrame();

            OnUpdate(deltaTime);
        };

        _window.Render += deltaTime =>
        {
            OnRender(deltaTime);
        };

        _window.Closing += () =>
        {
            OnClosing();
        };

        try
        {
            _window.Run();
        }
        catch (Exception ex)
        {
            Debug.LogError(
                "Falha ao criar a janela Silk.NET" + "\n" +
                ex + "\n" + 
                " -- --------------------------------------------------- -- "
            );
        }
    }

    private void OnLoad()
    {
        _gl.ClearColor(Color.LightSkyBlue);

        _gl.Enable(EnableCap.DepthTest);
        _gl.Enable(EnableCap.CullFace);

        _gl.Enable(EnableCap.Blend);
        _gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

        // construir e compilar nosso programa de shader
        // ------------------------------------
        _shader = new Shader("base"); // você pode nomear seus arquivos de shader como quiser

        // carregar e criar uma textura
        // ----------------------------

        // texture 1
        // ---------
        _texture1 = new Texture("terrain");

        _level = new Level(256, 256, 64);
        _levelRenderer = new LevelRenderer(_level);
        _levelRenderer.Init();

        Input.CursorLockMode = CursorLockMode.Raw;
        _camera = new Camera(_level);
        _blockInteraction = new BlockInteraction(_level, _camera);

        _guiManager = new GuiManager();
        _guiManager.Init(_level);
    }

    private void OnResize(Vector2D<int> newSize)
    {
        // certifique-se de que a viewport corresponda às novas dimensões da janela; observe que largura e a altura será significativamente maior do que a especificada em telas retina.
        _gl.Viewport(0, 0, (uint)newSize.X, (uint)newSize.Y);
    }

    private void OnUpdate(double deltaTime)
    {
        if (_load)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // _window.Close();
                _pause = !_pause;

                if (_pause)
                {
                    Input.CursorLockMode = CursorLockMode.Normal;
                }
                else
                {
                    _camera.ResetMouse();
                    Input.CursorLockMode = CursorLockMode.Raw;
                }
            }

            if (!_pause)
            {
                if (Input.GetKeyDown(KeyCode.Enter))
                {
                    _level.Save();
                }

                DebugHotkeys.Update();

                if (!DebugHotkeys.Pressd)
                {
                    _camera.ProcessKeyboad();
                    _camera.ProcessMouseMovement();
                    // _camera.ProcessMouseScroll();

                    _blockInteraction.Update();

                    _level.Tick();
                }
            }

            _guiManager.Update(_pause);
        }
    }

    private void OnRender(double deltaTime)
    {
        _gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        _shader.Use();

        Matrix4 model = Matrix4.Identity;
        _shader.SetUniform("uModel", model);

        Matrix4 view = _camera.GetViewMatrix();
        _shader.SetUniform("uView", view);

        Matrix4 projection = _camera.GetProjectionMatrix();
        _shader.SetUniform("uProjection", projection);

        // vincular texturas às unidades de textura correspondentes
        _texture1.Bind();

        _levelRenderer.Render(_shader);

        _load = true;

        _blockInteraction.Render(_shader);

        _guiManager.Render();
    }

    private void OnClosing()
    {
        // opcional: desalocar todos os recursos assim que não forem mais necessários:
        // ---------------------------------------------------------------------------
        
        _shader.Dispose();

        _level.Save();
    }
}
