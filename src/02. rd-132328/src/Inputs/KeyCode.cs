#region License
/*

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

*/
#endregion

namespace RubyDung.Inputs;

/// <summary>
/// Códigos de tecla retornados por Event.keyCode. Estes correspondem diretamente a uma tecla física no teclado.
/// </summary>
public enum KeyCode
{
    #region Keyboard

    /// <summary>
    /// Uma tecla desconhecida.
    /// </summary>
    Unknown = -1,

    /// <summary>
    /// A tecla de espaço.
    /// </summary>
    Space = 32,

    /// <summary>
    /// A tecla de apóstrofo.
    /// </summary>
    Apostrophe = 39 /* ' */,

    /// <summary>
    /// A tecla de vírgula.
    /// </summary>
    Comma = 44 /* , */,

    /// <summary>
    /// A tecla de menos (hífen).
    /// </summary>
    Minus = 45 /* - */,

    /// <summary>
    /// A tecla de ponto final.
    /// </summary>
    Period = 46 /* . */,

    /// <summary>
    /// A tecla de barra (slash).
    /// </summary>
    Slash = 47 /* / */,

    /// <summary>
    /// A tecla 0.
    /// </summary>
    Number0 = 48,

    /// <summary>
    /// A tecla 0; alias para <see cref="Number0"/>.
    /// </summary>
    D0 = Number0,

    /// <summary>
    /// A tecla 1.
    /// </summary>
    Number1 = 49,

    /// <summary>
    /// A tecla 2.
    /// </summary>
    Number2 = 50,

    /// <summary>
    /// A tecla 3.
    /// </summary>
    Number3 = 51,

    /// <summary>
    /// A tecla 4.
    /// </summary>
    Number4 = 52,

    /// <summary>
    /// A tecla 5.
    /// </summary>
    Number5 = 53,

    /// <summary>
    /// A tecla 6.
    /// </summary>
    Number6 = 54,

    /// <summary>
    /// A tecla 7.
    /// </summary>
    Number7 = 55,

    /// <summary>
    /// A tecla 8.
    /// </summary>
    Number8 = 56,

    /// <summary>
    /// A tecla 9.
    /// </summary>
    Number9 = 57,

    /// <summary>
    /// A tecla de ponto e vírgula.
    /// </summary>
    Semicolon = 59 /* ; */,

    /// <summary>
    /// A tecla de igual.
    /// </summary>
    Equal = 61 /* = */,

    /// <summary>
    /// A tecla A.
    /// </summary>
    A = 65,

    /// <summary>
    /// A tecla B.
    /// </summary>
    B = 66,

    /// <summary>
    /// A tecla C.
    /// </summary>
    C = 67,

    /// <summary>
    /// A tecla D.
    /// </summary>
    D = 68,

    /// <summary>
    /// A tecla E.
    /// </summary>
    E = 69,

    /// <summary>
    /// A tecla F.
    /// </summary>
    F = 70,

    /// <summary>
    /// A tecla G.
    /// </summary>
    G = 71,

    /// <summary>
    /// A tecla H.
    /// </summary>
    H = 72,

    /// <summary>
    /// A tecla I.
    /// </summary>
    I = 73,

    /// <summary>
    /// A tecla J.
    /// </summary>
    J = 74,

    /// <summary>
    /// A tecla K.
    /// </summary>
    K = 75,

    /// <summary>
    /// A tecla L.
    /// </summary>
    L = 76,

    /// <summary>
    /// A tecla M.
    /// </summary>
    M = 77,

    /// <summary>
    /// A tecla N.
    /// </summary>
    N = 78,

    /// <summary>
    /// A tecla O.
    /// </summary>
    O = 79,

    /// <summary>
    /// A tecla P.
    /// </summary>
    P = 80,

    /// <summary>
    /// A tecla Q.
    /// </summary>
    Q = 81,

    /// <summary>
    /// A tecla R.
    /// </summary>
    R = 82,

    /// <summary>
    /// A tecla S.
    /// </summary>
    S = 83,

    /// <summary>
    /// A tecla T.
    /// </summary>
    T = 84,

    /// <summary>
    /// A tecla U.
    /// </summary>
    U = 85,

    /// <summary>
    /// A tecla V.
    /// </summary>
    V = 86,

    /// <summary>
    /// A tecla W.
    /// </summary>
    W = 87,

    /// <summary>
    /// A tecla X.
    /// </summary>
    X = 88,

    /// <summary>
    /// A tecla Y.
    /// </summary>
    Y = 89,

    /// <summary>
    /// A tecla Z.
    /// </summary>
    Z = 90,

    /// <summary>
    /// A tecla de colchete esquerdo (abertura).
    /// </summary>
    LeftBracket = 91 /* [ */,

    /// <summary>
    /// A tecla de barra invertida (backslash).
    /// </summary>
    BackSlash = 92 /* \ */,

    /// <summary>
    /// A tecla de colchete direito (fechamento).
    /// </summary>
    RightBracket = 93 /* ] */,

    /// <summary>
    /// A tecla de acento grave (crase).
    /// </summary>
    GraveAccent = 96 /* ` */,

    /// <summary>
    /// Tecla de layout de teclado não americano 1.
    /// </summary>
    World1 = 161 /* non-US #1 */,

    /// <summary>
    /// Tecla de layout de teclado não americano 2.
    /// </summary>
    World2 = 162 /* non-US #2 */,

    /// <summary>
    /// A tecla Escape.
    /// </summary>
    Escape = 256,

    /// <summary>
    /// A tecla Enter.
    /// </summary>
    Enter = 257,

    /// <summary>
    /// A tecla Tab.
    /// </summary>
    Tab = 258,

    /// <summary>
    /// A tecla Backspace (retrocesso).
    /// </summary>
    Backspace = 259,

    /// <summary>
    /// A tecla Insert.
    /// </summary>
    Insert = 260,

    /// <summary>
    /// A tecla Delete.
    /// </summary>
    Delete = 261,

    /// <summary>
    /// A tecla de seta para a direita.
    /// </summary>
    RightArrow = 262,

    /// <summary>
    /// A tecla de seta para a esquerda.
    /// </summary>
    LeftArrow = 263,

    /// <summary>
    /// A tecla de seta para baixo.
    /// </summary>
    DownArrow = 264,

    /// <summary>
    /// A tecla de seta para cima.
    /// </summary>
    UpArrow = 265,

    /// <summary>
    /// A tecla Page Up (avançar página).
    /// </summary>
    PageUp = 266,

    /// <summary>
    /// A tecla Page Down (retroceder página).
    /// </summary>
    PageDown = 267,

    /// <summary>
    /// A tecla Home.
    /// </summary>
    Home = 268,

    /// <summary>
    /// A tecla End.
    /// </summary>
    End = 269,

    /// <summary>
    /// A tecla Caps Lock (fixa maiúsculas).
    /// </summary>
    CapsLock = 280,

    /// <summary>
    /// A tecla Scroll Lock.
    /// </summary>
    ScrollLock = 281,

    /// <summary>
    /// A tecla Num Lock.
    /// </summary>
    NumLock = 282,

    /// <summary>
    /// A tecla Print Screen.
    /// </summary>
    PrintScreen = 283,

    /// <summary>
    /// A tecla Pause.
    /// </summary>
    Pause = 284,

    /// <summary>
    /// A tecla F1.
    /// </summary>
    F1 = 290,

    /// <summary>
    /// A tecla F2.
    /// </summary>
    F2 = 291,

    /// <summary>
    /// A tecla F3.
    /// </summary>
    F3 = 292,

    /// <summary>
    /// A tecla F4.
    /// </summary>
    F4 = 293,

    /// <summary>
    /// A tecla F5.
    /// </summary>
    F5 = 294,

    /// <summary>
    /// A tecla F6.
    /// </summary>
    F6 = 295,

    /// <summary>
    /// A tecla F7.
    /// </summary>
    F7 = 296,

    /// <summary>
    /// A tecla F8.
    /// </summary>
    F8 = 297,

    /// <summary>
    /// A tecla F9.
    /// </summary>
    F9 = 298,

    /// <summary>
    /// A tecla F10.
    /// </summary>
    F10 = 299,

    /// <summary>
    /// A tecla F11.
    /// </summary>
    F11 = 300,

    /// <summary>
    /// A tecla F12.
    /// </summary>
    F12 = 301,

    /// <summary>
    /// A tecla F13.
    /// </summary>
    F13 = 302,

    /// <summary>
    /// A tecla F14.
    /// </summary>
    F14 = 303,

    /// <summary>
    /// A tecla F15.
    /// </summary>
    F15 = 304,

    /// <summary>
    /// A tecla F16.
    /// </summary>
    F16 = 305,

    /// <summary>
    /// A tecla F17.
    /// </summary>
    F17 = 306,

    /// <summary>
    /// A tecla F18.
    /// </summary>
    F18 = 307,

    /// <summary>
    /// A tecla F19.
    /// </summary>
    F19 = 308,

    /// <summary>
    /// A tecla F20.
    /// </summary>
    F20 = 309,

    /// <summary>
    /// A tecla F21.
    /// </summary>
    F21 = 310,

    /// <summary>
    /// A tecla F22.
    /// </summary>
    F22 = 311,

    /// <summary>
    /// A tecla F23.
    /// </summary>
    F23 = 312,

    /// <summary>
    /// A tecla F24.
    /// </summary>
    F24 = 313,

    /// <summary>
    /// A tecla F25.
    /// </summary>
    F25 = 314,

    /// <summary>
    /// A tecla 0 do teclado numérico.
    /// </summary>
    Keypad0 = 320,

    /// <summary>
    /// A tecla 1 do teclado numérico.
    /// </summary>
    Keypad1 = 321,

    /// <summary>
    /// A tecla 2 do teclado numérico.
    /// </summary>
    Keypad2 = 322,

    /// <summary>
    /// A tecla 3 do teclado numérico.
    /// </summary>
    Keypad3 = 323,

    /// <summary>
    /// A tecla 4 do teclado numérico.
    /// </summary>
    Keypad4 = 324,

    /// <summary>
    /// A tecla 5 do teclado numérico.
    /// </summary>
    Keypad5 = 325,

    /// <summary>
    /// A tecla 6 do teclado numérico.
    /// </summary>
    Keypad6 = 326,

    /// <summary>
    /// A tecla 7 do teclado numérico.
    /// </summary>
    Keypad7 = 327,

    /// <summary>
    /// A tecla 8 do teclado numérico.
    /// </summary>
    Keypad8 = 328,

    /// <summary>
    /// A tecla 9 do teclado numérico.
    /// </summary>
    Keypad9 = 329,

    /// <summary>
    /// A tecla de ponto decimal do teclado numérico.
    /// </summary>
    KeypadDecimal = 330,

    /// <summary>
    /// A tecla de divisão do teclado numérico.
    /// </summary>
    KeypadDivide = 331,

    /// <summary>
    /// A tecla de multiplicação do teclado numérico.
    /// </summary>
    KeypadMultiply = 332,

    /// <summary>
    /// A tecla de subtração do teclado numérico.
    /// </summary>
    KeypadSubtract = 333,

    /// <summary>
    /// A tecla de adição do teclado numérico.
    /// </summary>
    KeypadAdd = 334,

    /// <summary>
    /// A tecla Enter do teclado numérico.
    /// </summary>
    KeypadEnter = 335,

    /// <summary>
    /// A tecla de igual do teclado numérico.
    /// </summary>
    KeypadEqual = 336,

    /// <summary>
    /// A tecla Shift esquerda.
    /// </summary>
    ShiftLeft = 340,

    /// <summary>
    /// A tecla Control esquerda.
    /// </summary>
    ControlLeft = 341,

    /// <summary>
    /// A tecla Alt esquerda.
    /// </summary>
    AltLeft = 342,

    /// <summary>
    /// A tecla Super esquerda (Windows/Command).
    /// </summary>
    SuperLeft = 343,

    /// <summary>
    /// A tecla Shift direita.
    /// </summary>
    ShiftRight = 344,

    /// <summary>
    /// A tecla Control direita.
    /// </summary>
    ControlRight = 345,

    /// <summary>
    /// A tecla Alt direita.
    /// </summary>
    AltRight = 346,

    /// <summary>
    /// A tecla Super direita.
    /// </summary>
    SuperRight = 347,

    /// <summary>
    /// A tecla Menu.
    /// </summary>
    Menu = 348,

    #endregion

    #region Mouse

    /// <summary>
    /// The left mouse button.
    /// </summary>
    MouseLeft = 1000,

    /// <summary>
    /// The right mouse button.
    /// </summary>
    MouseRight = 1001,

    /// <summary>
    /// The middle mouse button.
    /// </summary>
    MouseMiddle = 1002,

    /// <summary>
    /// The fourth mouse button.
    /// </summary>
    Mouse4 = 1003,

    /// <summary>
    /// The fifth mouse button.
    /// </summary>
    Mouse5 = 1004,

    /// <summary>
    /// The sixth mouse button.
    /// </summary>
    Mouse6 = 1005,

    /// <summary>
    /// The seventh mouse button.
    /// </summary>
    Mouse7 = 1006,

    /// <summary>
    /// The eighth mouse button.
    /// </summary>
    Mouse8 = 1007,

    /// <summary>
    /// The ninth mouse button.
    /// </summary>
    Mouse9 = 1008,

    /// <summary>
    /// The tenth mouse button.
    /// </summary>
    Mouse10 = 1009,

    /// <summary>
    /// The eleventh mouse button.
    /// </summary>
    Mouse11 = 1010,

    /// <summary>
    /// The twelth mouse button.
    /// </summary>
    Mouse12 = 1011

    #endregion
}
