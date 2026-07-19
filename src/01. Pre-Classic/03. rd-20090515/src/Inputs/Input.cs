#region License
/*

Feito por Morgana Stradivarius.

Inspirações: Os codigos do OpenTK e da Unity.

*/
#endregion

using System.Numerics;
using RubyDung.Utilities;
using Silk.NET.Input;
using Silk.NET.Windowing;

namespace RubyDung.Inputs;

/// <summary>
/// Interface com o sistema de entrada.
/// </summary>
public class Input
{
    /// <summary>
    /// Referência ao teclado ativo, obtido através do Silk.NET.
    /// Inicializado como null! (supressão de aviso de nullable) pois será definido no método Initialize.
    /// </summary>
    private static IKeyboard _keyboard = null!;

    /// <summary>
    /// Conjunto de teclas que estão pressionadas no frame atual.
    /// Usado para verificar o estado atual do teclado.
    /// </summary>
    private static HashSet<KeyCode> _keys = [];

    /// <summary>
    /// Conjunto de teclas que estavam pressionadas no frame anterior.
    /// Usado para detectar transições (KeyDown e KeyUp).
    /// </summary>
    private static HashSet<KeyCode> _keysPrevious = [];

    /// <summary>
    /// Conjunto de todas as teclas válidas (excluindo Key.Unknown) para iterar durante a atualização.
    /// Isso evita que teclas desconhecidas sejam processadas.
    /// </summary>
    private static HashSet<KeyCode> _keysValid = Enum.GetValues<KeyCode>()
        .Where(key => key != KeyCode.Unknown)
        .ToHashSet();

    public readonly static Dictionary<KeyCode, float> _lastPressTime = [];
    private const float _doublePressedTime = 0.3f;
        
    /// <summary>
    /// 
    /// </summary>
    private static IMouse _mouse = null!;

    /// <summary>
    /// 
    /// </summary>
    public static Vector2 MousePosition => _mouse.Position;

    /// <summary>
    /// 
    /// </summary>
    public static Vector2 MouseScrollDelta
    {
        get
        {
            Vector2 delta = _mouseScrollDelta;
            _mouseScrollDelta = Vector2.Zero;

            return delta;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static Vector2 _mouseScrollDelta;
    
    /// <summary>
    /// 
    /// </summary>
    public static CursorLockMode CursorLockMode
    {
        get
        {
            CursorMode inputMode = _mouse.Cursor.CursorMode;

            switch(inputMode)
            {
                case CursorMode.Normal:
                    return CursorLockMode.Normal;
                case CursorMode.Hidden:
                    return CursorLockMode.Hidden;
                case CursorMode.Disabled:
                    return CursorLockMode.Disabled;
                case CursorMode.Raw:
                    return CursorLockMode.Raw;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        set
        {
            CursorMode inputMode;

            switch(value)
            {
                case CursorLockMode.Normal:
                    inputMode = CursorMode.Normal;
                    break;
                case CursorLockMode.Hidden:
                    inputMode = CursorMode.Hidden;
                    break;
                case CursorLockMode.Disabled:
                    inputMode = CursorMode.Disabled;
                    break;
                case CursorLockMode.Raw:
                    inputMode = CursorMode.Raw;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _mouse.Cursor.CursorMode = inputMode;
        }
    }

    /// <summary>
    /// Inicializa o sistema de input, configurando o teclado a partir da janela fornecida.
    /// Deve ser chamado uma vez durante a inicialização do programa.
    /// </summary>
    /// <param name="window">A janela principal do Silk.NET que fornecerá o contexto de input.</param>
    public static void Initialize(IWindow window)
    {
        // Cria o contexto de input (teclado, mouse, etc.) a partir da janela
        IInputContext input = window.CreateInput();

        // Obtém o primeiro teclado disponível (assume que há pelo menos um)
        _keyboard = input.Keyboards[0];

        _mouse = input.Mice[0];

        _mouse.Scroll += (mouse, scrollWheel) =>
        {
            _mouseScrollDelta = new Vector2(scrollWheel.X, scrollWheel.Y);
        };
    }

    /// <summary>
    /// Deve ser chamado uma vez por frame para atualizar o estado do teclado.
    /// Armazena o estado anterior das teclas e captura o estado atual pressionado.
    /// </summary>
    public static void NewFrame()
    {
        // 1. Salva o estado atual como estado anterior
        _keysPrevious.Clear();

        foreach (KeyCode key in _keys)
        {
            _keysPrevious.Add(key);
        }

        // 2. Limpa o estado atual para recapturar as teclas pressionadas neste frame
        _keys.Clear();

        // 3. Verifica se o teclado foi inicializado
        if (_keyboard == null)
        {
            return;
        }

        // 4. Itera sobre todas as teclas válidas e verifica se cada uma está pressionada
        foreach (KeyCode key in _keysValid)
        {
            if (key == KeyCode.Unknown)
            {
                continue; // Segurança extra (embora já tenha sido filtrado em _keysValid)
            }

            // Teclas do mouse (valores >= 1000)
            if (key >= KeyCode.MouseLeft)
            {
                if (_mouse.IsButtonPressed((MouseButton)(int)key - 1000))
                {
                    _keys.Add(key);
                }
            }
            else
            {
                // Teclas do teclado
                if (_keyboard.IsKeyPressed((Key)key))
                {
                    _keys.Add(key);
                }
            }            
        }
    }

    /// <summary>
    /// Retorna verdadeiro enquanto o usuário mantiver pressionada a tecla identificada pelo parâmetro de enumeração KeyCode.
    /// </summary>
    /// <param name="key">A tecla a ser verificada.</param>
    /// <returns>True se a tecla está pressionada no frame atual.</returns>
    public static bool GetKey(KeyCode key)
    {
        return _keys.Contains(key);
    }

    /// <summary>
    /// Retorna verdadeiro durante o frame em que o usuário começa a pressionar a tecla identificada pelo parâmetro de enumeração KeyCode.
    /// </summary>
    /// <param name="key">A tecla a ser verificada.</param>
    /// <returns>True se a tecla foi pressionada neste exato frame (e não estava pressionada no frame anterior).</returns>
    public static bool GetKeyDown(KeyCode key)
    {
        // Tecla está pressionada agora E NÃO estava pressionada no frame anterior
        return _keys.Contains(key) && !_keysPrevious.Contains(key);
    }

    /// <summary>
    /// Retorna verdadeiro durante o frame em que o usuário solta a tecla identificada pelo parâmetro de enumeração KeyCode.
    /// </summary>
    /// <param name="key">A tecla a ser verificada.</param>
    /// <returns>True se a tecla foi solta neste exato frame (estava pressionada no frame anterior e não está mais).</returns>
    public static bool GetKeyUp(KeyCode key)
    {
        // Tecla NÃO está pressionada agora E estava pressionada no frame anterior
        return !_keys.Contains(key) && _keysPrevious.Contains(key);
    }

    /// <summary>
    /// Retorna verdadeiro durante o frame em que o usuário pressionar duas vezes a tecla identificada pelo parâmetro de enumeração KeyCode.
    /// </summary>
    /// <param name="key">A tecla a ser verificada.</param>
    /// <returns>True se a tecla foi pressionada duas vezes neste exato frame.</returns>
    public static bool GetKeyDouble(KeyCode key)
    {
        if (GetKeyDown(key))
        {
            float now = Time.ElapsedTime;

            if (_lastPressTime.TryGetValue(key, out float last) && (now - last) < _doublePressedTime)
            {
                return true;
            }

            _lastPressTime[key] = now;
        }

        return false;
    }
}
