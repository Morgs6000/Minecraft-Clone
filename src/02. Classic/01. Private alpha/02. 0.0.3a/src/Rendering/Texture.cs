using Minecraft.Core;
using Minecraft.Utilities;
using Silk.NET.OpenGL;
using StbImageSharp;

namespace Minecraft.Rendering;

public class Texture
{
    private GL _gl = Game.GL;

    public int Width, Height;
    public byte[] Data = [];

    private uint _texture;

    public Texture(string file)
    {
        string path = GetValidFormat($"res/Textures/{file}");

        LoadTexture(path);
    }

    public void Bind(TextureUnit texture = TextureUnit.Texture0)
    {
        // vincular texturas às unidades de textura correspondentes
        _gl.ActiveTexture(texture);
        _gl.BindTexture(TextureTarget.Texture2D, _texture);
    }

    private void LoadTexture(string path)
    {
        _gl.GenTextures(1, out _texture);
        _gl.BindTexture(TextureTarget.Texture2D, _texture);

        // definir os parâmetros de wrapping da textura
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat); // define o modo de repetição da textura como GL_REPEAT (método padrão)
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        // definir parâmetros de filtragem de textura
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

        // carregar imagem, criar textura e gerar mipmaps
        // int width, height;
        // byte[] data;

        // StbImage.stbi_set_flip_vertically_on_load(1);

        using (Stream stream = File.OpenRead(path))
        {
            ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

            Width  = image.Width;
            Height = image.Height;
            Data   = image.Data;
        }

        if (Data != null) 
        {
            unsafe 
            {
                fixed (byte* ptr = Data) 
                {
                    _gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)Width, (uint)Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
                }
            }
            _gl.GenerateMipmap(TextureTarget.Texture2D);
        }
        else
        {
            Debug.LogError("Falha ao carregar a textura.");
        }
    }

    private string GetValidFormat(string path)
    {
        string[] extensions =
        {
            ".jpg", ".png", ".bmp", ".tga", ".psd", ".gif", ".hdr"
        };

        foreach (string? extension in extensions)
        {
            string fullPath = path + extension;

            if (File.Exists(fullPath))
            {
                return fullPath;
            }
        }

        return path;
    }
}
