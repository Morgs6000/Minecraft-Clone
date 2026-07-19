using RubyDung.Core;
using RubyDung.Mathematics;
using RubyDung.Utilities;
using Silk.NET.OpenGL;

namespace RubyDung.Rendering;

public class Shader : IDisposable
{
    private GL _gl = Game.GL;

    private uint _program;

    // O construtor gera o shader em tempo de execução.
    // ------------------------------------------------------------------------
    public Shader(string path)
    {
        string vertexPath = $"res/Shaders/{path}/vertex.glsl";
        string fragmentPath = $"res/Shaders/{path}/fragment.glsl";

        LoadShader(vertexPath, fragmentPath);
    }

    public Shader(string vertexPath, string fragmentPath)
    {
        LoadShader(vertexPath, fragmentPath);
    }

    // ativa o shader
    // ------------------------------------------------------------------------
    public void Use()
    {
        _gl.UseProgram(_program);
    }

    // funções utilitárias de uniformes
    // ------------------------------------------------------------------------

    // bool
    public void SetUniform(string name, bool value)
    {
        int location = _gl.GetUniformLocation(_program, name);
        _gl.Uniform1(location, value ? 1 : 0);
    }

    // int
    public void SetUniform(string name, int value)
    {
        int location = _gl.GetUniformLocation(_program, name);
        _gl.Uniform1(location, value);
    }

    // Vector2Int
    public void SetUniform(string name, int v0, int v1)
    {
        int location = _gl.GetUniformLocation(_program, name);
        _gl.Uniform2(location, v0, v1);
    }

    // Vector2Int
    public void SetUniform(string name, Vector2Int vector)
    {
        int location = _gl.GetUniformLocation(_program, name);
        _gl.Uniform2(location, vector);
    }

    // Vector3Int
    public void SetUniform(string name, int v0, int v1, int v2)
    {
        int location = _gl.GetUniformLocation(_program, name);
        _gl.Uniform3(location, v0, v1, v2);
    }

    // Vector3Int
    public void SetUniform(string name, Vector3Int vector)
    {
        int location = _gl.GetUniformLocation(_program, name);
        _gl.Uniform3(location, vector);
    }

    // Vector4Int
    public void SetUniform(string name, int v0, int v1, int v2, int v3)
    {
        int location = _gl.GetUniformLocation(_program, name);
        _gl.Uniform4(location, v0, v1, v2, v3);
    }

    // Vector4Int
    public void SetUniform(string name, Vector4Int vector)
    {
        int location = _gl.GetUniformLocation(_program, name);
        _gl.Uniform4(location, vector);
    }

    // float
    public void SetUniform(string name, float value)
    {
        int location = _gl.GetUniformLocation(_program, name);
        _gl.Uniform1(location, value);
    }

    // Vector2
    public void SetUniform(string name, float v0, float v1)
    {
        int location = _gl.GetUniformLocation(_program, name);
        _gl.Uniform2(location, v0, v1);
    }

    // Vector2
    public void SetUniform(string name, Vector2 vector)
    {
        int location = _gl.GetUniformLocation(_program, name);
        _gl.Uniform2(location, vector);
    }

    // Vector3
    public void SetUniform(string name, float v0, float v1, float v2)
    {
        int location = _gl.GetUniformLocation(_program, name);
        _gl.Uniform3(location, v0, v1, v2);
    }

    // Vector3
    public void SetUniform(string name, Vector3 vector)
    {
        int location = _gl.GetUniformLocation(_program, name);
        _gl.Uniform3(location, vector);
    }

    // Vector4
    public void SetUniform(string name, float v0, float v1, float v2, float v3)
    {
        int location = _gl.GetUniformLocation(_program, name);
        _gl.Uniform4(location, v0, v1, v2, v3);
    }

    // Vector4
    public void SetUniform(string name, Vector4 vector)
    {
        int location = _gl.GetUniformLocation(_program, name);
        _gl.Uniform4(location, vector);
    }

    // Matrix4x4
    public void SetUniform(string name, Matrix4 matrix)
    {
        int location = _gl.GetUniformLocation(_program, name);
        unsafe
        {
            _gl.UniformMatrix4(location, 1, false, (float*)&matrix);
        }
    }

    //
    // ------------------------------------------------------------------------

    private void LoadShader(string vertexPath, string fragmentPath)
    {
        // 1. recuperar o código-fonte do vértice/fragmento a partir de filePath

        string vShaderCode = string.Empty;
        string fShaderCode = string.Empty;

        try
        {
            // open files
            vShaderCode = File.ReadAllText(vertexPath);
            fShaderCode = File.ReadAllText(fragmentPath);
        }
        catch (Exception ex)
        {
            Debug.LogError(
                "ERROR::SHADER::FILE_NOT_SUCCESFULLY_READ" + "\n" +
                ex + "\n" + 
                " -- --------------------------------------------------- -- "
            );
        }

        // 2. compilar shaders

        uint vertex, fragment;

        // vertex Shader
        vertex = _gl.CreateShader(ShaderType.VertexShader);
        _gl.ShaderSource(vertex, vShaderCode);
        _gl.CompileShader(vertex);
        CheckCompileErrors(vertex, "VERTEX");

        // fragment Shader
        fragment = _gl.CreateShader(ShaderType.FragmentShader);
        _gl.ShaderSource(fragment, fShaderCode);
        _gl.CompileShader(fragment);
        CheckCompileErrors(fragment, "FRAGMENT");

        // shader Program
        _program = _gl.CreateProgram();
        _gl.AttachShader(_program, vertex);
        _gl.AttachShader(_program, fragment);
        _gl.LinkProgram(_program);
        CheckCompileErrors(_program, "PROGRAM");

        // exclua os shaders, pois eles já estão vinculados ao nosso programa e não são mais necessários
        _gl.DeleteShader(vertex);
        _gl.DeleteShader(fragment);
    }

    // função utilitária para verificar erros de compilação/vinculação de shaders.
    // ------------------------------------------------------------------------
    private void CheckCompileErrors(uint shader, string type)
    {
        int success;
        string infoLog;

        if (type != "PROGRAM")
        {
            _gl.GetShader(shader, ShaderParameterName.CompileStatus, out success);
            if (success == 0)
            {
                _gl.GetShaderInfoLog(shader, out infoLog);
                Debug.LogError(
                    "ERROR::SHADER_COMPILATION_ERROR of type: " + type + "\n" + 
                    infoLog + "\n" + 
                    " -- --------------------------------------------------- -- "
                );
            }
        }
        else
        {
            _gl.GetProgram(shader, ProgramPropertyARB.LinkStatus, out success);
            if (success == 0)
            {
                _gl.GetProgramInfoLog(shader, out infoLog);
                Debug.LogError(
                    "ERROR::PROGRAM_LINKING_ERROR of type: " + type + "\n" +
                    infoLog + "\n" + 
                    " -- --------------------------------------------------- -- "
                );
            }
        }
    }

    public void Dispose()
    {
        _gl.DeleteProgram(_program);
    }
}
