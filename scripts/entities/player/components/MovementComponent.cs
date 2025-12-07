using DTOS;
using Godot;

namespace PlayerEntityComponent;

public partial class MovementComponent : Node
{
    [Export] private MovementDTO _movementDTO;
    public void Movement(CharacterBody3D _entity, double _delta)
    {
        float xAxis = Input.GetAxis("left", "right");
        float zAxis = Input.GetAxis("foward", "backward");


        Vector3 direction = new Vector3(xAxis, _entity.Velocity.Y, zAxis) * _movementDTO.Speed;
        ApplyPlayerCondition(_entity, direction);

        _entity.Velocity = direction * GetPlayerFriction(_delta, direction);
        _entity.MoveAndSlide();
    }

    private float GetPlayerFriction(double _delta, Vector3 _direction)
    {
        float playerFriction = PlayerMoving(_direction) ?
        _movementDTO.Acceleration * (float)_delta :
        _movementDTO.Deceleration * (float)_delta;

        return playerFriction;
    }

    private static void ApplyPlayerCondition(CharacterBody3D _entity, Vector3 _direction)
    {
        if (PlayerMoving(_direction))
        {
            _direction.Normalized();
        }

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
