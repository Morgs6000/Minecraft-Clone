using RubyDung.Levels;
using RubyDung.Mathematics;
using RubyDung.Physics;

namespace RubyDung.Character;

public class Entity
{
    protected Level _level;
    protected AABB Bounds = null!;

    protected Random _ramdom;

    // camera Attributes
    public Vector3 Position { get; protected set; }
    public Vector3 Front { get; protected set; }
    public Vector3 Up { get; protected set; }

    // euler Angles
    public float Yaw { get; protected set; }
    public float Pitch { get; protected set; }

    // camera options
    public float MovementSpeed { get; protected set; }
    public float MouseSensitivity { get; protected set; }
    public float Zoom { get; protected set; }

    protected const float _falling_speed       = 77.71f;
    protected const float _jumping_height      = 1.2522f;
    
    protected float _boundsWidht  = 0.0f;
    protected float _boundsHeight = 0.0f;
    protected float _offsetHeight = 0.0f;

    protected Vector3 _delta = Vector3.Zero;
    protected Vector3 _velocity = Vector3.Zero;
    protected bool _onGround = false;

    public Entity(Level level)
    {
        _level = level;
        _ramdom = new Random();

        ResetPos();
    }

    protected void ResetPos()
    {
        float x = (float)(_ramdom.NextDouble() * _level.Width);
        float y = (float)(_ramdom.NextDouble() * _level.Depth);
        float z = (float)(_level.Height + 10);

        SetPos(new Vector3(x, y, z));
    }

    private void SetPos(Vector3 position)
    {
        Position = position;
    }

    protected void Move(Vector3 delta)
    {
        // Tenta mover na direção X (esquerda/direita)
        Vector3 newPosition = Position + new Vector3(delta.X, 0.0f, 0.0f);

        if (!IsColliding(newPosition))
        {
            Position = newPosition;
        }

        // Tenta mover na direção Y (frente/trás)
        newPosition = Position + new Vector3(0.0f, delta.Y, 0.0f);
        
        if (!IsColliding(newPosition))
        {
            Position = newPosition;
        }

        // Tenta mover na direção Z (cima/baixo)
        newPosition = Position + new Vector3(0.0f, 0.0f, delta.Z);

        if (!IsColliding(newPosition))
        {
            Position = newPosition;
            _onGround = false;
        }
        else
        {
            if (delta.Z < 0)
            {
                _onGround = true;
                _velocity.Z = 0;
            }

        }
    }

    // Verifica se a posição da câmera colide com algum bloco sólido
    private bool IsColliding(Vector3 position)
    {
        // Calcula as dimensões da caixa de colisão
        float _w = _boundsWidht / 2.0f;
        float _h = _boundsHeight / 2.0f;

        // Centro da caixa em X e Z, centro da caixa em Y ajustado pela altura dos olhos
        float _x = position.X;
        float _y = position.Y;
        float _z = position.Z - _offsetHeight + _h;

        // Vetores min e max da caixa de colisão
        Bounds = new AABB(
            new Vector3(_x - _w, _y - _w, _z - _h),
            new Vector3(_x + _w, _y + _w, _z + _h)
        );

        // Limites inteiros para verificar blocos
        Vector3Int min = new Vector3Int(
            Mathf.FloorToInt(Bounds.Min.X),
            Mathf.FloorToInt(Bounds.Min.Y),
            Mathf.FloorToInt(Bounds.Min.Z)
        );

        Vector3Int max = new Vector3Int(
            Mathf.CeilToInt(Bounds.Max.X),
            Mathf.CeilToInt(Bounds.Max.Y),
            Mathf.CeilToInt(Bounds.Max.Z)
        );

        // Verifica colisão com cada bloco possível
        for (int x = min.X; x <= max.X; x++)
        {
            for (int y = min.Y; y <= max.Y; y++)
            {
                for (int z = min.Z; z <= max.Z; z++)
                {
                    if (_level.IsSolidBlock(new Vector3Int(x, y, z)))
                    {
                        // Vetores min e max do bloco
                        AABB bounds = new AABB(
                            new Vector3(x, y, z),
                            new Vector3(x + 1, y + 1, z + 1)
                        );

                        // Verifica se as caixas AABB se intersectam
                        if (Bounds.Intersects(bounds))
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
}
