using Minecraft.Inputs;
using Minecraft.Levels;
using Minecraft.Levels.Blocks;
using Minecraft.Mathematics;
using Minecraft.Physics;
using Minecraft.Utilities;
using Silk.NET.Windowing;

namespace Minecraft.Rendering;

// Uma classe de câmera abstrata que processa a entrada e calcula os ângulos de Euler, vetores e matrizes correspondentes para uso no OpenGL
public class Camera
{
    private Level _level;
    public AABB Bounds = null!;

    // camera Attributes
    public Vector3 Position;
    public Vector3 Front;
    public Vector3 Up;

    // euler Angles
    public float Yaw;
    public float Pitch;

    // camera options
    public float MovementSpeed;
    public float MouseSensitivity;
    public float Zoom;

    private bool _firstMouse = true;
    private Vector2 _lastPos;

    private const float _walking_speed       = 4.317f;
    // private const float _sprinting_speed     = 5.612f;
    // private const float _sneaking_speed      = 1.295f;
    // private const float _flying_speed        = 10.79f;
    // private const float _sprint_flying_speed = 21.58f;
    private const float _falling_speed       = 77.71f;
    private const float _jumping_height      = 1.2522f;
    
    private float _boundsWidht  = 0.6f;
    private float _boundsHeight = 1.8f;
    private float _offsetHeight = 1.62f;

    private Vector3 _delta = Vector3.Zero;
    private Vector3 _velocity = Vector3.Zero;
    private bool _onGround = false;

    // constructor
    public Camera(Level level)
    {
        _level = level;

        Position = new Vector3(0.0f, -3.0f, 0.0f);
        Front = Vector3.PositiveY;
        Up = Vector3.PositiveZ;

        Yaw = 90.0f;
        Pitch = 0.0f;

        MovementSpeed = _walking_speed;
        MouseSensitivity = 0.1f;
        Zoom = 60.0f;

        ResetPos();
    }

    public void ResetMouse()
    {
        _firstMouse = true;
    }

    private void ResetPos()
    {
        Random random = new Random();

        float x = (float)(random.NextDouble() * _level.Width);
        float y = (float)(random.NextDouble() * _level.Depth);
        float z = (float)(_level.Height + 10);

        SetPos(new Vector3(x, y, z));
    }

    private void SetPos(Vector3 position)
    {
        Position = position;
    }

    // retorna a matriz de visualização calculada usando ângulos de Euler e a matriz LookAt
    public Matrix4 GetViewMatrix()
    {
        return Matrix4.LookAt(
            Position,
            Position + Front,
            Up
        );
    }

    // retorna a matriz de projeção
    public Matrix4 GetProjectionMatrix()
    {
        return Matrix4.Perspective(
            Mathf.Radians(Zoom), 
            (float)Screen.Width / (float)Screen.Height, 
            0.3f, 
            1000.0f
        );
    }

    // processa a entrada recebida de qualquer sistema de entrada do tipo teclado. Aceita um parâmetro de entrada na forma de um ENUM definido pela câmera (para abstraí-lo de sistemas de janelas)
    public void ProcessKeyboad()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ResetPos();
        }

        ProcessKeyboadMovement();
    }

    private void ProcessKeyboadMovement()
    {
        float velocity = MovementSpeed * Time.DeltaTime;

        _delta = Vector3.Zero;

        Vector3 front = Vector3.Normalize(new Vector3(Front.X, Front.Y, 0.0f));
        Vector3 right = Vector3.Normalize(Vector3.Cross(Front, Up));
        Vector3 up    = Up;

        if (Input.GetKey(KeyCode.W))
        {
            _delta += velocity * front;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _delta -= velocity * front;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _delta -= velocity * right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _delta += velocity * right;
        }

        /*
        if (Input.GetKey(KeyCode.Space))
        {
            Position += velocity * up;
        }
        if (Input.GetKey(KeyCode.ShiftLeft))
        {
            Position -= velocity * up;
        }
        */

        // Aplicar gravidade
        _velocity.Z -= _falling_speed * Time.DeltaTime;

        if (Input.GetKey(KeyCode.Space) && _onGround)
        {
            _velocity.Z = (float)Math.Sqrt(2 * _falling_speed * _jumping_height);
        }

        _delta.Z = _velocity.Z * Time.DeltaTime;

        Move(_delta);
    }

    // processa a entrada recebida de um sistema de entrada de mouse. Espera o valor de deslocamento nas direções x e y.
    public void ProcessMouseMovement()
    {
        if (_firstMouse)
        {
            _lastPos = Input.MousePosition;
            _firstMouse = false;
        }

        float xoffset = _lastPos.X - Input.MousePosition.X;
        float yoffset = _lastPos.Y - Input.MousePosition.Y;
        _lastPos = Input.MousePosition;

        xoffset *= MouseSensitivity;
        yoffset *= MouseSensitivity;

        Yaw   += xoffset;
        Pitch += yoffset;

        // certifique-se de que a tela não seja invertida quando o pitch estiver fora dos limites
        Pitch = Mathf.Clamp(Pitch, -89.0f, 89.0f);

        // atualiza os vetores Front, Right e Up usando os ângulos de Euler atualizados
        UpdateCameraVectors();
    }

    // processa a entrada recebida de um evento de roda de rolagem do mouse. Requer entrada apenas no eixo vertical da roda.
    public void ProcessMouseScroll()
    {
        Zoom -= Input.MouseScrollDelta.Y;
        Zoom = Mathf.Clamp(Zoom, 1.0f, 45.0f);
    }

    // calcula o vetor frontal a partir dos ângulos de Euler (atualizados) da câmera
    private void UpdateCameraVectors()
    {
        // calcula o novo vetor Front
        Vector3 direction;

        direction.X = Mathf.Cos(Mathf.Radians(Pitch)) * Mathf.Cos(Mathf.Radians(Yaw));
        direction.Y = Mathf.Cos(Mathf.Radians(Pitch)) * Mathf.Sin(Mathf.Radians(Yaw));
        direction.Z = Mathf.Sin(Mathf.Radians(Pitch));

        Front = Vector3.Normalize(direction);
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
                        /*
                        AABB bounds = new AABB(
                            new Vector3(x, y, z),
                            new Vector3(x + 1, y + 1, z + 1)
                        );
                        */
                        int blockID = _level.GetBlock(new Vector3Int(x, y, z));
                        AABB bounds = Block.Blocks[blockID].GetBounds(new Vector3Int(x, y, z));

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

    private void Move(Vector3 delta)
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
}
