using RubyDung.Inputs;
using RubyDung.Levels;
using RubyDung.Mathematics;
using RubyDung.Meshing;
using RubyDung.Rendering;
using RubyDung.Utilities;

namespace RubyDung.Character;

public class Zombie : Entity
{
    private Texture _texture = null!;

    private MeshRender _head = null!;
    private MeshRender _body = null!;
    private MeshRender _armL = null!;
    private MeshRender _armR = null!;
    private MeshRender _legL = null!;
    private MeshRender _legR = null!;

    private float _timeOffs;
    private float _speed = 1.0f;

    public Zombie(Level level) : base(level)
    {
        _timeOffs = (float)_ramdom.NextDouble() * 12345.0f;

        _texture = new Texture("char");

        // Debug.Log($"{_texture.Width}, {_texture.Height}");

        MeshCube head = new MeshCube();
        head.TextureSize = new Vector2Int(_texture.Width, _texture.Height);
        head.UVRect = new Vector4Int(8, 8, 8, 8);
        head.AddCube(new Vector3(0.0f, 0.0f, 2.4f), new Vector3(0.8f, 0.8f, 0.8f));
        _head = new MeshRender(head);

        MeshCube body = new MeshCube();
        body.TextureSize = new Vector2Int(_texture.Width, _texture.Height);
        body.UVRect = new Vector4Int(20, 20, 8, 12);
        body.AddCube(new Vector3(0.0f, 0.2f, 1.2f), new Vector3(0.8f, 0.4f, 1.2f));
        _body = new MeshRender(body);

        MeshCube armL = new MeshCube();
        armL.TextureSize = new Vector2Int(_texture.Width, _texture.Height);
        armL.UVRect = new Vector4Int(44, 20, 4, 12);
        armL.AddCube(new Vector3(0.8f, 0.2f, 1.2f), new Vector3(0.4f, 0.4f, 1.2f));
        _armL = new MeshRender(armL);

        MeshCube armR = new MeshCube();
        armR.TextureSize = new Vector2Int(_texture.Width, _texture.Height);
        armR.UVRect = new Vector4Int(44, 20, 4, 12);
        armR.AddCube(new Vector3(-0.4f, 0.2f, 1.2f), new Vector3(0.4f, 0.4f, 1.2f));
        _armR = new MeshRender(armR);

        MeshCube legL = new MeshCube();
        legL.TextureSize = new Vector2Int(_texture.Width, _texture.Height);
        legL.UVRect = new Vector4Int(4, 20, 4, 12);
        legL.AddCube(new Vector3(0.4f, 0.2f, 0.0f), new Vector3(0.4f, 0.4f, 1.2f));
        _legL = new MeshRender(legL);

        MeshCube legR = new MeshCube();
        legR.TextureSize = new Vector2Int(_texture.Width, _texture.Height);
        legR.UVRect = new Vector4Int(4, 20, 4, 12);
        legR.AddCube(new Vector3(0.0f, 0.2f, 0.0f), new Vector3(0.4f, 0.4f, 1.2f));
        _legR = new MeshRender(legR);
    }

    public void Update()
    {
        // Movimento IA Simples
        if (_ramdom.NextDouble() < 0.02f)
        {
            Yaw += (float)(_ramdom.NextDouble() - _ramdom.NextDouble()) * 0.5f;
        }

        float xDir = Mathf.Cos(Yaw);
        float yDir = Mathf.Sin(Yaw);

        _delta.X = xDir * 0.02f;
        _delta.Y = yDir * 0.02f;

        // Aplicar gravidade
        _velocity.Z -= _falling_speed * Time.DeltaTime;

        if (_ramdom.NextDouble() < 0.01f && _onGround)
        {
            _velocity.Z = (float)Math.Sqrt(2 * _falling_speed * _jumping_height);
        }

        _delta.Z = _velocity.Z * Time.DeltaTime;

        Move(_delta);

        // Fricção
        _delta.X *= 0.91f;
        _delta.Z *= 0.91f;
        _delta.Y *= 0.98f;
    }

    public void Render(Shader shader)
    {
        _texture.Bind();

        float time = Time.ElapsedTime * 10.0f * _speed + _timeOffs;
        float size = 0.058333334f; // Escala mágica do rd-132328

        // Bobbing vertical (pulo suave ao andar)
        float yy = -MathF.Abs(MathF.Sin(time * 0.6662f)) * 5.0f - 23.0f;

        // Ordem correta para OpenTK (Escala -> Rotação -> Translação)
        // Para seguir o comportamento do GL11 (Stack), multiplicamos na ordem inversa
        Matrix4 baseTransform = Matrix4.RotateY(Yaw + MathF.PI);
        baseTransform *= Matrix4.Translate(0, yy, 0);
        baseTransform *= Matrix4.Scale(size, -size, size); // Escala e Inversão de Y combinadas
        baseTransform *= Matrix4.Translate(Position);

        shader.SetUniform("uModel", baseTransform);

        _head.Draw(shader);
        _body.Draw(shader);

        _armL.Draw(shader);
        _armR.Draw(shader);

        _legL.Draw(shader);
        _legR.Draw(shader);
    }
}
