using RubyDung.Core;
using RubyDung.Gui;
using RubyDung.Inputs;
using RubyDung.Levels.Blocks;
using RubyDung.Mathematics;
using RubyDung.Meshing;
using RubyDung.Rendering;
using Silk.NET.OpenGL;
using Shader = RubyDung.Rendering.Shader;

namespace RubyDung.Levels;

public class BlockInteraction
{
    private GL _gl = Game.GL;

    private Level _level;

    private Camera _camera;

    private HitResult _hitResult = null!;

    private Color _color;

    private MeshQuad _mesh = null!;

    private MeshRender _meshRender;

    public BlockInteraction(Level level, Camera camera)
    {
        _level = level;
        _camera = camera;

        _mesh = new MeshQuad();
        _meshRender = new MeshRender();
    }

    public void Update()
    {
        if (RayCast())
        {
            RenderHit(_hitResult);

            Vector3Int position = new(
                _hitResult.X,
                _hitResult.Y,
                _hitResult.Z
            );

            if (Input.GetKeyDown(KeyCode.MouseRight))
            {
                _level.SetBlock(position, 0);
            }
            if (Input.GetKeyDown(KeyCode.MouseLeft))
            {
                if (_hitResult.F == (int)BlockFace.NegativeX)
                {
                    position.X--;
                }
                if (_hitResult.F == (int)BlockFace.PositiveX)
                {
                    position.X++;
                }
                if (_hitResult.F == (int)BlockFace.NegativeY)
                {
                    position.Y--;
                }
                if (_hitResult.F == (int)BlockFace.PositiveY)
                {
                    position.Y++;
                }
                if (_hitResult.F == (int)BlockFace.NegativeZ)
                {
                    position.Z--;
                }
                if (_hitResult.F == (int)BlockFace.PositiveZ)
                {
                    position.Z++;
                }

                int b = GuiSelectedBlock.PaintTexture;

                _level.SetBlock(position, b);
            }
        }
        else
        {
            _mesh.Clear();
            _meshRender.Mesh = _mesh;
        }
    }

    public void Render(Shader shader)
    {
        shader.SetUniform("uColor", _color);

        _gl.Enable(EnableCap.Blend);
        _gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

        _gl.Enable(EnableCap.PolygonOffsetFill);
        _gl.PolygonOffset(-1.0f, -1.0f);

        _meshRender.Draw(shader);
        
        _gl.Disable(EnableCap.PolygonOffsetFill);
        _gl.Disable(EnableCap.Blend);

        shader.SetUniform("uColor", 1.0f, 1.0f, 1.0f, 1.0f);
    }

    private void RenderHit(HitResult h)
    {
        float alpha = Mathf.Sin(Environment.TickCount / 100.0f) * 0.2f + 0.4f;
        _color = new Color(1.0f, 1.0f, 1.0f, alpha);

        _mesh.Clear();

        Block.Rock.AddFace(_mesh, new Vector3Int(h.X, h.Y, h.Z), (BlockFace)h.F);

        _meshRender.Mesh = _mesh;
    }

    private bool RayCast()
    {
        Vector3 start = _camera.Position;
        Vector3 dir = _camera.Front;

        float maxDist = 5.0f;

        // Verifica se o vetor direção é zero
        if (dir.X == 0 && dir.Y == 0 && dir.Z == 0)
            return false;

        dir = Vector3.Normalize(dir);

        // Posição inicial do voxel (arredondando para baixo)
        Vector3Int pos = new Vector3Int(
            Mathf.FloorToInt(start.X),
            Mathf.FloorToInt(start.Y),
            Mathf.FloorToInt(start.Z)
        );

        // Direção dos passos (1 ou -1)
        Vector3Int step = new Vector3Int(
            Mathf.SignToInt(dir.X),
            Mathf.SignToInt(dir.Y),
            Mathf.SignToInt(dir.Z)
        );

        // Distâncias até a próxima fronteira do voxel (em unidades do ray)
        Vector3 tMax = new Vector3(
            dir.X != 0 ? (dir.X > 0 ? (pos.X + 1 - start.X) / dir.X : (start.X - pos.X) / -dir.X) : float.MaxValue,
            dir.Y != 0 ? (dir.Y > 0 ? (pos.Y + 1 - start.Y) / dir.Y : (start.Y - pos.Y) / -dir.Y) : float.MaxValue,
            dir.Z != 0 ? (dir.Z > 0 ? (pos.Z + 1 - start.Z) / dir.Z : (start.Z - pos.Z) / -dir.Z) : float.MaxValue
        );

        Vector3 tDelta = new Vector3(
            dir.X != 0 ? 1f / Mathf.Abs(dir.X) : float.MaxValue,
            dir.Y != 0 ? 1f / Mathf.Abs(dir.Y) : float.MaxValue,
            dir.Z != 0 ? 1f / Mathf.Abs(dir.Z) : float.MaxValue
        );

        float dist = 0;
        int face = -1;

        while (dist < maxDist)
        {
            // Verifica se o bloco atual é sólido usando o World
            if (_level.IsSolidBlock(pos))
            {
                _hitResult = new HitResult(pos.X, pos.Y, pos.Z, face);
                return true;
            }

            // Avança para o próximo voxel (menor tMax)
            if (tMax.X < tMax.Y && tMax.X < tMax.Z)
            {
                pos.X += step.X;
                dist = tMax.X;
                tMax.X += tDelta.X;

                // 0: +X, 1: -X
                face = step.X > 0 ?
                    (int)BlockFace.NegativeX : 
                    (int)BlockFace.PositiveX;
            }
            else if (tMax.Y < tMax.Z)
            {
                pos.Y += step.Y;
                dist = tMax.Y;
                tMax.Y += tDelta.Y;

                // 2: +Y (frente), 3: -Y (trás)
                face = step.Y > 0 ?
                    (int)BlockFace.NegativeY :
                    (int)BlockFace.PositiveY;
            }
            else
            {
                pos.Z += step.Z;
                dist = tMax.Z;
                tMax.Z += tDelta.Z;

                // 4: +Z (cima), 5: -Z (baixo)
                face = step.Z > 0 ?
                    (int)BlockFace.NegativeZ :
                    (int)BlockFace.PositiveZ;
            }
        }

        return false;
    }
}
