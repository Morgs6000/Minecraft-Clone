using RubyDung.Inputs;
using RubyDung.Rendering;

namespace RubyDung.Core;

public static class DebugHotkeys
{   
    public static bool Pressd = false;

    public static void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            OnBeginPress();
        }
        if (Input.GetKey(KeyCode.F3))
        {
            OnPress();
        }
        if (Input.GetKeyUp(KeyCode.F3))
        {
            OnEndPress();
        }
    }

    private static void OnBeginPress()
    {
        Pressd = true;
    }

    private static void OnPress()
    {
        ShadedModeSwitcher();
    }

    private static void OnEndPress()
    {
        Pressd = false;
    }

    private static void ShadedModeSwitcher()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            switch (Game.ShadingMode)
            {
                case ShadingMode.Shaded:
                    Game.ShadingMode = ShadingMode.Shaded_Wireframe;
                    break;
                case ShadingMode.Shaded_Wireframe:
                    Game.ShadingMode = ShadingMode.Wireframe;
                    break;
                case ShadingMode.Wireframe:
                    Game.ShadingMode = ShadingMode.Shaded;
                    break;
            }
        }
    }
}
