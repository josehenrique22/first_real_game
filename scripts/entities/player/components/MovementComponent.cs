using System;
using DTOS;
using Godot;
using InputManagerSystem;

namespace PlayerEntityComponent;

public partial class MovementComponent : Node
{
    [Export] private MovementDTO _movementDTO;
    [Export] private InputManager _inputManager;

    /*
        Quando o player se mecher
            rotacione na direção onde esta se andando.

        Não sei como a rotação funciona para poder aplicar oque quero!

    */

    // TODO: Descubra como funciona rotações apropriadamente e implemente um limite ate onde pode rotacionar (axis Y)
    public void Movement(CharacterBody3D _entity, double _delta)
    {
        Vector3 direction = new Vector3(_inputManager.XAxisInput,
        _entity.Velocity.Y, _inputManager.ZAxisInput).Normalized()
        * _movementDTO.Speed;

        if (_entity.Velocity != Vector3.Zero)
        {
            _entity.LookAtFromPosition(_entity.Position, direction);
        } 

        PlayerInTheAirCondition(_entity);

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
