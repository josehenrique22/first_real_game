using Godot;
using PlayerEntityComponent;
using System;

public partial class PlayerCharacterController : CharacterBody3D
{
    [Export] private MovementComponent _movementComponent;

    public override void _PhysicsProcess(double delta)
    {
        _movementComponent.Movement(this, delta);        
    }
}
