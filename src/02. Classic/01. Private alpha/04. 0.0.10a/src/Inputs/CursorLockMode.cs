#region License
/*

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

*/
#endregion

namespace Minecraft.Inputs;

/// <summary>
/// 
/// </summary>
public enum CursorLockMode
{
    /// <summary>
    /// O cursor está visível e não apresenta restrições de mobilidade.
    /// </summary>
    Normal,

    /// <summary>
    /// O cursor é invisível e não possui restrições de mobilidade.
    /// </summary>
    Hidden,

    /// <summary>
    /// O cursor é invisível e está restrito ao centro da tela.
    /// </summary>
    /// <remarks>
    /// Suportado apenas pelo GLFW; gera um erro no SDL se utilizado.
    /// </remarks>
    Disabled,

    /// <summary>
    /// O cursor é invisível e fica restrito ao centro da tela. O movimento do mouse não é escalonado.
    /// </summary>
    Raw
}
