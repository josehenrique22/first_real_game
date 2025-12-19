using DTOS;
using Godot;
using InputManagerSystem;

namespace PlayerEntityComponent;

// TODO: Fazer uma interpolação entre as rotação da camera.
public partial class MovementComponent : Node
{
    [Export] private MovementDTO _movementDTO;
    [Export] private InputManager _inputManager;

    public void Movement(CharacterBody3D _entity, double _delta)
    {
        Vector3 direction = new Vector3(_inputManager.XAxisInput,
        _entity.Velocity.Y, _inputManager.ZAxisInput).Normalized()
        * _movementDTO.Speed;
        
        PlayerMovementRotation(_entity, direction);

        PlayerInTheAirCondition(_entity);

        _entity.Velocity = direction * GetPlayerFriction(_delta, direction);
        _entity.MoveAndSlide();
    }

    private static void PlayerMovementRotation(CharacterBody3D _entity, Vector3 direction)
    {
        if (PlayerMoving(direction))
        {
            // Criar um ponto de destino na direção do movimento
            Vector3 lookAtTarget = _entity.GlobalPosition + new Vector3(direction.X, 0, direction.Z);
            _entity.LookAt(lookAtTarget, Vector3.Up);
        }
    }

    private float GetPlayerFriction(double _delta, Vector3 _direction)
    {
        float playerFriction = PlayerMoving(_direction) ?
        _movementDTO.Acceleration * (float)_delta :
        _movementDTO.Deceleration * (float)_delta;

        return playerFriction;
    }

    private static void PlayerInTheAirCondition(CharacterBody3D _entity)
    {
        if (!_entity.IsOnFloor())
        {
            _entity.Velocity = _entity.GetGravity();
        }
    }

    private static bool PlayerMoving(Vector3 _direction)
    {
        return _direction != Vector3.Zero;
    }
}
