using RubyDung.Character;
using RubyDung.Inputs;
using RubyDung.Levels;
using RubyDung.Mathematics;
using RubyDung.Physics;
using RubyDung.Utilities;
using Silk.NET.Windowing;

namespace RubyDung.Rendering;

// Uma classe de câmera abstrata que processa a entrada e calcula os ângulos de Euler, vetores e matrizes correspondentes para uso no OpenGL
public class Camera : Entity
{
    private bool _firstMouse = true;
    private Vector2 _lastPos;

    private const float _walking_speed       = 4.317f;
    // private const float _sprinting_speed     = 5.612f;
    // private const float _sneaking_speed      = 1.295f;
    // private const float _flying_speed        = 10.79f;
    // private const float _sprint_flying_speed = 21.58f;

    // constructor
    public Camera(Level level) : base(level)
    {
        // Position = new Vector3(0.0f, -3.0f, 0.0f);
        Front = Vector3.PositiveY;
        Up = Vector3.PositiveZ;

        Yaw = 90.0f;
        Pitch = 0.0f;

        MovementSpeed = _walking_speed;
        MouseSensitivity = 0.1f;
        Zoom = 60.0f;

        _boundsWidht = 0.6f;
        _boundsHeight = 1.8f;
        _offsetHeight = 1.62f;
    }

    public void Update()
    {
        ProcessKeyboad();
        ProcessMouseMovement();
        // ProcessMouseScroll();
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
    public Matrix4 GetProjectionMatrix(IWindow window)
    {
        return Matrix4.Perspective(
            Mathf.Radians(Zoom), 
            (float)window.Size.X / (float)window.Size.Y, 
            0.3f, 
            1000.0f
        );
    }

    // processa a entrada recebida de qualquer sistema de entrada do tipo teclado. Aceita um parâmetro de entrada na forma de um ENUM definido pela câmera (para abstraí-lo de sistemas de janelas)
    private void ProcessKeyboad()
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
        //*/

        //*
        // Aplicar gravidade
        _velocity.Z -= _falling_speed * Time.DeltaTime;

        if (Input.GetKey(KeyCode.Space) && _onGround)
        {
            _velocity.Z = (float)Math.Sqrt(2 * _falling_speed * _jumping_height);
        }

        _delta.Z = _velocity.Z * Time.DeltaTime;
        //*/

        Move(_delta);
    }

    // processa a entrada recebida de um sistema de entrada de mouse. Espera o valor de deslocamento nas direções x e y.
    private void ProcessMouseMovement()
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
    private void ProcessMouseScroll()
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
}
